using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZombieStats : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 100;
    private int currentHealth;

    [Header("HealthBar UI")]
    public Slider healthBar;

    void Start()
    {
        currentHealth = maxHealth;

        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }
    }

    public void TakeDamage(int dmg)
    {
        if (currentHealth <= 0) return;

        currentHealth = Mathf.Max(currentHealth - dmg, 0);

        if (healthBar != null)
            healthBar.value = currentHealth;

        if (currentHealth == 0)
            Die();
    }

    void Die()
    {
        Monster monster = GetComponent<Monster>();
        if(monster != null)
        {
            monster.Die();
            GameManager.Instance.AddKill();
        }
        else
        {
            Destroy(gameObject);
        }

    }

    public void SetMaxHealth(int max)
    {
        maxHealth = max;
        currentHealth = max;

        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }
    }
}
