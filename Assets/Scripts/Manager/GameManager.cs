using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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
    public TextMeshProUGUI ammoText;
    public GameObject reloadTextObject;

    [Header("EXP&GOLD")]
    public int gold = 0;
    public int exp = 0;
    public int level = 1;
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI expText;
    public Slider expBar;
    public TextMeshProUGUI levelText;

    public CharacterSO selectedCharacter;

    public List<WeaponSO> ownedWeapons = new List<WeaponSO>();
    public void PauseGame() => Time.timeScale = 0f;
    public void ResumeGame() => Time.timeScale = 1f;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        if (gameOverUI != null)
            gameOverUI.SetActive(false);
    }

    void Start()
    {
        UpdateExpUI();
        goldText.text = $"GOLD: {gold}";
        killText.text = $"���� ���� ��: {killCount} ����";
    }

    void Update()
    {
        if (isGameOver) return;

        timeRemaining -= Time.deltaTime;
        timeRemaining = Mathf.Max(0, timeRemaining);
        UpdateTimerUI();

        if (timeRemaining <= 0)
            EndGame();

        if (Input.GetKeyDown(KeyCode.Z))
        {
            CheckLevelUp();
        }
    }

    public void AddKill()
    {
        killCount++;
        if (killText != null)
            killText.text = $"���� ���� ��: {killCount} ����";
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

        Time.timeScale = 0f; // ����
        Debug.Log("���� �����");
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
        int requiredExp = level * 100;

        if (expBar != null)
        {
            expBar.maxValue = requiredExp;
            expBar.value = exp;
        }

        if (expText != null)
            expText.text = $"{exp} / {requiredExp}";
    }

    void CheckLevelUp()
    {
        int requiredExp = level * 100; // ��: ������ 100exp �ʿ�
        while (exp >= requiredExp)
        {
            exp -= requiredExp;
            level++;
            Debug.Log($"���� ��! ���� ����: {level}");
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
