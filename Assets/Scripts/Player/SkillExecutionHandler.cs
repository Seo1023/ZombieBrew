using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillExecutionHandler : MonoBehaviour
{
    public GameObject fireBombEffect;

    public static void Execute(ActiveSkillSO skill, GameObject caster, Vector3 targetPosition)
    {
        switch (skill.id)
        {
            case 1001:
                FireBomb(skill, caster, targetPosition);
                break;

            default:
                break;
        }
    }

    static void FireBomb(ActiveSkillSO skill, GameObject caster, Vector3 targetPosition)
    {
        Debug.Log("공중폭격 실행");

        if(skill.effectPrefab != null)
        {
            GameObject fx = Object.Instantiate(skill.effectPrefab, targetPosition, Quaternion.identity);
            Object.Destroy(fx, 2f);
        }

        Collider[] targets = Physics.OverlapSphere(targetPosition, skill.range);
        foreach(var enemy in targets)
        {
            var damageable = enemy.GetComponent<ZombieStats>();
            if(damageable != null)
            {
                damageable.TakeDamage(skill.damage);
            }
        }
    }
}
