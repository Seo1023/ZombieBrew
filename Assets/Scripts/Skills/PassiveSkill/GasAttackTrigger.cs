using UnityEngine;
using System.Collections.Generic;

public class GasAttackTrigger : MonoBehaviour
{
    public int damagePerSecond = 5;
    private float tickInterval = 1f;
    private Dictionary<Collider, float> lastHitTime = new();

    public void SetDamage(int damage)
    {
        damagePerSecond = damage;
    }

    public void SetRadius(float radius)
    {
        transform.localScale = Vector3.one * radius;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Zombie"))
        {
            float last;
            lastHitTime.TryGetValue(other, out last);

            if (Time.time - last >= tickInterval)
            {
                var stats = other.GetComponent<ZombieStats>();
                if (stats != null)
                {
                    stats.TakeDamage(damagePerSecond);
                    lastHitTime[other] = Time.time;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (lastHitTime.ContainsKey(other))
        {
            lastHitTime.Remove(other);
        }
    }
}
