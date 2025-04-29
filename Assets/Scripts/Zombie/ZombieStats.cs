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
        Debug.Log($"[TakeDamage] ȣ���! ���� ü��: {currentHealth}, ���� ������: {damage}");

        currentHealth -= damage;

        if (healthBar != null)
        {
            healthBar.SetHealth(currentHealth);
            Debug.Log($"[TakeDamage] ü�¹� ������Ʈ: {currentHealth}");
        }
        else
        {
            Debug.LogWarning("[TakeDamage] healthBar�� null�Դϴ�!");
        }

        if (currentHealth <= 0)
        {
            Debug.Log("[TakeDamage] ü���� 0 ����! Die ȣ�� ����");
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

