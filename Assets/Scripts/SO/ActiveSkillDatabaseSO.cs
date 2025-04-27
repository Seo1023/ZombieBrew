using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ActiveSkillSO;

[CreateAssetMenu(fileName = "ActiveSkillDatabase", menuName = "Inventory/Database")]
public class ActiveSkillDatabaseSO : ScriptableObject
{
    public List<ActiveSkillSO> activeskills = new List<ActiveSkillSO>();

    private Dictionary<int, ActiveSkillSO> activeskillById;
    private Dictionary<string, ActiveSkillSO> activeskillByName;

    public void Initialize()
    {
        activeskillById = new Dictionary<int, ActiveSkillSO>();
        activeskillByName = new Dictionary<string, ActiveSkillSO>();

        foreach (var activeskill in activeskills)
        {
            activeskillById[activeskill.id] = activeskill;
            activeskillByName[activeskill.name] = activeskill;
        }
    }

    public ActiveSkillSO GetWeaponById(int id)
    {
        if (activeskillById == null)
        {
            Initialize();
        }
        if (activeskillById.TryGetValue(id, out ActiveSkillSO activeskill))
        {
            return activeskill;
        }
        return null;
    }

    public ActiveSkillSO GetWeaponByName(string name)
    {
        if (activeskillByName == null)
        {
            Initialize();
        }
        if (activeskillByName.TryGetValue(name, out ActiveSkillSO activeskill))
        {
            return activeskill;
        }
        return null;
    }

    public List<ActiveSkillSO> GetWEaponByType(SkillType type)
    {
        return activeskills.FindAll(activeskill => activeskill.skillType == type);
    }
}
