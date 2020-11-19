using System.Collections;
using UnityEngine;

public class SnowballSpawner : MonoBehaviour
{
    public float baseForce;
    public float forceSpread;
    public float positionSpread = 5f;

    public GameObject snowballPrefab;


    public void Cast(float durationSeconds)
    {
        StartCoroutine(CastCoroutine(durationSeconds));
    }


    public IEnumerator CastCoroutine(float durationSeconds)
    {
        while (durationSeconds > 0)
        {
            GameObject snowball = Instantiate(snowballPrefab, transform.position, transform.rotation);
            
            float randomPositionX = Random.Range(-positionSpread, positionSpread);
            snowball.transform.position += new Vector3(randomPositionX, 0, 0);

            float randomForce = Random.Range(-forceSpread, forceSpread) + baseForce;
            Rigidbody snowballRB = snowball.GetComponent<Rigidbody>();
            snowballRB.AddForce(Vector3.forward * randomForce);

            durationSeconds -= Time.deltaTime;
            yield return null;
        }

        yield break;
    }
}
