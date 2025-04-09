using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // 로드할 씬의 이름 또는 번호
    public string NightScene;

    // 버튼에 연결할 메서드
    public void LoadScene()
    {
        if (!string.IsNullOrEmpty(NightScene))
        {
            SceneManager.LoadScene("NightScene");
        }
        else
        {
            Debug.LogWarning("Scene name is not set.");
        }
    }
}
