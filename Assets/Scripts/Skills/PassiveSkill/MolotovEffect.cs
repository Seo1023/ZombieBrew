using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MolotovEffect : MonoBehaviour
{
    private int damage;
    private float radius;
    private float duration;
    private float tick = 1f;

    private Dictionary<Collider, float> lastHitTime = new();

    public void Initialize(int dmg, float areaRadius, float dur)
    {
        damage = dmg;
        radius = areaRadius;
        duration = dur;

        transform.localScale = new Vector3(radius, radius, radius);
        StartCoroutine(DamageRoutine());
    }

    private IEnumerator DamageRoutine()
    {
        float timer = 0f;

        while (timer < duration)
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, radius / 2f);
            foreach (var hit in hits)
            {
                if (hit.CompareTag("Zombie"))
                {
                    if (!lastHitTime.ContainsKey(hit) || Time.time - lastHitTime[hit] >= tick)
                    {
                        hit.GetComponent<ZombieStats>()?.TakeDamage(damage);
                        lastHitTime[hit] = Time.time;
                    }
                }
            }

            timer += tick;
            yield return new WaitForSeconds(tick);
        }

        Destroy(gameObject);
    }
}
