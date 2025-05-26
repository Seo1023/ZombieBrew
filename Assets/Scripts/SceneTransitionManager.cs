using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager Instance;

    //public GameObject loadingScreen;
    public Slider loadingBar;

    private string nextSceneName;

    private void Awake()
    {
        Debug.Log("SceneTransitionManager Awake. Instance: " + Instance);
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.LogWarning("Duplicate SceneTransitionManager detected! Destroying this one.");
            Destroy(gameObject);
        }
    }

    public void LoadSceneWithLoading(string targetSceneName)
    {
        nextSceneName = targetSceneName;
        SceneManager.LoadScene("LoadingScene"); 
    }

    public string GetNextSceneName()
    {
        return nextSceneName;
    }

    /*private IEnumerator LoadSceneWithLoadingScreen(string loadingScene, string targetScene)
    {
        yield return SceneManager.LoadSceneAsync(loadingScene);

        *//*if (loadingScreen != null)
        {
            loadingScreen.SetActive(true);
        }*//*

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

        *//*if (loadingScreen == null)
        {
            loadingScreen.SetActive(false);
        }*//*
    }*/

    
}