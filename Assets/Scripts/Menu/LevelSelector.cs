using UnityEngine;
using UnityEngine.InputSystem;

public class LevelSelector : MonoBehaviour
{
    private Vector2 _mousePos;

    public void ReadPosition(InputAction.CallbackContext callbackContext)
    {
        _mousePos = callbackContext.ReadValue<Vector2>();
    }

    public void SelectLevel(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.phase != InputActionPhase.Started) return;

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(_mousePos);
        if (Physics.Raycast(ray, out hit, 100.0f))
        {
            LevelLoader levelLoader = hit.transform.parent.GetComponent<LevelLoader>();

            if (levelLoader != null)
            {
                levelLoader.LoadLevel();
            }
        }
    }
}
