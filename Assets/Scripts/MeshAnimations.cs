using UnityEngine;

public class MeshAnimations : MonoBehaviour
{
    public AnimatedMeshSequence walk;
    public AnimatedMeshSequence swing;
    public AnimatedMeshSequence throwing;
    public AnimatedMeshSequence death;

    public void StopAll()
    {
        if (walk != null) walk.Stop();
        if (swing != null) swing.Stop();
        if (throwing != null) throwing.Stop();
        if (death != null) death.Stop();
    }
}
