using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.OnScreen;

[RequireComponent(typeof(PlayerShooter))]
[RequireComponent(typeof(PlayerMover))]
[RequireComponent(typeof(PlayerAnimator))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Mana))]
public class Player : MonoBehaviour
{
    public HeartsHealthBar healthBar;
    public ProgressBar spellBar;

    public OnScreenButton shootButton;

    private bool _isSwinging = false;

    private PlayerShooter _shooter;
    private PlayerMover _mover;
    private PlayerAnimator _playerAnimator;
    private ProgressBarTimed _shootProgressBar;
    private Health _health;
    private Mana _mana;

    private GameControls _gameControls;

    private void Awake()
    {
        _shooter = GetComponent<PlayerShooter>();
        _playerAnimator = GetComponent<PlayerAnimator>();
        _mover = GetComponent<PlayerMover>();
        _shootProgressBar = _shooter.progressBar;
        _health = GetComponent<Health>();
        _mana = GetComponent<Mana>();

        _gameControls = new GameControls();
        ReadShootInput();
    }

    private void OnEnable()
    {
        _gameControls.Enable();
    }

    private void OnDisable()
    {
        _gameControls.Disable();
    }

    private void Update()
    {
        ReadMoveInput();

        if (_isSwinging)
        {
            float FPS = Mathf.Lerp(4, 10, _shootProgressBar.currentFill);
            _playerAnimator.SetFPS(FPS);
        }
    }

    public void ReadShootInput()
    {
        _gameControls.Gameplay.Shoot.started += ctx =>
        {
            _shootProgressBar.StartFilling();
            _isSwinging = true;
            _playerAnimator.Swing();
            _playerAnimator.SetFPS(4f);
        };

        _gameControls.Gameplay.Shoot.canceled += ctx =>
        {
            if (_shootProgressBar.currentFill > 0.1f)
            {
                _shooter.Shoot();
                _playerAnimator.Throw();
            }
            else
            {
                _shootProgressBar.StopAndReset();
                _playerAnimator.Walk();
            }

            _isSwinging = false;
        };
    }

    public void ReadMoveInput()
    {
        float value = _gameControls.Gameplay.Move.ReadValue<float>();
        _mover.SetHorizontal(value);
    }

    public void FillHealthBar()
    {
        healthBar.value = _health.current;
        healthBar.onChange();
    }

    public void OnZeroHealth()
    {
        if (!_health.isAlive) Die();
    }

    public void FillSpellBar()
    {
        spellBar.SetFill(_mana.currentFraction);
    }

    private void Die()
    {
        _playerAnimator.Die();

        _shooter.enabled = false;
        _mover.enabled = false;
        _mover.joystick.gameObject.SetActive(false);
        _shootProgressBar.enabled = false;

        shootButton.gameObject.SetActive(false);
        spellBar.gameObject.SetActive(false);
    }
}
