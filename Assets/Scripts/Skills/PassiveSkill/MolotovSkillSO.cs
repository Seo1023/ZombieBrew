using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Passive/Molotov")]
public class MolotovSkillSO : PassiveSkillSO
{
    public GameObject fireAreaPrefab;
    public override void Activate(GameObject caster, Vector3 targetPosition, int level)
    {
        SkillLevelData data = GetLevelData(level);
        var manager = caster.GetComponent<PassiveSkillManager>();
        var mySkill = manager?.GetSkill(this);
        if (mySkill == null) return;

        if (Time.time - mySkill.lastActivationTime < data.cooldown) return;

        Vector3 randomOffset = new Vector3(Random.Range(-4f, 4f), 0, Random.Range(-4f, 4f));

        Vector3 spawnPos = caster.transform.position + randomOffset;

        GameObject fireArea = Instantiate(fireAreaPrefab, spawnPos, Quaternion.identity);
        var effect = fireArea.GetComponent<MolotovEffect>();

        if (effect != null)
        {
            effect.Initialize(data.damage, data.effectValue);
        }

        mySkill.lastActivationTime = Time.time;
    }
}
