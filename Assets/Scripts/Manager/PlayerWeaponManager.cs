using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponManager : MonoBehaviour
{
    public static PlayerWeaponManager Instance;

    public Transform weaponHolder;
    public WeaponSO currentWeaponSO; // 무기 정보 저장
    private GameObject currentWeaponGO;

    void Awake()
    {
        Instance = this;
        Debug.Log("PlayerWeaponManager 인스턴스 등록 완료");
    }

    public void EquipWeapon(WeaponSO weapon)
    {
        if (weapon == null || weapon.weaponPrefab == null)
        {
            Debug.LogError("무기나 프리팹이 비어 있음!");
            return;
        }

        currentWeaponSO = weapon;

        if (currentWeaponGO != null)
        {
            Destroy(currentWeaponGO);
            Debug.Log($"기존 무기 제거됨: {currentWeaponGO.name}");
        }

        currentWeaponGO = Instantiate(
            weapon.weaponPrefab,
            weaponHolder.position,
            weaponHolder.rotation,
            weaponHolder
        );

        Debug.Log($"새 무기 장착됨: {currentWeaponGO.name}");
    }

}
