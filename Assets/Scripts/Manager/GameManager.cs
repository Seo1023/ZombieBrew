using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Game State")]
    public int killCount = 0;
    public float timeRemaining = 600f;
    public bool isGameOver = false;

    [Header("References")]
    public TextMeshProUGUI killText;
    public TextMeshProUGUI timerText;
    public GameObject gameOverUI;
    public Transform player;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        if (gameOverUI != null)
            gameOverUI.SetActive(false);
    }

    void Update()
    {
        if (isGameOver) return;

        timeRemaining -= Time.deltaTime;
        timeRemaining = Mathf.Max(0, timeRemaining);
        UpdateTimerUI();

        if (timeRemaining <= 0)
            EndGame();
    }

    public void AddKill()
    {
        killCount++;
        if (killText != null)
            killText.text = $"잡은 좀비 수: {killCount} 마리";
    }

    void UpdateTimerUI()
    {
        if (timerText == null) return;

        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = $"{minutes:00}:{seconds:00}";
    }

    public void EndGame()
    {
        isGameOver = true;
        if (gameOverUI != null)
            gameOverUI.SetActive(true);

        Time.timeScale = 0f; // 정지
        Debug.Log("게임 종료됨");
    }
}
