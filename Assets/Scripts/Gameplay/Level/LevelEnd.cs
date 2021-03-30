using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnd : MonoBehaviour
{
    public float waitTimeBeforeWin = 1f;
    public int spinsAmount = 5;
    public float spinTime = 1f;
    public float jumpHeight = 0.5f;
    public GameObject winPanel;

    private Player _player;

    private void Start()
    {
        _player = FindObjectOfType<Player>();
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

        float playerStartRotationY = _player.transform.rotation.y;
        float rotationX = _player.transform.rotation.x;
        float rotationZ = _player.transform.rotation.z;

        float playerStartPositionY = _player.transform.position.y;

        float playerEndRotationY = playerStartPositionY + 360 * spinsAmount + 180;

        while (t < 1f)
        {
            float rotationY = Mathf.Lerp(playerStartRotationY, playerEndRotationY, t);
            // TODO: jump

            _player.transform.rotation = Quaternion.Euler(new Vector3(rotationX, rotationY, rotationZ));

            t += Time.deltaTime / spinTime;
            yield return null;
        }
        _player.transform.rotation = Quaternion.Euler(new Vector3(rotationX, playerEndRotationY, rotationZ));
        // TODO: jump

        // show score
        winPanel.SetActive(true);
        yield return null;
    }

    private IEnumerator LoseCoroutine()
    {
        yield return null;
    }
}
