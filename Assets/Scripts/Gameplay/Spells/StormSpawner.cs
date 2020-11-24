using System.Collections;
using UnityEngine;

public class StormSpawner : MonoBehaviour
{
    public GameObject wind;
    public GameObject stormParticlesPrefab;


    public void Cast(float seconds)
    {
        StartCoroutine(CastCoroutine(seconds));
    }


    private IEnumerator CastCoroutine(float seconds)
    {
        wind.SetActive(true);
        GameObject storm = Instantiate(stormParticlesPrefab, transform.position, transform.rotation);

        yield return new WaitForSeconds(seconds);

        wind.SetActive(false);
        Destroy(storm, 10f);
    }
}
