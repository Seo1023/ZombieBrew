using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Passive/GasAttack")]
public class GasAttackSkillSO : PassiveSkillSO
{
    public GameObject gasEffectPrefab;

    public override void Activate(GameObject caster, Vector3 targetPosition, int level)
    {
        SkillLevelData data = GetLevelData(level);
        Vector3 center = caster.transform.position;
        float radius = data.range;

        if(gasEffectPrefab != null)
        {
            GameObject fx = GameObject.Instantiate(gasEffectPrefab, center, Quaternion.identity);
            GameObject.Destroy(fx, 2f);
        }

        Collider[] hits = Physics.OverlapSphere(center, radius);
        foreach(var hit in hits)
        {
            ZombieStats zombie = hit.GetComponent<ZombieStats>();
            if(zombie != null)
            {
                zombie.TakeDamage(data.damage);
            }
        }
    }
}
