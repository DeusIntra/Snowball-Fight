using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public AudioClip start;
    public AudioClip loop;
    public AudioClip ending;

    public float correction;

    public AudioSource source1;
    public AudioSource source2;

    //private bool _playing = false;

    private void Update()
    {
        //if (!_playing) return;

        //if (source1.clip != null)
        //{
        //    if (!source1.isPlaying)
        //    {
        //        source1.clip = loop;
        //        source1.Play();

        //        source2.clip = ending;
        //        source2.Play();
        //    }
        //}
        //else
        //{
        //    source1.clip = start;
        //    source1.Play();
        //}
    }

    public void Play()
    {
        StartCoroutine(PlayCoroutine());
        //source1.clip = loop;
        //source1.Play();
    }

    private IEnumerator PlayCoroutine()
    {
        //float startLength = start.length;
        float loopLength = loop.length;

        source1.clip = loop;
        source1.loop = false;
        //source2.clip = start;
        //source2.PlayDelayed(0.001f);
        //yield return new WaitForSecondsRealtime(startLength);
        //source2.clip = ending;
        source1.PlayOneShot(loop);

        while(true)
        {
            source1.PlayOneShot(loop);
            //source2.PlayDelayed(0.01f);
            Debug.Log(correction);
            yield return new WaitForSecondsRealtime(loopLength + correction);
        }
    }

}
