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
        }
        else
        {
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }


    void Die()
    {
        if (spawner != null)
        {
            spawner.ZombieKilled(gameObject);
            GameManager.Instance.AddKill();
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}

