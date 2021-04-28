using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Logos : MonoBehaviour
{
    public float time = 4f;

    public List<Sprite> logos;

    public GameParametersSingleton parameters;

    private void Start()
    {
        if (parameters.showTitle) StartCoroutine(ShowLogosCoroutine());
    }

    private IEnumerator ShowLogosCoroutine()
    {
        Image image = GetComponent<Image>();
        float t;
        foreach (var logo in logos)
        {
            image.sprite = logo;
            t = 0;
            float tPong;
            while (true)
            {
                if (t >= 2 * time) break;

                tPong = Mathf.PingPong(t, time);

                float x = Mathf.Lerp(0, 1, tPong / time);
                image.color = new Color(x, x, x, 1);

                t += Time.deltaTime;
                yield return null;
            }
        }
        
        t = 0;
        while (true)
        {
            if (t >= 1) break;

            float x = Mathf.Lerp(0, 1, t / time);
            image.color = new Color(1, 1, 1, x);

            t += Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }
}
