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

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (agent == null)
        {
            Debug.LogError("NavMeshAgent가 좀비에 없음!");
            enabled = false;
            return;
        }
        target = GameManager.Instance.player;
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
        if(Time.time - lastAttackTime >= attackCooldown)
        {
            lastAttackTime = Time.time;
            target.GetComponent<ChracterStats>().TakeDamage(damage);
            Debug.Log("좀비가 공격했다.");
        }
    }
}
