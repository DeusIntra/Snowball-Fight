using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public Vector3 rotation;
    public float randomOffset = 0f;

    private void Start()
    {
        rotation.x = rotation.x + Random.Range(-randomOffset, randomOffset);
        rotation.y = rotation.y + Random.Range(-randomOffset, randomOffset);
        rotation.z = rotation.z + Random.Range(-randomOffset, randomOffset);
    }

    void Update()
    {
        transform.Rotate(rotation * Time.deltaTime);
    }
}
