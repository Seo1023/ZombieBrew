using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZombieSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject zombiePrefab;         // 좀비 프리팹
    public GameObject hpBarPrefab;          // 월드 스페이스 체력바 프리팹
    public Transform player;                // 플레이어 Transform
    public float spawnRadius = 15f;
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
        // 1) 스폰 위치 계산 (플레이어 주변 원형)
        Vector2 randomCircle = Random.insideUnitCircle.normalized;
        Vector3 spawnPos = player.position + new Vector3(randomCircle.x, 0, randomCircle.y) * spawnRadius;

        // 2) 좀비 생성
        GameObject zombie = Instantiate(zombiePrefab, spawnPos, Quaternion.identity);

        // 3) 체력바 생성
        GameObject hpBar = Instantiate(hpBarPrefab);
        hpBar.transform.SetParent(zombie.transform, false);              // 좀비 자식으로 붙임
        hpBar.transform.localPosition = new Vector3(0, 4f, 0);           // 머리 위 위치

        // 4) healthBar 연결
        ZombieStats stats = zombie.GetComponent<ZombieStats>();
        Slider slider = hpBar.GetComponentInChildren<Slider>();
        stats.healthBar = slider;

        // 5) 체력 초기화 (안정성 ↑)
        stats.SetMaxHealth(stats.maxHealth);
    }
}
