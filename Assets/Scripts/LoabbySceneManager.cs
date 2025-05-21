using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoabbySceneManager : MonoBehaviour
{
    public void GameSelect()
    {
        Debug.Log("GameSelect Button Clicked");
        Debug.Log("SceneTransitionManager.Instance: " + SceneTransitionManager.Instance);
        SceneTransitionManager.Instance.DoLoadSceneWithLoadingScene("LoadingScene", "SelectScene");
    }

    public void GameLobby()
    {
        SceneTransitionManager.Instance.DoLoadSceneWithLoadingScene("LoadingScene", "LobbyScene");
    }

    public void GameStart()
    {
        SceneTransitionManager.Instance.DoLoadSceneWithLoadingScene("LoadingScene", "MainScene");
    }
}
