using System.Collections;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float secondsToChangeMaterial = 0.5f;

    public Material defaultMaterial;
    public Material hitMaterial;

    private Renderer _renderer;
    private Coroutine _coroutine;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Snowball"))
        {
            _renderer.material = hitMaterial;
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }
            _coroutine = StartCoroutine(ChangeMaterial());
        }

        Destroy(collision.gameObject);
    }

    private IEnumerator ChangeMaterial()
    {
        yield return new WaitForSecondsRealtime(secondsToChangeMaterial);
        _renderer.material = defaultMaterial;
    }
}
