using UnityEngine;

public class MeshAnimations : MonoBehaviour
{
    public AnimatedMeshSequence walk;
    public AnimatedMeshSequence swing;
    public AnimatedMeshSequence throwing;
    public AnimatedMeshSequence death;

    public void StopAll()
    {
        walk.Stop();
        swing.Stop();
        throwing.Stop();
        death.Stop();
    }
}
