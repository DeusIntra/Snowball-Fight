using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerShooter))]
[RequireComponent(typeof(PlayerMover))]
[RequireComponent(typeof(PlayerAnimator))]
public class Player : MonoBehaviour
{
    private bool _isSwinging = false;

    private PlayerShooter _shooter;
    private PlayerMover _mover;
    private PlayerAnimator _playerAnimator;
    private ProgressBar _shootProgressBar;    

    private void Awake()
    {
        _shooter = GetComponent<PlayerShooter>();
        _playerAnimator = GetComponent<PlayerAnimator>();
        _mover = GetComponent<PlayerMover>();
        _shootProgressBar = _shooter.progressBar;
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
            _shooter.Shoot();
            _isSwinging = false;
            _playerAnimator.Throw();
        }
    }

    private void Die()
    {
        _shooter.enabled = false;
        _mover.enabled = false;
    }
}
