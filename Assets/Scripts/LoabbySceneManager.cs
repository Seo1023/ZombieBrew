using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoabbySceneManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneTransitionManager.Instance.DoLoadSceneWithLoadingScene("LoadingScene", "MainScene");
    }
}
