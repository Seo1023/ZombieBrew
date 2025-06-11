using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Passive/SupplyDrop")]
public class SupplyDropSkillSO : PassiveSkillSO
{
    public GameObject supplyBoxPrefab;

    public override void Activate(GameObject caster, Vector3 targetPosition, int level)
    {
        SkillLevelData data = GetLevelData(level);
        var manager = caster.GetComponent<PassiveSkillManager>();
        var mySkill = manager?.GetSkill(this);
        if (mySkill == null) return;

        if (Time.time - mySkill.lastActivationTime < data.cooldown) return;

        Vector3 spawnPos = caster.transform.position + new Vector3(Random.Range(-3f, 3f), 0f, Random.Range(-3f, 3f));
        
        if(supplyBoxPrefab != null)
        {
            GameObject box = GameObject.Instantiate(supplyBoxPrefab, spawnPos, Quaternion.identity);
        }
        mySkill.lastActivationTime = Time.time;
    }
}
