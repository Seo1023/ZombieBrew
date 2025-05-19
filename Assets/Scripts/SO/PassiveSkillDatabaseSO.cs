using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PassiveSkillSO;

[CreateAssetMenu(fileName = "PassiveSkillDatabase", menuName = "Inventory/PassiveSkillDatabase")]
public class PassiveSkillDatabaseSO : ScriptableObject
{
    public List<PassiveSkillSO> passiveskills = new List<PassiveSkillSO>();

    private Dictionary<int, PassiveSkillSO> passiveskillById;
    private Dictionary<string, PassiveSkillSO> passiveskillByName;

    public void Initialize()
    {
        passiveskillById = new Dictionary<int, PassiveSkillSO>();
        passiveskillByName = new Dictionary<string, PassiveSkillSO>();

        foreach (var passiveskill in passiveskills)
        {
            passiveskillById[passiveskill.id] = passiveskill;
            passiveskillByName[passiveskill.name] = passiveskill;
        }
    }

    public PassiveSkillSO GetWeaponById(int id)
    {
        if (passiveskillById == null)
        {
            Initialize();
        }
        if (passiveskillById.TryGetValue(id, out PassiveSkillSO passiveskill))
        {
            return passiveskill;
        }
        return null;
    }

    public PassiveSkillSO GetWeaponByName(string name)
    {
        if (passiveskillByName == null)
        {
            Initialize();
        }
        if (passiveskillByName.TryGetValue(name, out PassiveSkillSO passiveskill))
        {
            return passiveskill;
        }
        return null;
    }

    public List<PassiveSkillSO> GetWEaponByType(PassiveSkillType type)
    {
        return passiveskills.FindAll(passiveskill => passiveskill.passiveSkillType == type);
    }
}
