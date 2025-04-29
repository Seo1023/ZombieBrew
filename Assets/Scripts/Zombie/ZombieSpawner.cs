using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ZombieSpawner : MonoBehaviour
{
    public ObjectPooler zombiePool;
    public ObjectPooler bossPool;
    public Transform player;
    public GameObject groundObject;
    public GameObject hpBarPrefab;
    public float spawnInterval = 1f;
    public int zombiesPerWave = 5;
    public float waveDelay = 5f; // 웨이브 간 대기 시간

    private int currentWave = 1;
    private int spawnedCount = 0;
    private int deadCount = 0;
    private bool isSpawning = false;

    void Start()
    {
        StartCoroutine(StartWave());
    }

    IEnumerator StartWave()
    {
        isSpawning = true;
        spawnedCount = 0;
        deadCount = 0;

        for (int i = 0; i < zombiesPerWave; i++)
        {
            SpawnZombie(false); // 일반 좀비 스폰
            spawnedCount++;
            yield return new WaitForSeconds(spawnInterval);
        }

        yield return new WaitForSeconds(spawnInterval);

        SpawnZombie(true); // 보스 좀비 스폰
        spawnedCount++;

        isSpawning = false;
    }

    void SpawnZombie(bool isBoss)
    {
        Vector3 spawnPos = GetRandomPositionOnGround();
        Debug.Log($"[스폰] Random SpawnPos: {spawnPos}"); //추가

        GameObject zombie = isBoss ? bossPool.Get() : zombiePool.Get();
        ZombieStats stats = zombie.GetComponent<ZombieStats>();
        stats.spawner = this;
        stats.isBoss = isBoss;

        NavMeshAgent agent = zombie.GetComponent<NavMeshAgent>();
        if (agent != null)
        {
            agent.enabled = false;
            zombie.transform.position = spawnPos;
            zombie.transform.rotation = Quaternion.identity;
            agent.enabled = true;

            agent.Warp(spawnPos); // 추가해야 함!
            agent.SetDestination(player.position);
        }
        else
        {
            zombie.transform.position = spawnPos;
            zombie.transform.rotation = Quaternion.identity;
        }


        StartCoroutine(DebugPosition(zombie)); //디버깅 추가

        if (stats.healthBar == null && hpBarPrefab != null)
        {
            GameObject hpBar = Instantiate(hpBarPrefab, zombie.transform);
            hpBar.transform.localPosition = new Vector3(0, 4f, 0);
            stats.healthBar = hpBar.GetComponentInChildren<Slider>();
        }
        else if (stats.healthBar != null)
        {
            stats.healthBar.value = 1f;
            stats.healthBar.maxValue = stats.maxHealth;
        }

        stats.SetMaxHealth(stats.maxHealth);
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
        zombiesPerWave += 3; // 웨이브마다 3마리씩 증가
        StartCoroutine(StartWave());
    }

    Vector3 GetRandomPositionOnGround()
    {
        Renderer renderer = groundObject.GetComponent<Renderer>();
        Bounds bounds = renderer.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float z = Random.Range(bounds.min.z, bounds.max.z);
        Vector3 spawnPos = new Vector3(x, bounds.center.y + 1f, z);

        return spawnPos;
    }

    IEnumerator DebugPosition(GameObject zombie)
    {
        yield return new WaitForSeconds(0.1f); // 딱 0.1초 기다렸다가

        if (zombie != null)
        {
            Debug.Log($"[스폰] Real Position After Spawn: {zombie.transform.position}");
        }
    }

}

