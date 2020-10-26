using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerShooter))]
[RequireComponent(typeof(PlayerMover))]
[RequireComponent(typeof(PlayerAnimator))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Mana))]
public class Player : MonoBehaviour
{
    public ProgressBar healthBar;
    public ProgressBar spellBar;

    private bool _isSwinging = false;

    private PlayerShooter _shooter;
    private PlayerMover _mover;
    private PlayerAnimator _playerAnimator;
    private ProgressBarTimed _shootProgressBar;
    private Health _health;
    private Mana _mana;

    private void Awake()
    {
        _shooter = GetComponent<PlayerShooter>();
        _playerAnimator = GetComponent<PlayerAnimator>();
        _mover = GetComponent<PlayerMover>();
        _shootProgressBar = _shooter.progressBar;
        _health = GetComponent<Health>();
        _mana = GetComponent<Mana>();
    }

    private void Update()
    {
        if (_isSwinging)
        {
            float FPS = Mathf.Lerp(4, 10, _shootProgressBar.currentFill);
            _playerAnimator.SetFPS(FPS);
        }
    }

    public void ReadShootInput(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.phase == InputActionPhase.Started)
        {
            _shootProgressBar.StartFilling();
            _isSwinging = true;
            _playerAnimator.Swing();
            _playerAnimator.SetFPS(4f);
        }

        if (callbackContext.phase == InputActionPhase.Canceled)
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
        }
    }

    public void FillHealthBar()
    {
        healthBar.SetFill(_health.currentFraction);
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

        _shootProgressBar.enabled = false;
    }
}
