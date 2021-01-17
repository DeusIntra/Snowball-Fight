using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Rigidbody))]
public class Snowball : MonoBehaviour
{
    public float lifetime = 2f;
    public int damage = 1;
    public float destructionChance = 0.1f;
    
    public ParticleSystem particlesPrefab;
    public AudioClip breakSound;

    private AudioSource _audioSource;
    private MeshRenderer _meshRenderer;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _meshRenderer = GetComponent<MeshRenderer>();
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter(Collider otherCollider)
    {
        Snowball other = otherCollider.GetComponent<Snowball>();
        if (other != null)
        {
            float p = 1 - Mathf.Sqrt(1 - destructionChance);
            if (Random.Range(0f, 1f) < p)
            {
                other.Break();
                Break();
            }
        }
        else
        {
            Break();
        }
    }

    public void Break()
    {
        ParticleSystem particles = Instantiate(particlesPrefab, transform.position, particlesPrefab.transform.rotation);
        Destroy(particles.gameObject, 0.5f);

        _audioSource.clip = breakSound;
        _audioSource.Play();

        _meshRenderer.enabled = false;

        GetComponent<Collider>().enabled = false;

        Destroy(gameObject, 1f);
    }
}
