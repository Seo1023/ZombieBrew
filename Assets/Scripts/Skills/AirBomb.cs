using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AirBomb
{
    public static void Execute(ActiveSkillSO skill, GameObject caster, Vector3 target)
    {
        UIManager.Instance?.StartAirBombCooldown(skill.cooldownTime);

        if(skill.audioClip != null)
        {
            AudioSource.PlayClipAtPoint(skill.audioClip, target, 1.5f);
        }

        if (skill.effectPrefab != null)
        {
            GameObject fx = Object.Instantiate(skill.effectPrefab, target, Quaternion.identity);
            fx.transform.localScale = Vector3.one * skill.range;
            Object.Destroy(fx, 2f);
        }

        Collider[] hits = Physics.OverlapSphere(target, skill.range);

        foreach(Collider hit in hits)
        {
            if (hit.GetComponent<ZombieStats>())
            {
                hit.GetComponent<ZombieStats>().TakeDamage(skill.damage);
            }
        }
    }
}