using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    public float speed = 0.05f;
    public GameObject snowballPrefab;

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
        GameObject snowball = Instantiate(snowballPrefab, transform.position, Quaternion.identity);
        Debug.Log(callbackContext.ReadValueAsButton());
    }
}
