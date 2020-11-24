using UnityEngine;
using System.Collections;

public class AnimatedMeshSequence : MonoBehaviour
{
    public float framesPerSecond = 12f;
    public bool playAtStart = false;
    public Mesh[] meshes;

    protected MeshFilter meshFilter;
    protected int position;

    private Coroutine coroutine;
    
    public bool IsPlaying { get; private set; }


    private void Awake()
    {
        IsPlaying = false;        
        if (!IsReady()) return;

        if (playAtStart)
            Play();
    }


    public void SetMeshFilter(MeshFilter meshFilter)
    {
        this.meshFilter = meshFilter;
    }


    public void Play()
    {
        if (!IsReady()) return;

        if (!IsPlaying)
        {
            IsPlaying = true;
            coroutine = StartCoroutine(AnimateMesh());
        }
    }


    public void Stop()
    {
        if (!IsReady()) return;

        if (IsPlaying)
        {
            IsPlaying = false;
            if (coroutine != null) StopCoroutine(coroutine);
        }
    }


    private IEnumerator AnimateMesh()
    {
        while (IsPlaying)
        {
            float waitTime = 1f/framesPerSecond;
            IncrementPlayhead();
            meshFilter.mesh = meshes[position];
            yield return new WaitForSeconds(waitTime);
        }
    }


    private void IncrementPlayhead()
    {
        position++;
        if (position > meshes.Length - 1)
            position = 0;
    }


    private bool IsReady()
    {
        if (meshes.Length > 0)
            return true;
        Debug.LogError("No Meshes have been added to the AnimatedMeshSequence");
        return false;
    }
}
