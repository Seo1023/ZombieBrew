using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager Instance;

    public GameObject loadingScreen;
    public Slider loadingBar;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        if (loadingScreen == null)
        {
            Debug.LogWarning("Loading Screen is not assigned in the inspector.");
        }
    }

    public void DoLoadSceneWithLoadingScene(string loadingScene, string targetScene)
    {
        StartCoroutine(LoadSceneWithLoadingScreen(loadingScene, targetScene));
    }

    private IEnumerator LoadSceneWithLoadingScreen(string loadingScene, string targetScene)
    {
        yield return SceneManager.LoadSceneAsync(loadingScene);

        if (loadingScreen != null)
        {
            loadingScreen.SetActive(true);
        }

        AsyncOperation operation = SceneManager.LoadSceneAsync(targetScene);
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            if (loadingBar != null)
            {
                loadingBar.value = progress;
            }

            if (operation.progress >= 0.9f)
            {
                loadingBar.value = 1.0f;
                yield return new WaitForSeconds(0.5f);
                operation.allowSceneActivation = true;
            }

            yield return null;
        }

        if (loadingScreen == null)
        {
            loadingScreen.SetActive(false);
        }
    }

    public void LoadLobby()
    {
        SceneManager.LoadScene("LobbyScene");
    }
}
