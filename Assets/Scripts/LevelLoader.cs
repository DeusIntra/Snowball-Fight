using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public string sceneName;
    
    public void LoadLevel(InputAction.CallbackContext callbackContext)
    {
        Debug.Log(sceneName);
    }
}
