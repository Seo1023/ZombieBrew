using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Passive/AirStrike")]
public class AriStrikeSkillSO : PassiveSkillSO
{
    public override void Activate(GameObject caster, Vector3 targetPosition, int level)
    {
        var data = GetLevelData(level);
        ZombieStats target = FindClosestZombie(caster.transform.position, 50f);

        if (target != null)
        {
            target.TakeDamage(data.damage);
            Debug.Log($"[공중지원사격] Lv.{level} → {data.damage} 데미지 입힘");
        }
    }

    private ZombieStats FindClosestZombie(Vector3 origin, float range)
    {
        var hits = Physics.OverlapSphere(origin, range);
        float minDist = Mathf.Infinity;
        ZombieStats closest = null;

        foreach (var hit in hits)
        {
            var zombie = hit.GetComponent<ZombieStats>();
            if (zombie == null) continue;

            float dist = Vector3.Distance(origin, zombie.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                closest = zombie;
            }
        }

        return closest;
    }
}

