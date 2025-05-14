using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    public float detectRange = 100f;
    public float attackRange = 3f;
    public float attackCooldown = 1.5f;
    public int damage = 10;

    private Transform target;
    private NavMeshAgent agent;
    private float lastAttackTime;
    private Monster monster;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        monster = GetComponent<Monster>();
    }

    public void Init(Transform player)
    {
        target = player;
    }

    void Update()
    {
        if (target == null || !agent.isOnNavMesh) return;

        agent.SetDestination(target.position);

        if (Vector3.Distance(transform.position, target.position) <= attackRange)
            TryAttack();
    }

    void TryAttack()
    {
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            lastAttackTime = Time.time;

            var stats = target.GetComponent<ChracterStats>();
            if (stats != null)
            {
                stats.TakeDamage(damage);
            }
        }
    }
}

