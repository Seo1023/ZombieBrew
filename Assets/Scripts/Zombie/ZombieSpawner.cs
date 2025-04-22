using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZombieSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject zombiePrefab;         // ���� ������
    public GameObject hpBarPrefab;          // ���� �����̽� ü�¹� ������
    public Transform player;                // �÷��̾� Transform
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
        // 1) ���� ��ġ ��� (�÷��̾� �ֺ� ����)
        Vector2 randomCircle = Random.insideUnitCircle.normalized;
        Vector3 spawnPos = player.position + new Vector3(randomCircle.x, 0, randomCircle.y) * spawnRadius;

        // 2) ���� ����
        GameObject zombie = Instantiate(zombiePrefab, spawnPos, Quaternion.identity);

        // 3) ü�¹� ����
        GameObject hpBar = Instantiate(hpBarPrefab);
        hpBar.transform.SetParent(zombie.transform, false);              // ���� �ڽ����� ����
        hpBar.transform.localPosition = new Vector3(0, 4f, 0);           // �Ӹ� �� ��ġ

        // 4) healthBar ����
        ZombieStats stats = zombie.GetComponent<ZombieStats>();
        Slider slider = hpBar.GetComponentInChildren<Slider>();
        stats.healthBar = slider;

        // 5) ü�� �ʱ�ȭ (������ ��)
        stats.SetMaxHealth(stats.maxHealth);
    }
}
