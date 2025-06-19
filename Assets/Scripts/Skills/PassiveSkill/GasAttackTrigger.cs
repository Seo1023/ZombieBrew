using UnityEngine;
using System.Collections.Generic;

public class GasAttackTrigger : MonoBehaviour
{
    public int damagePerSecond = 5;
    private float tickInterval = 1f;
    public GameObject effectPrefab;
    PassiveSkill passiveSkill;
    GameObject spawneffect;
    private Dictionary<Collider, float> lastHitTime = new();

    private void Start()
    {
        passiveSkill = GameManager.Instance.ownedPassiveSkills.Find(x => x.data.skillName == "È­»ý¹æ");
        spawneffect = Instantiate(effectPrefab);
    }

    private void Update()
    {
        spawneffect.transform.position = transform.position;
        SetScale();
    }
    public void SetDamage(int damage)
    {
        damagePerSecond = damage;
    }
    void SetScale()
    {
        float scale = passiveSkill.levelScalse;
        transform.localScale = new Vector3(scale, 1, scale);
        if(spawneffect != null)
        {
            if(passiveSkill.currentLevel == 1)
            {
                spawneffect.transform.localScale = new Vector3(2.4f, 2.4f, 2.4f);
            } 
            else if(passiveSkill.currentLevel == 2)
            {
                spawneffect.transform.localScale = new Vector3(2.8f, 2.8f, 2.8f);
            }
            else if (passiveSkill.currentLevel == 3)
            {
                spawneffect.transform.localScale = new Vector3(3.2f, 3.2f, 2.4f);
            }
            else if (passiveSkill.currentLevel == 4)
            {
                spawneffect.transform.localScale = new Vector3(2.4f, 2.4f, 2.4f);
            }
            else if (passiveSkill.currentLevel == 5)
            {
                spawneffect.transform.localScale = new Vector3(4f, 4f, 2.4f);
            }
        }
    }
    public void SetRadius(float radius)
    {
        transform.localScale = Vector3.one * radius;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Zombie"))
        {
            float last;
            lastHitTime.TryGetValue(other, out last);

            if (Time.time - last >= tickInterval)
            {
                var stats = other.GetComponent<ZombieStats>();
                if (stats != null)
                {
                    stats.TakeDamage(damagePerSecond);
                    lastHitTime[other] = Time.time;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (lastHitTime.ContainsKey(other))
        {
            lastHitTime.Remove(other);
        }
    }
}
