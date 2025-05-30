using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("UI Components")]
    public TextMeshProUGUI killText;
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI expText;
    public TextMeshProUGUI levelText;
    public Slider expBar;
    public TextMeshProUGUI ammoText;
    public GameObject reloadingUI;
    public TextMeshProUGUI timeText;

    [Header("Game Over UI")]
    public GameObject gameOverUI;
    public GameObject clearUI;
    public GameObject overUI;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        if(GameManager.Instance != null)
        {
            GameManager.Instance.IsTimerActive = true;
        }
        killText.text = "잡은 좀비 수: 0";
        goldText.text = "GOLD : 0";
    }

    public void SetKillCount(int value)
    {
        if (killText != null)
            killText.text = $"잡은 좀비 수: {value}";
    }

    public void SetGold(int value)
    {
        if (goldText != null)
            goldText.text = $"GOLD : {value}";
    }

    public void SetExp(int current, int max)
    {
        if (expBar != null)
        {
            expBar.maxValue = max;
            expBar.value = current;
        }
        if (expText != null)
            expText.text = $"{current} / {max}";
    }

    public void SetLevel(int level)
    {
        if (levelText != null)
            levelText.text = $"{level}";
    }

    public void SetAmmo(int current, int max)
    {
        if (ammoText != null)
            ammoText.text = $"{current} / {max}";
    }

    public void SetReloading(bool isReloading)
    {
        if (reloadingUI != null)
            reloadingUI.SetActive(isReloading);
    }

    public void SetTime(float time)
    {
        if (timeText != null)
        {
            int min = Mathf.FloorToInt(time / 60);
            int sec = Mathf.FloorToInt(time % 60);
            timeText.text = $"{min:D2}:{sec:D2}";
        }
    }

    public void ShowGameOverUI(bool isClear)
    {
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
            if (clearUI != null && overUI != null)
                if (isClear)
                {
                    clearUI.SetActive(true);
                    overUI.SetActive(false);
                }
                else
                {
                    overUI.SetActive(true);
                    clearUI.SetActive(false);
                }
        }
    }
}

