﻿using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Snowball : MonoBehaviour
{
    public float lifetime = 2f;
    public int damage = 1;
    public float destructionChance = 0.1f;
    public bool canBreak = true;
    
    public ParticleSystem particlesPrefab;
    public AudioClip breakSound;

    private bool isBroken = false;
    private AudioSource _audioSource;
    private MeshRenderer _meshRenderer;
    private IDebuffEffect _debuffEffect;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _debuffEffect = GetComponent<IDebuffEffect>();
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter(Collider otherCollider)
    {
        Snowball otherSnowball = otherCollider.GetComponent<Snowball>();
        if (otherSnowball != null)
        {
            if (canBreak == false) otherSnowball.Break();
            else
            {
                float p = 1 - Mathf.Sqrt(1 - destructionChance);
                if (Random.Range(0f, 1f) < p)
                {
                    otherSnowball.Break();
                    Break();
                }
            }
        }
        else
        {
            if (_debuffEffect != null)
            {
                _debuffEffect.Debuff(otherCollider.gameObject);
            }

            Break();
        }
    }

    public void Break()
    {
        if (isBroken) return;
        isBroken = true;

        Instantiate(particlesPrefab, transform.position, particlesPrefab.transform.rotation);

        if (_audioSource != null)
        {
            _audioSource.clip = breakSound;
            _audioSource.Play();
        }

        if (_meshRenderer != null)
        {
            _meshRenderer.enabled = false;
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }

        GetComponent<Collider>().enabled = false;

        Destroy(gameObject, 0.5f);
    }
}
