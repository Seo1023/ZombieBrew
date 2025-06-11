using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Passive/AirStrike")]
public class AriStrikeSkillSO : PassiveSkillSO
{
    public override void Activate(GameObject caster, Vector3 targetPosition, int level)
    {
        SkillLevelData data = GetLevelData(level);
        var manager = caster.GetComponent<PassiveSkillManager>();
        var mySkill = manager?.GetSkill(this);
        if (mySkill == null) return;

        if (Time.time - mySkill.lastActivationTime < data.cooldown) return;

        ZombieStats closest = FindClosestZombie(caster.transform.position, 10f);
        if(closest != null)
        {
            closest.TakeDamage(data.damage);
            mySkill.lastActivationTime = Time.time;
        }
    }

    private ZombieStats FindClosestZombie(Vector3 origin, float range)
    {
        Collider[] hits = Physics.OverlapSphere(origin, range);
        float closestDist = Mathf.Infinity;
        ZombieStats closest = null;

        foreach(var hit in hits)
        {
            ZombieStats z = hit.GetComponent<ZombieStats>();
            if( z!= null)
            {
                float dist = Vector3.Distance(origin, z.transform.position);
                if(dist < closestDist)
                {
                    closestDist = dist;
                    closest = z;
                }
            }
        }
        return closest;
    }
}
