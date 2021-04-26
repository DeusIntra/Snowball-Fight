using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class LevelEnd : MonoBehaviour
{
    public int onWinGoldAmount;

    public float waitTimeBeforeWin = 1f;
    public float waitTimeBeforeLose = 1f;
    public int spinsAmount = 5;
    public float spinTime = 1f;
    public float jumpHeight = 0.5f;
    [HideInInspector] public int enemyCount;
    public GameObject winPanel;
    public GameObject losePanel;
    public TextMeshProUGUI winPanelMoneyText;
    public TextMeshProUGUI losePanelMoneyText;
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
        winPanelMoneyText.text = $"you earned ${onWinGoldAmount}";

        yield return null;

        Debug.Log("TODO: open location after finishing level 6");

        parameters.goldAmount += onWinGoldAmount;

        OpenLevel();
        parameters.Save();
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
        int onLoseGoldAmount = enemyCount - GetComponent<EnemyHolder>().enemies.Count;

        losePanel.SetActive(true);
        losePanelMoneyText.text = $"you earned ${onLoseGoldAmount}";

        parameters.goldAmount += onLoseGoldAmount;
        parameters.Save();
    }

    private void OpenLevel()
    {
        int location = parameters.currentLocationIndex;
        int level = parameters.currentLevelIndex;

        parameters.finishedLevelsOnLocation[location] = level + 1;
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
