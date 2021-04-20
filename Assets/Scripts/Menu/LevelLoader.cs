using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public string levelName;
    public ProgressBar progressBar;

    public void LoadLevel()
    {
        StartCoroutine(LoadingCoroutine());
    }

    private IEnumerator LoadingCoroutine()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(levelName);

        operation.allowSceneActivation = false;

        float progress = 0f;

        while (!operation.isDone)
        {
            progress = operation.progress / 0.9f;
            if (progressBar != null) progressBar.SetFill(progress);
            yield return null;

            if (progress >= 1f)
            {
                operation.completed += (AsyncOperation a) => Time.timeScale = 1f;
                operation.allowSceneActivation = true;
            }
        }
    }
}
