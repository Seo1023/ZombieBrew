using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public string sceneName;
    
   
    public void OnClickSelectMap()
    {
        if(GameManager.Instance.selectedCharacter == null)
        {
            Debug.LogWarning("ĳ���͸� �������ּ���.");
            return;
        }
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    public void SelectSceneChange()
    {
        SceneManager.LoadScene("SelectScene");
    }

    public void LobbySceneChange()
    {
        SceneManager.LoadScene("LobbyScene");
    }
}
