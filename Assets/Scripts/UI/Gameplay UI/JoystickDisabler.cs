using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickDisabler : MonoBehaviour
{
    private PlayerMover _playerMover;

    private void Awake()
    {
        _playerMover = FindObjectOfType<PlayerMover>();
    }
    private void OnEnable()
    {
        _playerMover.enabled = true;
    }

    private void OnDisable()
    {
        _playerMover.enabled = false;
    }

}
