using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    public float speed = 4f;
    public float maxShootForce = 15f;
    public float minShootForceFraction = 0.4f;
    public float boundaryDistance = 5f;
    public float snowballSideVelocityMultiplier = 1f;

    public GameObject snowballPrefab;
    public Transform snowballSpawn;
    public ProgressBar shootProgressBar;
    public MeshAnimations meshAnimations;

    private float _shootForce;
    private Vector2 _movementVector;
    private Rigidbody _rigidbody;
    private Animator _animator;
    private Coroutine _delayedCoroutine;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        meshAnimations.walk.Play();
    }

    private void Update()
    {
        Animate();
    }

    private void FixedUpdate()
    {
        float x = speed * _movementVector.x;
        _rigidbody.velocity = new Vector3(x, 0f, 0f);

        if (transform.position.x < -boundaryDistance)
        {
            transform.position = new Vector3(-boundaryDistance, transform.position.y, transform.position.z);
        }

        if (transform.position.x > boundaryDistance)
        {
            transform.position = new Vector3(boundaryDistance, transform.position.y, transform.position.z);
        }
    }

    public void ReadMovementInput(InputAction.CallbackContext callbackContext)
    {
        _movementVector = callbackContext.ReadValue<Vector2>();
    }

    public void ReadShootInput(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.phase == InputActionPhase.Started)
        {
            shootProgressBar.StartFilling();
            meshAnimations.StopAll();
            meshAnimations.swing.Play();
            _animator.Play("swing");

            if (_delayedCoroutine != null)
                StopCoroutine(_delayedCoroutine);
        }

        if (callbackContext.phase == InputActionPhase.Canceled)
        {
            _shootForce = shootProgressBar.currentFill * maxShootForce * (1f - minShootForceFraction);
            shootProgressBar.StopAndReset();
            Shoot();

            meshAnimations.StopAll();
            meshAnimations.throwing.Play();
            _animator.Play("throwing");
            
            _delayedCoroutine = StartCoroutine(WalkAnimation());
        }
    }

    private void Shoot()
    {
        GameObject snowball = Instantiate(snowballPrefab, snowballSpawn.position, snowballSpawn.rotation);        
        Rigidbody snowballRB = snowball.GetComponent<Rigidbody>();

        float multiplier = _shootForce + maxShootForce * minShootForceFraction;
        Vector3 forwardVelocity = snowball.transform.forward * multiplier;
        Vector3 sideVelocity = _rigidbody.velocity * snowballSideVelocityMultiplier;
        Vector3 snowballVelocity = forwardVelocity + sideVelocity;
        snowballRB.AddForce(snowballVelocity, ForceMode.Impulse);
        
        _shootForce = 0f;
    }

    private void Animate()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("walk"))
        {
            _animator.speed = Mathf.Abs(_movementVector.x);
        }

        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("swing"))
        {
            meshAnimations.swing.FramesPerSecond = Mathf.Lerp(4, 10, shootProgressBar.currentFill);
        }
    }

    private IEnumerator WalkAnimation()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        meshAnimations.StopAll();
        meshAnimations.walk.Play();
        _animator.Play("walk");
    }
}
