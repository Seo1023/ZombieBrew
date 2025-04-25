using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Linq;

public class WeaponSelectorUI : MonoBehaviour
{
    public static WeaponSelectorUI Instance;

    [Header("UI Elements")]
    public GameObject panel;                        // ���� ���� ��ü �г�
    public Transform buttonContainer;              // ��ư�� �� �θ�
    public GameObject weaponButtonPrefab;          // ��ư ������ (�Ʒ� ���� ����)

    [Header("Weapon Data")]
    public WeaponSO[] allWeapons;                // ���� �� ��� ���� SO��

    void Awake()
    {
        Instance = this;
        panel.SetActive(false);
        Debug.Log("WeaponSelectorUI �ν��Ͻ� ��ϵ�");
    }

    public void OpenRandomChoices()
    {
        panel.SetActive(true);

        // ���� ��ư ����
        foreach (Transform child in buttonContainer)
            Destroy(child.gameObject);

        // 3�� ���� ���� ����
        List<WeaponSO> randomWeapons = allWeapons.OrderBy(x => Random.value).Take(3).ToList();

        foreach (var weapon in randomWeapons)
        {
            GameObject btn = Instantiate(weaponButtonPrefab, buttonContainer);

            // ������ ���� ã��
            WeaponSO clone = GameManager.Instance.ownedWeapons
                .Find(w => w.weaponName == weapon.weaponName);

            //���� ���̸� ���� ���� �� ���׷��̵� ���� ������ �����ֱ�
            int displayLevel = (clone != null) ? clone.level + 1 : weapon.level;

            // �ؽ�Ʈ ����
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
