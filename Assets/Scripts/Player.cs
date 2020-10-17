using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    public float speed = 4f;
    public float maxShootForce = 15f;
    public float minShootForceFraction = 0.4f;

    public GameObject snowballPrefab;
    public Transform snowballSpawn;
    public ProgressBar shootProgressBar;

    private float _shootForce;
    private Vector2 _movementVector;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float x = speed * _movementVector.x;
        _rigidbody.velocity = new Vector3(x, 0, 0);
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
            _shootForce = shootProgressBar.currentFill * maxShootForce * (1 - minShootForceFraction);
            shootProgressBar.StopAndReset();
            Shoot();
        }
    }

    private void Shoot()
    {
        GameObject snowball = Instantiate(snowballPrefab, snowballSpawn.position, snowballSpawn.rotation);        
        Rigidbody snowballRB = snowball.GetComponent<Rigidbody>();

        float multiplier = _shootForce + maxShootForce * minShootForceFraction;
        snowballRB.AddForce(snowball.transform.forward * multiplier, ForceMode.Impulse);
        
        _shootForce = 0f;
    }
}
