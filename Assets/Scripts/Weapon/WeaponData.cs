using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Weapons/Weapon Data")]
public class WeaponData : ScriptableObject
{
    public enum WeaponType
    {
        Pistol,
        AssaultRifle,
        SubmachineGun,
        Shotgun,
        Grenade,
        Knife
    }

    public string weaponName;
    public string description;
    public Sprite icon;
    public int level;
    public int damage;
    public int maxAmmo;
    public int currentAmmo;
    public float fireRate;
    public WeaponType weaponType;
}
