using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Passive/SupplyDrop")]
public class SupplyDropSkillSO : PassiveSkillSO
{
    public GameObject supplyBoxPrefab;

    public override void Activate(GameObject caster, Vector3 targetPosition, int level)
    {
        var data = GetLevelData(level);
        Vector3 spawnPos = caster.transform.position + new Vector3(
            Random.Range(-20f, 20f), 0, Random.Range(-20f, 20f));

        if (supplyBoxPrefab != null)
        {
            Instantiate(supplyBoxPrefab, spawnPos, Quaternion.identity);
            Debug.Log($"[공중보급] Lv.{level} → 보급품 생성 at {spawnPos}");
        }
    }
}
