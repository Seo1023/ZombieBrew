using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ZombieStats : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    private HealthBarController healthBar;

    public bool isBoss;
    public ZombieSpawner spawner;

    public void Init(HealthBarController hpUI)
    {
        healthBar = hpUI;

        currentHealth = maxHealth;

        if (healthBar != null)
        {
            healthBar.SetMaxHealth(maxHealth);
        }
    }


    public void TakeDamage(int damage)
    {
        Debug.Log($"[TakeDamage] 호출됨! 현재 체력: {currentHealth}, 입은 데미지: {damage}");

        currentHealth -= damage;

        if (healthBar != null)
        {
            healthBar.SetHealth(currentHealth);
            Debug.Log($"[TakeDamage] 체력바 업데이트: {currentHealth}");
        }
        else
        {
            Debug.LogWarning("[TakeDamage] healthBar가 null입니다!");
        }

        if (currentHealth <= 0)
        {
            Debug.Log("[TakeDamage] 체력이 0 이하! Die 호출 예정");
            Die();
        }
    }


    void Die()
    {
        if (spawner != null)
        {
            spawner.ZombieKilled(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}

