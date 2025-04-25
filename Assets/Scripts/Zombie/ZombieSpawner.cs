using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ZombieSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject zombiePrefab;
    public GameObject hpBarPrefab;
    public Transform player;
    public GameObject groundObject;
    public float spawnInterval = 2f;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            timer = 0f;
            SpawnZombie();
        }
    }

    void SpawnZombie()
    {
        Vector3 spawnPos = GetRandomPositionOnGround();

        // NavMesh 위 위치로 보정
        if (NavMesh.SamplePosition(spawnPos, out NavMeshHit hit, 2f, NavMesh.AllAreas))
        {
            // 바닥보다 1f 위로 띄워서 박힘 방지!
            spawnPos = hit.position + Vector3.up * 2.0f;

            GameObject zombie = Instantiate(zombiePrefab, spawnPos, Quaternion.identity);

            GameObject hpBar = Instantiate(hpBarPrefab);
            hpBar.transform.SetParent(zombie.transform, false);
            hpBar.transform.localPosition = new Vector3(0, 4f, 0);

            ZombieStats stats = zombie.GetComponent<ZombieStats>();
            Slider slider = hpBar.GetComponentInChildren<Slider>();
            stats.healthBar = slider;
            stats.SetMaxHealth(stats.maxHealth);
        }
    }

    Vector3 GetRandomPositionOnGround()
    {
        Renderer groundRenderer = groundObject.GetComponent<Renderer>();
        if (groundRenderer == null)
        {
            Debug.LogWarning("Ground object에 Renderer가 없음!");
            return transform.position;
        }

        Bounds bounds = groundRenderer.bounds;

        for (int i = 0; i < 10; i++)
        {
            float x = Random.Range(bounds.min.x, bounds.max.x);
            float z = Random.Range(bounds.min.z, bounds.max.z);
            Vector3 samplePoint = new Vector3(x, bounds.max.y + 5f, z); // 샘플 지점: 바닥보다 확실히 높게

            if (NavMesh.SamplePosition(samplePoint, out NavMeshHit hit, 10f, NavMesh.AllAreas))
            {
                // 바닥에서 살짝 위로 띄우기 (안 박히게)
                return hit.position + Vector3.up * .5f;
            }
        }

        Debug.LogWarning("NavMesh 위치를 찾지 못했습니다.");
        return transform.position;
    }

}
