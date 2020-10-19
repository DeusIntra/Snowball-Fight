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

    private float _shootForce;
    private Vector2 _movementVector;
    private Rigidbody _rigidbody;
    private Animator _animator;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        _animator.speed = Mathf.Abs(_movementVector.x);
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
        }

        if (callbackContext.phase == InputActionPhase.Canceled)
        {
            _shootForce = shootProgressBar.currentFill * maxShootForce * (1f - minShootForceFraction);
            shootProgressBar.StopAndReset();
            Shoot();
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
}
