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
    public bool IsTimerActive = false;

    public CharacterSO selectedCharacter;
    public string selectedMap;
    public List<WeaponSO> ownedWeapons = new List<WeaponSO>();

    [Header("EXP & Gold")]
    public int gold = 0;
    public int exp = 0;
    public int level = 1;

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
        if(selectedCharacter == null)
        {
            Debug.LogError("선택된 캐릭터가 없습니다.");
        }
        else
        {
            Debug.Log($"선택된 캐릭터 : {selectedCharacter.name}");
        }
    }

    void Update()
    {
        if (isGameOver || !IsTimerActive) return;

        timeRemaining -= Time.deltaTime;
        timeRemaining = Mathf.Max(0, timeRemaining);
        UIManager.Instance.SetTime(timeRemaining);

        if (timeRemaining <= 0)
            ClearGame();
    }

    public void PauseGame() => Time.timeScale = 0f;
    public void ResumeGame() => Time.timeScale = 1f;

    public void AddKill()
    {
        killCount++;
        UIManager.Instance.SetKillCount(killCount);
    }
    public void AddGold(int amount)
    {
        gold += amount;
        UIManager.Instance?.SetGold(gold);
    }

    public void AddExp(int amount)
    {
        exp += amount;

        UpdateExpUI();

        CheckLevelUp();
    }

    void UpdateExpUI()
    {
        UIManager.Instance?.SetExp(exp, GetMaxExpForLevel(level));
        UIManager.Instance?.SetLevel(level);
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
            requiredExp = GetMaxExpForLevel(level);
        }
        UpdateExpUI();
    }

    int GetMaxExpForLevel(int level) => level * 100;

    public void ClearGame()
    {
        isGameClear = true;
        UIManager.Instance.ShowGameOverUI(true);
        Time.timeScale = 0f;
    }
    
    public void EndGame()
    {
        isGameOver = true;
        UIManager.Instance.ShowGameOverUI(false);
        Time.timeScale = 0f;
    }
    public void UpdateAmmoUI(WeaponSO weapon)
    {
        if (weapon != null)
            UIManager.Instance?.SetAmmo(weapon.currentAmmo, weapon.maxAmmo);
    }

    public void SetReloadingUI(bool isReloading)
    {
        UIManager.Instance?.SetReloading(isReloading);
    }

    public WeaponSO CloneWeapon(WeaponSO original)
    {
        return ScriptableObject.Instantiate(original);
    }
}
