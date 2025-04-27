using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Weapons/Weapon Data")]
public class WeaponSO : ScriptableObject
{
    public enum WeaponType
    {
        Pistol,
        AssaultRifle,
        SubmachineGun,
        Shotgun,
        Grenade,
        SniperRifle
    }

    public int id;
    public string weaponName;
    public string description;
    public Sprite icon;
    public int level;
    public int damage;
    public int maxAmmo;
    public int currentAmmo;
    public float fireRate;
    public float reloadTime = 1.5f;
    public GameObject weaponPrefab;
    public WeaponType weaponType;

    public void Upgrade()
    {
        level++;

        // 예시: 스탯 증가
        damage += 5;
        fireRate += 0.5f;
        maxAmmo += 10;
        currentAmmo = maxAmmo;

        Debug.Log($"{weaponName} 업그레이드! Lv.{level}");
    }
}
