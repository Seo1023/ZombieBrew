using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponManager : MonoBehaviour
{
    public static PlayerWeaponManager Instance;
    public Transform weaponHolder;
    public WeaponSO currentWeaponSO;
    private GameObject currentWeaponGO;

    void Awake() => Instance = this;

    public void SetWeapon(WeaponSO weapon)
    {
        if (weapon == null || weapon.weaponPrefab == null)
        {
            Debug.LogError("무기 정보 누락!");
            return;
        }

        currentWeaponSO = weapon;

        if (currentWeaponGO != null)
            Destroy(currentWeaponGO);

        currentWeaponGO = Instantiate(weapon.weaponPrefab, weaponHolder);
        GameManager.Instance?.UpdateAmmoUI(currentWeaponSO);
    }

    public bool TryUseAmmo()
    {
        if (currentWeaponSO == null || currentWeaponSO.currentAmmo <= 0)
            return false;

        currentWeaponSO.currentAmmo--;
        GameManager.Instance?.UpdateAmmoUI(currentWeaponSO);
        return true;
    }

    public void Reload()
    {
        currentWeaponSO.currentAmmo = currentWeaponSO.maxAmmo;
        GameManager.Instance?.UpdateAmmoUI(currentWeaponSO);
    }
}

