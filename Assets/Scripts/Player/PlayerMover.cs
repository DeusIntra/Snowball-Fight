using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
public class PlayerMover : MonoBehaviour
{
    public float speed = 4f;
    public float speedMultiplier = 1f;
    public float boundaryDistance = 5f;
    public float stepTime = 0.5f;
    public bool useJoystick = true;
    public bool doStep = true;

    public Joystick joystick;
    public Transform leftFoot;
    public Transform rightFoot;
    public ParticleSystem particlesPrefab;

    private float _horizontalMovement;
    private float _timeToStep;
    private bool _isLeftStep;

    private Rigidbody _rigidbody;
    private PlayerAnimator _playerAnimator;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _playerAnimator = GetComponent<PlayerAnimator>();
    }

    void Update()
    {
        if (useJoystick)
        {
            _horizontalMovement = joystick.Horizontal;
        }
        
        float horizontalAbs = Mathf.Abs(_horizontalMovement);
        _playerAnimator.SetSpeed(horizontalAbs);

        if (_timeToStep >= stepTime)
        {
            _timeToStep = 0;
            Step();
        }

        if (horizontalAbs > 0.01f) 
            _timeToStep += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        float x_speed = speed * speedMultiplier * _horizontalMovement;
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


    public void SetHorizontal(float value)
    {
        if (!useJoystick)
        {
            _horizontalMovement = value;
        }
    }

    private void Step()
    {
        if (doStep)
        {
            Vector3 spawnPoint;
            if (_isLeftStep) spawnPoint = leftFoot.position;
            else spawnPoint = rightFoot.position;
            _isLeftStep = !_isLeftStep;

            ParticleSystem particles = Instantiate(particlesPrefab, spawnPoint, particlesPrefab.transform.rotation);
            Destroy(particles.gameObject, stepTime);
        }
    }
}
