﻿using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerShooter))]
[RequireComponent(typeof(PlayerMover))]
[RequireComponent(typeof(PlayerAnimator))]
[RequireComponent(typeof(Health))]
public class Player : MonoBehaviour
{
    public ProgressBar healthBar;

    private bool _isSwinging = false;

    private PlayerShooter _shooter;
    private PlayerMover _mover;
    private PlayerAnimator _playerAnimator;
    private ProgressBarTimed _shootProgressBar;
    private Health _health;

    private void Awake()
    {
        _shooter = GetComponent<PlayerShooter>();
        _playerAnimator = GetComponent<PlayerAnimator>();
        _mover = GetComponent<PlayerMover>();
        _shootProgressBar = _shooter.progressBar;
        _health = GetComponent<Health>();
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

    public void FillHealthBar()
    {
        healthBar.SetFill(_health.currentFraction);
    }

    private void Die()
    {
        _shooter.enabled = false;
        _mover.enabled = false;
    }
}
