using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Linq;

public class WeaponSelectorUI : MonoBehaviour
{
    public static WeaponSelectorUI Instance;

    [Header("UI Elements")]
    public GameObject panel;                        // 무기 선택 전체 패널
    public Transform buttonContainer;              // 버튼이 들어갈 부모
    public GameObject weaponButtonPrefab;          // 버튼 프리팹 (아래 구조 참고)

    [Header("Weapon Data")]
    public WeaponSO[] allWeapons;                // 게임 내 모든 무기 SO들

    void Awake()
    {
        Instance = this;
        panel.SetActive(false);
        Debug.Log("WeaponSelectorUI 인스턴스 등록됨");
    }

    public void OpenRandomChoices()
    {
        panel.SetActive(true);

        // 기존 버튼 제거
        foreach (Transform child in buttonContainer)
            Destroy(child.gameObject);

        // 3개 무기 랜덤 선택
        List<WeaponSO> randomWeapons = allWeapons.OrderBy(x => Random.value).Take(3).ToList();

        foreach (var weapon in randomWeapons)
        {
            GameObject btn = Instantiate(weaponButtonPrefab, buttonContainer);

            // 복제된 무기 찾기
            WeaponSO clone = GameManager.Instance.ownedWeapons
                .Find(w => w.weaponName == weapon.weaponName);

            //보유 중이면 다음 선택 시 업그레이드 예상 레벨로 보여주기
            int displayLevel = (clone != null) ? clone.level + 1 : weapon.level;

            // 텍스트 갱신
            btn.GetComponentInChildren<TextMeshProUGUI>().text = $"{weapon.weaponName} (Lv.{displayLevel})";
            btn.GetComponentInChildren<Image>().sprite = weapon.icon;

            btn.GetComponent<Button>().onClick.AddListener(() =>
            {
                if (clone != null)
                {
                    clone.Upgrade();
                }
                else
                {
                    var newClone = GameManager.Instance.CloneWeapon(weapon);
                    GameManager.Instance.ownedWeapons.Add(newClone);
                    PlayerWeaponManager.Instance.EquipWeapon(newClone);
                }

                GameManager.Instance.UpdateAmmoUI(clone ?? weapon);
                panel.SetActive(false);
                GameManager.Instance.ResumeGame();
            });
        }
    }
}
