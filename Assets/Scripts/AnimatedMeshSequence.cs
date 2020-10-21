using UnityEngine;
using System.Collections;

public class AnimatedMeshSequence : MonoBehaviour
{
    public float FramesPerSecond = 12f;
    public bool PlayAtStart = false;
    public Mesh[] Meshes;
    protected MeshFilter MyMeshFilter;
    protected int Position;

    private Coroutine coroutine;
    
    public bool IsPlaying { get; private set; }

    private void Awake()
    {
        MyMeshFilter = transform.parent.GetComponent<MeshFilter>();
        IsPlaying = false;

        if (!IsReady()) return;
        MyMeshFilter.mesh = Meshes[0];

        if (PlayAtStart)
            Play();
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
            float waitTime = 1f/FramesPerSecond;
            IncrementPlayhead();
            MyMeshFilter.mesh = Meshes[Position];
            yield return new WaitForSeconds(waitTime);
        }
    }

    private void IncrementPlayhead()
    {
        Position++;
        if (Position > Meshes.Length - 1)
            Position = 0;
    }

    private bool IsReady()
    {
        if (Meshes.Length > 0)
            return true;
        Debug.LogError("No Meshes have been added to the AnimatedMeshSequence");
        return false;
    }
}
