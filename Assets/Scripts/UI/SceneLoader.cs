using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // �ε��� ���� �̸� �Ǵ� ��ȣ
    public string NightScene;

    // ��ư�� ������ �޼���
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
