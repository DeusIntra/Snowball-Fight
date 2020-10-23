using UnityEngine;

public class ApplicationTargetFPS : MonoBehaviour
{
    void Start()
    {
        Application.targetFrameRate = 60;
    }
}
