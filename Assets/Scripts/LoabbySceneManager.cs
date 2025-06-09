using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoabbySceneManager : MonoBehaviour
{
    public void GameSelect()
    {
        Debug.Log("GameSelect Button Clicked");

        if (SceneTransitionManager.Instance == null)
        {
            Debug.LogError("SceneTransitionManager.Instance is NULL!!");
            return;
        }

        Debug.Log("SceneTransitionManager.Instance: " + SceneTransitionManager.Instance);

        SceneTransitionManager.Instance.LoadSceneWithLoading("SelectScene");
    }

    public void GameLobby()
    {
        SceneTransitionManager.Instance.LoadSceneWithLoading("LobbyScene");
    }

    public void GameStart()
    {
        SceneTransitionManager.Instance.LoadSceneWithLoading("Cafe");
    }
}
