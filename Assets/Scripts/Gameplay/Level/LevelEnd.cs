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

    public AudioSource musicSource;
    public AudioClip winMusic;
    public AudioClip loseMusic;

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
        musicSource.Stop();
        musicSource.clip = winMusic;
        musicSource.loop = false;

        DisableUI();

        PlayerMover mover = _player.GetComponent<PlayerMover>();
        mover.enabled = false;

        PlayerAnimator playerAnimator = _player.GetComponent<PlayerAnimator>();
        playerAnimator.SetSpeed(0);

        DestroySnowballs();

        yield return new WaitForSecondsRealtime(waitTimeBeforeWin);

        // spin player and jump
        yield return SpinAndJump(spinsAmount, spinTime, jumpHeight);

        musicSource.Play();

        // show score
        // TODO: animations and score
        winPanel.SetActive(true);
        winPanelMoneyText.text = $"Earned ${onWinGoldAmount}";

        yield return null;

        int lastLocationFinished = parameters.finishedLevelsOnLocation[parameters.finishedLevelsOnLocation.Count - 1];
        if (lastLocationFinished >= 10)
        {
            parameters.finishedLevelsOnLocation.Add(0);
        }

        parameters.goldAmount += onWinGoldAmount;

        OpenLevel();
        parameters.Save();
    }

    private IEnumerator LoseCoroutine()
    {
        musicSource.Stop();
        musicSource.clip = loseMusic;
        musicSource.loop = false;

        DisableUI();

        PlayerMover mover = _player.GetComponent<PlayerMover>();
        mover.enabled = false;

        Time.timeScale = 0f;

        DestroySnowballs();

        yield return SpinAndJump(spinsAmount, waitTimeBeforeLose, 0);

        musicSource.Play();

        // animate death
        PlayerAnimator playerAnimator = _player.GetComponent<PlayerAnimator>();
        playerAnimator.Die();
        _player.GetComponent<Player>().PlayDeathSound();

        // show score
        // TODO: animations and score
        int onLoseGoldAmount = enemyCount - GetComponent<EnemyHolder>().enemies.Count;

        losePanel.SetActive(true);
        losePanelMoneyText.text = $"Earned ${onLoseGoldAmount}";

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
