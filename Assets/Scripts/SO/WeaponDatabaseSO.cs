using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static WeaponSO;

[CreateAssetMenu(fileName = "WeaponDatabase", menuName = "Inventory/WeaponDatabase")]
public class WeaponDatabaseSO : ScriptableObject
{
    public List<WeaponSO> weapons = new List<WeaponSO>();

    private Dictionary<int, WeaponSO> weaponById;
    private Dictionary<string,  WeaponSO> weaponByName;

    public void Initialize()
    {
        weaponById = new Dictionary<int, WeaponSO>();
        weaponByName = new Dictionary<string, WeaponSO>();

        foreach (var weapon in weapons)
        {
            weaponById[weapon.id] = weapon;
            weaponByName[weapon.name] = weapon;
        }
    }

    public WeaponSO GetWeaponById(int id)
    {
        if(weaponById == null)
        {
            Initialize();
        }
        if(weaponById.TryGetValue(id, out WeaponSO weapon))
        {
            return weapon;
        }
        return null;
    }

    public WeaponSO GetWeaponByName(string name)
    {
        if (weaponByName == null)
        {
            Initialize();
        }
        if (weaponByName.TryGetValue(name, out WeaponSO weapon))
        {
            return weapon;
        }
        return null;
    }

    public List<WeaponSO> GetWEaponByType(WeaponType type)
    {
        return weapons.FindAll(weapon => weapon.weaponType == type);
    }
}
