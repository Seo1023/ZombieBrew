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
    public TextMeshProUGUI[] lables = new TextMeshProUGUI[3];
    public int total = 0;

    public CharacterSO selectedCharacter;
    public string selectedMap;
    public List<WeaponSO> ownedWeapons = new List<WeaponSO>();
    public Transform player { get; set; }
    public Transform spawnPoint;

    public List<PassiveSkill> ownedPassiveSkills = new();
    public PassiveSkillManager skillManager;

    [Header("EXP & Gold")]
    public int gold = 0;
    public int exp = 0;
    public int level = 1;

    public float expBonusPercent = 0f;

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

    public void InitPlayerAndWeapon()
    {
        // ���� ���� �����ϴ� �÷��̾� ã�Ƽ� ����
        GameObject playerObj = GameObject.FindWithTag("Player");
        Debug.Log(playerObj != null ? $"[GameManager] playerObj ã��: {playerObj.name}" : "[GameManager] playerObj �� ã��!");

        player = playerObj.transform;

        // PassiveSkillManager ����
        skillManager = playerObj.GetComponent<PassiveSkillManager>();
        if (skillManager == null)
        {
            Debug.LogError("Player ������Ʈ�� PassiveSkillManager�� �����ϴ�.");
        }
        else
        {
            Debug.Log("PassiveSkillManager ���� �Ϸ�");
        }

        // ���� ���� (���õ� ĳ���Ͱ� �ִٸ�)
        if (selectedCharacter != null && selectedCharacter.defaultWeapon != null)
        {
            WeaponSO clonedWeapon = CloneWeapon(selectedCharacter.defaultWeapon);
            clonedWeapon.currentAmmo = clonedWeapon.maxAmmo;

            ownedWeapons.Clear();
            ownedWeapons.Add(clonedWeapon);

            var weaponManager = playerObj.GetComponent<PlayerWeaponManager>();
            if (weaponManager != null)
            {
                weaponManager.SetWeapon(clonedWeapon);
            }

            UpdateAmmoUI(clonedWeapon);
        }

        ownedPassiveSkills.Clear(); // ���� ������ �ʱ�ȭ
    }



    void Update()
    {
        if (isGameOver || !IsTimerActive) return;

        timeRemaining -= Time.deltaTime;
        timeRemaining = Mathf.Max(0, timeRemaining);
        UIManager.Instance.SetTime(timeRemaining);

        if (timeRemaining <= 0)
            ClearGame();

        if (Input.GetKeyDown(KeyCode.P))
        {
            CheatLevel();
        }
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
        int bounsAmount = Mathf.FloorToInt(amount * (expBonusPercent / 100f));

        exp += amount + bounsAmount;

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
        int requiredExp = level * 100; // ��: ������ 100exp �ʿ�
        while (exp >= requiredExp)
        {
            exp -= requiredExp;
            level++;
            Debug.Log($"���� ��! ���� ����: {level}");
            PassiveSkillSelectorUI.Instance.OpenRandomChoices();
            PauseGame();
            requiredExp = GetMaxExpForLevel(level);
        }
        UpdateExpUI();
    }

    public void CheatLevel()
    {
        int requiredExp = GetMaxExpForLevel(level);  // ���� ������ �ʿ��� �� ����ġ
        int neededExp = requiredExp - exp;           // ���� EXP���� ������ �縸ŭ�� �߰�

        if (neededExp > 0)
        {
            exp += neededExp;
            Debug.Log($"[Cheat] �������� �ʿ��� EXP {neededExp}��ŭ �߰���. ���� EXP: {exp}");
            CheckLevelUp();
        }
        else
        {
            Debug.Log("[Cheat] �̹� ������ ������ EXP�Դϴ�.");
            CheckLevelUp(); // Ȥ�ö� ���������� ���� Ȯ��
        }
    }

    int GetMaxExpForLevel(int level) => level * 100;

    public void ClearGame()
    {
        isGameClear = true;
        UIManager.Instance.ShowGameOverUI(true, killCount, gold);
        Time.timeScale = 0f;
    }
    
    public void EndGame()
    {
        isGameOver = true;
        UIManager.Instance.ShowGameOverUI(false, killCount, gold);
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

    public void AddOrUpgradePassiveSkill(PassiveSkillSO skillData)
    {
        Debug.Log("[GameManager] AddOrUpgradePassiveSkill() ȣ���.");

        var skill = ownedPassiveSkills.Find(s => s.data == skillData);
        if (skill == null)
        {
            var newSkill = new PassiveSkill { data = skillData, currentLevel = 1 };
            ownedPassiveSkills.Add(newSkill);

            Debug.Log($"[GameManager] ��ų {skillData.skillName} �߰���");

            if (skillManager != null)
            {
                skillManager.AddSkill(newSkill);
                Debug.Log($"[GameManager] ��ų {skillData.skillName} �� PassiveSkillManager�� ���޵�");
            }
            else
            {
                Debug.LogError("[GameManager] skillManager�� null�Դϴ�!");
            }
        }
        else if (skill.CanLevelUp)
        {
            skill.LevelUp();
            Debug.Log($"[GameManager] ��ų {skillData.skillName} ������ �� Lv.{skill.currentLevel}");
        }
    }

}
