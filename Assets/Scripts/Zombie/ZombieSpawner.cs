using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class ZombieSpawner : MonoBehaviour
{
    public ObjectPooler zombiePool;
    public ObjectPooler bossPool;
    public GameObject hpBarPrefab;
    public Transform player;

    public Vector2 spawnAreaMin;
    public Vector2 spawnAreaMax;

    public float waveDelay = 5f;
    public int currentWave = 1;
    public int zombiesPerWave = 5;
    private int spawnedCount = 0;
    private int deadCount = 0;

    void Start()
    {
        StartCoroutine(StartWave());
        StartCoroutine(StartLoop());
    }

    IEnumerator StartWave()
    {
        spawnedCount = 0;
        deadCount = 0;

        for (int i = 0; i < zombiesPerWave; i++)
        {
            SpawnZombie(false);
            yield return new WaitForSeconds(0.5f);
        }

        SpawnZombie(true); // 보스 한 마리
    }

    IEnumerator StartLoop()
    {
        while (true)
        {
            SpawnZombie(false);
            yield return new WaitForSeconds(1f);
        }
    }

    void SpawnZombie(bool isBoss)
    {
        //GameObject zombie = isBoss ? bossPool.Get() : zombiePool.Get();
        GameObject prefab = isBoss ? bossPool.prefab : zombiePool.prefab;
        Vector3 spawnPos = new Vector3(
            Random.Range(spawnAreaMin.x, spawnAreaMax.x),
            1f,
            Random.Range(spawnAreaMin.y, spawnAreaMax.y)
        );
        GameObject zombie = Instantiate(prefab, spawnPos, Quaternion.identity);
        if (zombie == null)
        {
            Debug.LogError("[스폰 에러] 풀에서 좀비를 못 가져왔습니다.");
            return;
        }

        NavMeshAgent agent = zombie.GetComponent<NavMeshAgent>();
        if (agent != null)
        {
            agent.enabled = false;
            zombie.transform.position = spawnPos;
            zombie.transform.rotation = Quaternion.identity;
            agent.enabled = true;
            agent.Warp(spawnPos);
            agent.SetDestination(player.position);
        }

        ZombieAI ai = zombie.GetComponent<ZombieAI>();
        if(ai != null)
        {
            ai.Init(player);
        }

        // 체력바 생성 및 연결
        GameObject hpBar = Instantiate(hpBarPrefab, zombie.transform);
        hpBar.transform.localPosition = new Vector3(0, 4f, 0);

        HealthBarController hpUI = hpBar.GetComponent<HealthBarController>();
        ZombieStats stats = zombie.GetComponent<ZombieStats>();

        if (stats != null && hpUI != null)
        {
            stats.spawner = this;
            stats.isBoss = isBoss;
            stats.Init(hpUI);
        }

        spawnedCount++;
    }

    public void ZombieKilled(GameObject zombie)
    {
        Monster monster = zombie.GetComponent<Monster>();
        if (monster != null)
        {
            monster.DropItems();
        }

        if (zombie.GetComponent<ZombieStats>().isBoss)
            bossPool.Return(zombie);
        else
            zombiePool.Return(zombie);

        deadCount++;

        if (deadCount >= spawnedCount)
        {
            StartCoroutine(NextWave());
        }
    }

    IEnumerator NextWave()
    {
        yield return new WaitForSeconds(waveDelay);
        currentWave++;
        zombiesPerWave += 3;
        StartCoroutine(StartWave());
    }
}

