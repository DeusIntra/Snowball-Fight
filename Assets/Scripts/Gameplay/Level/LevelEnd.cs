using System.Collections;
using UnityEngine;

public class LevelEnd : MonoBehaviour
{
    public float waitTimeBeforeWin = 1f;
    public int spinsAmount = 5;
    public float spinTime = 1f;
    public float jumpHeight = 0.5f;
    public GameObject winPanel;

    private Transform _player;

    private void Start()
    {
        _player = FindObjectOfType<Player>().transform;
    }

    public void Win()
    {
        StartCoroutine(WinCoroutine());
    }

    public void Lose()
    {
        StartCoroutine(LoseCoroutine());
    }

    private IEnumerator WinCoroutine()
    {
        // disable all ui except win panel
        Debug.Log("TODO this");

        // wait
        while (waitTimeBeforeWin > 0)
        {
            waitTimeBeforeWin -= Time.deltaTime;
            yield return null;
        }

        // destroy snowballs
        Snowball[] snowballs = FindObjectsOfType<Snowball>();
        foreach (Snowball snowball in snowballs)
        {
            snowball.Break();
        }

        // spin player + jump
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
        winPanel.SetActive(true);
        yield return null;
    }

    private IEnumerator LoseCoroutine()
    {
        yield return null;
    }
}
