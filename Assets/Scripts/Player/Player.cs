using UnityEngine;
using UnityEngine.Audio;
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
    public HeartsHealthBar healthBar6;
    public ProgressBar spellBar;

    public OnScreenButton shootButton;

    public AudioClip playerDeathSound;
    public AudioMixerGroup playerDeathMixer;
    [SerializeField] private ProgressBarTimed _shotProgressBar;

    private bool _isSwinging = false;

    private PlayerShooter _shooter;
    private PlayerMover _mover;
    private PlayerAnimator _playerAnimator;
    private Health _health;
    private Mana _mana;
    private CharacterAudio _playerAudio;

    private GameControls _gameControls;

    private void Awake()
    {
        _shooter = GetComponent<PlayerShooter>();
        _playerAnimator = GetComponent<PlayerAnimator>();
        _mover = GetComponent<PlayerMover>();
        _health = GetComponent<Health>();
        _mana = GetComponent<Mana>();
        _playerAudio = GetComponent<CharacterAudio>();

        _gameControls = new GameControls();
        _gameControls.Gameplay.Shoot.started += OnShootActionStarted;
        _gameControls.Gameplay.Shoot.canceled += OnShootActionCancelled;
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
        if (_isSwinging)
        {
            float FPS = Mathf.Lerp(4, 10, _shotProgressBar.currentFill);
            _playerAnimator.SetFPS(FPS);
        }
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
        _shooter.enabled = false;
        _mover.enabled = false;
        _mover.joystick.gameObject.SetActive(false);
        _shotProgressBar.enabled = false;
        GetComponent<Collider>().enabled = false;

        _playerAudio.Die();
    }

    private void OnShootActionStarted(InputAction.CallbackContext ctx)
    {
        _shotProgressBar.StartFilling();
        _isSwinging = true;
        _playerAnimator.Swing();
        _playerAnimator.SetFPS(4f);
    }

    private void OnShootActionCancelled(InputAction.CallbackContext ctx)
    {
        if (_shotProgressBar.currentFill > 0.1f)
        {
            _shooter.Shoot(_shotProgressBar.currentFill);
            _playerAnimator.Shoot();
            _playerAudio.Shoot();
        }
        else
        {
            _playerAnimator.Walk();
        }
        _shotProgressBar.StopAndReset();

        _isSwinging = false;
    }
}
