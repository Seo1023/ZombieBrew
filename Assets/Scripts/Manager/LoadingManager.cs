using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{
    public Slider loadingBar;

    void Start()
    {
        string nextScene = SceneTransitionManager.Instance.GetNextSceneName();
        Debug.Log("Next Scene to load: " + nextScene);
        if (string.IsNullOrEmpty(nextScene))
        {
            Debug.LogError("Next scene name is null or empty!");
            return;
        }

        StartCoroutine(LoadAsync(nextScene));
    }

    IEnumerator LoadAsync(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;

        Debug.Log("Start loading: " + sceneName);

        while (!operation.isDone)
        {
            Debug.Log($"Loading progress: {operation.progress}");

            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            if (loadingBar != null)
                loadingBar.value = progress;

            if (operation.progress >= 0.9f)
            {
                Debug.Log("Almost done loading, waiting 0.5s then allow activation.");
                loadingBar.value = 1f;
                yield return new WaitForSecondsRealtime(0.5f);
                operation.allowSceneActivation = true;
                Debug.Log("Scene activation allowed.");
            }

            yield return null;
        }

    }
}
