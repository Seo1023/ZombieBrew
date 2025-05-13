using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Game State UI")]
    public TextMeshProUGUI gameText;
    public GameObject gameOverUI;

    [Header("PlayerUI")]
    public TextMeshProUGUI killText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI ammoText;
    public GameObject reloadTextObject;

    [Header("EXP & Gold UI")]
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI expText;
    public Slider expBar;
    public TextMeshProUGUI levelText;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        if(gameOverUI != null)
        {
            gameOverUI.SetActive(false);
        }
    }

    public void SetKillCount(int count)
    {
        if(killText != null)
        {
            killText.text = $"잡은 좀비 수 : {count} 마리";
        }
    }

    public void SetTime(float time)
    {
        if(timerText != null)
        {
            int minutes = Mathf.FloorToInt(time / 60);
            int seconds = Mathf.FloorToInt(time % 60);
            timerText.text = $"{minutes:D2} : {seconds:D2}";
        }
    }

    public void SetGold(int gold)
    {
        if (goldText != null)
            goldText.text = $"Gold : {gold}";
    }
    
    public void SetExp(int exp, int maxExp)
    {
        if (expText != null)
            expText.text = $"EXP : {exp} / {maxExp}";
        if (expBar != null)
            expBar.value = (float)exp / maxExp;
    }

    public void SetLevel(int level)
    {
        if(levelText != null)
        {
            if(levelText != null)
            {
                levelText.text = $"{level}";
            }
        }
    }

    public void ShowGameOverUI()
    {
        if (gameOverUI != null)
            gameOverUI.SetActive(true);
    }

    public void SetAmmo(int ammo)
    {
        if (ammoText != null)
        {
            ammoText.text = $"탄약 : {ammo}";
        }
    }

    public void SetReloadingVisible(bool isVisible)
    {
        if(reloadTextObject != null)
            reloadTextObject.SetActive(isVisible);
    }
}
