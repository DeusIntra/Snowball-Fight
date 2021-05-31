using UnityEngine;

/// <summary>
/// Inherit from this base class to create a singleton.
/// e.g. public class MyClassName : Singleton<MyClassName> {}
/// </summary>
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    // Check to see if we're about to be destroyed.
    private static bool _shuttingDown = false;
    private static object _lock = new object();
    private static T _instance;

    /// <summary>
    /// Access singleton instance through this propriety.
    /// </summary>
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                // Search for existing instance.
                _instance = (T)FindObjectOfType(typeof(T));

                // Create new instance if one doesn't already exist.
                if (_instance == null)
                {
                    var singletonObject = new GameObject();
                    _instance = singletonObject.AddComponent<T>();
                    singletonObject.name = typeof(T).ToString() + " (Singleton)";

                    // Make instance persistent.
                    //DontDestroyOnLoad(singletonObject);
                }
            }

            return _instance;

        }
    }
}