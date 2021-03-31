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

        DestroySnowballs();

        yield return new WaitForSecondsRealtime(waitTimeBeforeWin);

        // spin player and jump
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

            t += Time.deltaTime / spinTime;
            yield return null;
        }
        _player.rotation = Quaternion.Euler(new Vector3(rot.x, playerEndRotationY, rot.z));
        _player.position = pos;

        // show score
        // TODO: animations and score
        winPanel.SetActive(true);
        yield return null;
    }

    private IEnumerator LoseCoroutine()
    {
        DisableUI();

        Time.timeScale = 0f;

        DestroySnowballs();

        yield return new WaitForSecondsRealtime(waitTimeBeforeLose);

        // animate death
        PlayerAnimator playerAnimator = _player.GetComponent<PlayerAnimator>();
        playerAnimator.Die();
        _player.GetComponent<Player>().PlayDeathSound();

        // show score

        yield return null;
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
}
