using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneLoader : MonoBehaviour
{
    public void LoadSceneByName(string MainScene)
    {
        SceneManager.LoadScene(MainScene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
