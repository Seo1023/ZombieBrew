using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static WeaponSO;

[SerializeField]
public class WeaponData : MonoBehaviour
{
    public int id;
    public string weaponName;
    public string description;
    public string type;
    public int level;
    public int damage;
    public int maxAmmo;
    public int currentAmmo;
    public float fireRate;
    public float reloadTime;
    public string iconpath;
    public WeaponType weaponType;

    public void InitalizEnums()
    {
        if(Enum.TryParse(type, out WeaponType Type))
        {
            weaponType = Type;
        }
        else
        {
            Debug.LogError($"아이템 '{name}'에 유효하지 않은 타입 : {type}");
        }
    }
}
