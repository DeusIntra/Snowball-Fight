using System;
using System.Collections;
using UnityEngine;

public class LevelEnd : MonoBehaviour
{
    public float waitTimeBeforeWin = 1f;
    public float waitTimeBeforeLose = 1f;
    public int spinsAmount = 5;
    public float spinTime = 1f;
    public float jumpHeight = 0.5f;
    public GameObject winPanel;
    public GameObject losePanel;
    public EnablerDisabler disableOnEndGame;
    public GameParametersSingleton parameters;

    private Transform _player;

    private void Start()
    {
        _player = FindObjectOfType<Player>().transform;
    }

    public void Win()
    {        
        StartCoroutine(WinCoroutine());
    }

    public void LoseOnZeroHealth()
    {
        if (!_player.GetComponent<Health>().isAlive)
            StartCoroutine(LoseCoroutine());
    }

    private IEnumerator WinCoroutine()
    {
        DisableUI();

        PlayerMover mover = _player.GetComponent<PlayerMover>();
        mover.enabled = false;

        PlayerAnimator playerAnimator = _player.GetComponent<PlayerAnimator>();
        playerAnimator.SetSpeed(0);

        DestroySnowballs();

        yield return new WaitForSecondsRealtime(waitTimeBeforeWin);

        // spin player and jump
        yield return SpinAndJump(spinsAmount, spinTime, jumpHeight);

        // show score
        // TODO: animations and score
        winPanel.SetActive(true);

        yield return null;

        SaveProgress();
    }

    private IEnumerator LoseCoroutine()
    {
        DisableUI();

        PlayerMover mover = _player.GetComponent<PlayerMover>();
        mover.enabled = false;

        Time.timeScale = 0f;

        DestroySnowballs();

        yield return SpinAndJump(spinsAmount, waitTimeBeforeLose, 0);

        // animate death
        PlayerAnimator playerAnimator = _player.GetComponent<PlayerAnimator>();
        playerAnimator.Die();
        _player.GetComponent<Player>().PlayDeathSound();

        // show score
        // TODO: animations and score
        losePanel.SetActive(true);

        yield return null;

        SaveProgress();
    }

    private void SaveProgress()
    {
        var levelsOpened = parameters.openedLevelsOnLocation;
        if (levelsOpened.Count < parameters.currentLevelIndex)
            levelsOpened.Add(1);
        else
            levelsOpened[parameters.currentLevelIndex - 1] += 1;
        parameters.Save();
    }

    private void DisableUI()
    {
        disableOnEndGame.DisableObjects();
    }

    private void DestroySnowballs()
    {
        Snowball[] snowballs = FindObjectsOfType<Snowball>();
        foreach (Snowball snowball in snowballs)
        {
            snowball.Break();
        }
    }

    private IEnumerator SpinAndJump(int spinsAmount, float spinTime, float jumpHeight)
    {
        float t = 0;

        float playerStartRotationY = _player.rotation.y;
        float playerEndRotationY = playerStartRotationY + 360 * spinsAmount + 180;
        Vector3 rot = _player.rotation.eulerAngles;
        Vector3 pos = _player.position;

        while (t < 1f)
        {
            float rotationY = Mathf.Lerp(playerStartRotationY, playerEndRotationY, t);
            float sqrtHeight = Mathf.Sqrt(jumpHeight);
            float positionY = Mathf.Lerp(-sqrtHeight, sqrtHeight, t);

            _player.rotation = Quaternion.Euler(new Vector3(rot.x, rotationY, rot.z));
            _player.position = new Vector3(pos.x, jumpHeight - Mathf.Pow(positionY, 2), pos.z);

            t += Time.unscaledDeltaTime / spinTime;
            yield return null;
        }
        _player.rotation = Quaternion.Euler(new Vector3(rot.x, playerEndRotationY, rot.z));
        _player.position = pos;
    }
}
