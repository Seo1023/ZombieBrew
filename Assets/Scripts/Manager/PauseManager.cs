using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject pauseMenuUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void GoToSelectScene()
    {
        Time.timeScale = 1f; // �� �̵� �� �ݵ�� �簳
        SceneManager.LoadScene("SelectScene"); // �� �̸��� �°� ����
    }

    public void OpenSettings()
    {
        Debug.Log("����â ���� (�̱���)");
    }

}
