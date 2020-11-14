using UnityEngine;

public class InventoryButton : MonoBehaviour
{
    public float slowMotion = 0.05f;

    [HideInInspector] public bool isOpen = false;

    public void OnPress()
    {
        isOpen = !isOpen;

        if (isOpen)
        {
            Time.timeScale = slowMotion;
            Time.fixedDeltaTime = slowMotion * 0.02f;
        }
        else
        {
            Time.timeScale = 1f;
            Time.fixedDeltaTime = 0.02f;
        }
    }
}
