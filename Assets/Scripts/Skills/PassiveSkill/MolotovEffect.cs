using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MolotovEffect : MonoBehaviour
{
    public float radius = 2.5f;
    private int damagePerTick;
    private float duration;
    private float tickInterval = 1f;

    public void Initialize(int damage, float duration)
    {
        this.damagePerTick = damage;
        this.duration = duration;

        StartCoroutine(DamageRoutine());
    }

    IEnumerator DamageRoutine()
    {
        float timer = 0f;

        while (timer < duration)
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, radius);
            foreach(var hit in hits)
            {
                ZombieStats zombie = hit.GetComponent<ZombieStats>(); 
                if(zombie != null)
                {
                    zombie.TakeDamage(damagePerTick);
                }
            }

            timer += tickInterval;
            yield return new WaitForSeconds(tickInterval);
        }
        Destroy(gameObject);
    }
}
