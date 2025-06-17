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
        // 씬에 직접 존재하는 플레이어 찾아서 연결
        GameObject playerObj = GameObject.FindWithTag("Player");
        Debug.Log(playerObj != null ? $"[GameManager] playerObj 찾음: {playerObj.name}" : "[GameManager] playerObj 못 찾음!");

        player = playerObj.transform;

        // PassiveSkillManager 연결
        skillManager = playerObj.GetComponent<PassiveSkillManager>();
        if (skillManager == null)
        {
            Debug.LogError("Player 오브젝트에 PassiveSkillManager가 없습니다.");
        }
        else
        {
            Debug.Log("PassiveSkillManager 연결 완료");
        }

        // 무기 세팅 (선택된 캐릭터가 있다면)
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

        ownedPassiveSkills.Clear(); // 이전 데이터 초기화
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
        int requiredExp = level * 100; // 예: 레벨당 100exp 필요
        while (exp >= requiredExp)
        {
            exp -= requiredExp;
            level++;
            Debug.Log($"레벨 업! 현재 레벨: {level}");
            PassiveSkillSelectorUI.Instance.OpenRandomChoices();
            PauseGame();
            requiredExp = GetMaxExpForLevel(level);
        }
        UpdateExpUI();
    }

    public void CheatLevel()
    {
        int requiredExp = GetMaxExpForLevel(level);  // 현재 레벨에 필요한 총 경험치
        int neededExp = requiredExp - exp;           // 현재 EXP에서 부족한 양만큼만 추가

        if (neededExp > 0)
        {
            exp += neededExp;
            Debug.Log($"[Cheat] 레벨업에 필요한 EXP {neededExp}만큼 추가됨. 현재 EXP: {exp}");
            CheckLevelUp();
        }
        else
        {
            Debug.Log("[Cheat] 이미 레벨업 가능한 EXP입니다.");
            CheckLevelUp(); // 혹시라도 누락됐으면 강제 확인
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
        Debug.Log("[GameManager] AddOrUpgradePassiveSkill() 호출됨.");

        var skill = ownedPassiveSkills.Find(s => s.data == skillData);
        if (skill == null)
        {
            var newSkill = new PassiveSkill { data = skillData, currentLevel = 1 };
            ownedPassiveSkills.Add(newSkill);

            Debug.Log($"[GameManager] 스킬 {skillData.skillName} 추가됨");

            if (skillManager != null)
            {
                skillManager.AddSkill(newSkill);
                Debug.Log($"[GameManager] 스킬 {skillData.skillName} → PassiveSkillManager에 전달됨");
            }
            else
            {
                Debug.LogError("[GameManager] skillManager가 null입니다!");
            }
        }
        else if (skill.CanLevelUp)
        {
            skill.LevelUp();
            Debug.Log($"[GameManager] 스킬 {skillData.skillName} 레벨업 → Lv.{skill.currentLevel}");
        }
    }

}
