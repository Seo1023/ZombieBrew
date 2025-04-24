using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponManager : MonoBehaviour
{
    public static PlayerWeaponManager Instance;

    public Transform weaponHolder;
    public WeaponSO currentWeaponSO; // ���� ���� ����
    private GameObject currentWeaponGO;

    void Awake()
    {
        Instance = this;
        Debug.Log("PlayerWeaponManager �ν��Ͻ� ��� �Ϸ�");
    }

    public void EquipWeapon(WeaponSO weapon)
    {
        if (weapon == null || weapon.weaponPrefab == null)
        {
            Debug.LogError("���⳪ �������� ��� ����!");
            return;
        }

        currentWeaponSO = weapon;

        if (currentWeaponGO != null)
        {
            Destroy(currentWeaponGO);
            Debug.Log($"���� ���� ���ŵ�: {currentWeaponGO.name}");
        }

        currentWeaponGO = Instantiate(
            weapon.weaponPrefab,
            weaponHolder.position,
            weaponHolder.rotation,
            weaponHolder
        );

        Debug.Log($"�� ���� ������: {currentWeaponGO.name}");
    }

}
