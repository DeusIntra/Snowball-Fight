using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMover : MonoBehaviour
{
    public float speed = 4f;
    public float boundaryDistance = 5f;

    public Joystick joystick;

    private float _horizontalMovement;

    private Rigidbody _rigidbody;
    private PlayerAnimator _playerAnimator;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _playerAnimator = GetComponent<PlayerAnimator>();
    }

    void Update()
    {
        _horizontalMovement = joystick.Horizontal;
        _playerAnimator.SetSpeed(Mathf.Abs(_horizontalMovement));
    }

    private void FixedUpdate()
    {
        float x_speed = speed * _horizontalMovement;
        _rigidbody.velocity = new Vector3(x_speed, 0f, 0f);

        if (transform.position.x < -boundaryDistance || transform.position.x > boundaryDistance)
        {
            float x = Mathf.Round(transform.position.x);
            float y = transform.position.y;
            float z = transform.position.z;
            transform.position = new Vector3(x, y, z);
        }
    }

    private void OnDisable()
    {
        _rigidbody.velocity = Vector3.zero;
    }
}
