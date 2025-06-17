using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChracterStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public Slider healthBar;
    public TextMeshProUGUI healthText;

    void Start()
    {
        currentHealth = maxHealth;

        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }

        if (healthText != null)
            healthText.text = $"{currentHealth} / {maxHealth}";
    }

    public void TakeDamage(int damage)
    {
        if (currentHealth <= 0) return;

        if (HasLuckyCoinEvade(damage)) return;

        currentHealth = Mathf.Max(currentHealth - damage, 0);

        if (healthBar != null)
            healthBar.value = currentHealth;

        if (healthText != null)
            healthText.text = $"{currentHealth} / {maxHealth}";

        if (currentHealth == 0)
        {
            GameManager.Instance.EndGame();
        }
    }

    public void Heal(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);

        if (healthBar != null)
            healthBar.value = currentHealth;

        if (healthText != null)
            healthText.text = $"{currentHealth} / {maxHealth}";
    }

    private bool HasLuckyCoinEvade(int damage)
    {
        var skillManager = GetComponent<PassiveSkillManager>();
        if (skillManager == null) return false;

        foreach(var skill in skillManager.skills)
        {
            if(skill.data is LuckyCoinSkillSO luckyCoin)
            {
                float chance = luckyCoin.GetEvasionChance(skill.currentLevel);
                if(Random.value < chance / 100f)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
