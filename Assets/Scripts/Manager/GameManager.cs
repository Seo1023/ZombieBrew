using TMPro;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Game State")]
    public int killCount = 0;
    public float timeRemaining = 600f;
    public bool isGameOver = false;
    public bool isGameClear = false;

    [Header("Player")]
    public Transform player;
    public CharacterSO selectedCharacter;
    public List<WeaponSO> ownedWeapons = new List<WeaponSO>();

    [Header("EXP & Gold")]
    public int gold = 0;
    public int exp = 0;
    public int level = 1;

    public void PauseGame() => Time.timeScale = 0f;
    public void ResumeGame() => Time.timeScale = 1f;

    void Awake()
    {
       if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UIManager.Instance.SetGold(gold);
        UIManager.Instance.SetKillCount(killCount);
        UpdateExpUI();
    }

    void Update()
    {
        if (isGameOver) return;

        timeRemaining -= Time.deltaTime;
        timeRemaining = Mathf.Max(0, timeRemaining);
        UIManager.Instance.SetTime(timeRemaining);

        if (timeRemaining <= 0)
            ClearGame();

        if (Input.GetKeyDown(KeyCode.Z))
        {
            CheckLevelUp();
        }
    }

    public void AddKill()
    {
        killCount++;
        UIManager.Instance.SetKillCount(killCount);
    }

    public void ClearGame()
    {
        isGameClear = true;
        UIManager.Instance.ShowGameOverUI();
    }

    public void EndGame()
    {
        isGameOver = true;
        UIManager.Instance.ShowGameOverUI();
        Time.timeScale = 0f; // 정지
        Debug.Log("게임 종료됨");
    }

    public void AddGold(int amount)
    {
        gold += amount;
        if (goldText != null)
            goldText.text = $"GOLD: {gold}";
    }

    public void AddExp(int amount)
    {
        exp += amount;

        UpdateExpUI();

        CheckLevelUp();
    }

    void UpdateExpUI()
    {
        UIManager.Instance.SetExp(exp, GetMaxExpForLevel(level));
        UIManager.Instance.SetLevel(level);
    }

    void CheckLevelUp()
    {
        int requiredExp = level * 100; // 예: 레벨당 100exp 필요
        while (exp >= requiredExp)
        {
            exp -= requiredExp;
            level++;
            Debug.Log($"레벨 업! 현재 레벨: {level}");
            WeaponSelectorUI.Instance.OpenRandomChoices();
            PauseGame();
            requiredExp = level * 100;
            if (levelText != null)
                levelText.text = $"{level}";
        }
        UpdateExpUI();
    }

    public void UpdateAmmoUI(WeaponSO weapon)
    {
        if (ammoText != null && weapon != null)
            ammoText.text = $"{weapon.currentAmmo} / {weapon.maxAmmo}";
    }

    public void SetReloadingUI(bool isReloading)
    {
        if (reloadTextObject != null)
            reloadTextObject.SetActive(isReloading);
    }

    public WeaponSO CloneWeapon(WeaponSO original)
    {
        WeaponSO clone = ScriptableObject.Instantiate(original);
        return clone;
    }
}
