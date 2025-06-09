using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PassiveSkillManager : MonoBehaviour
{
    public List<PassiveSkill> skills = new();

    void Start()
    {
        StartCoroutine(ActivateSkillsRoutine());
    }

    IEnumerator ActivateSkillsRoutine()
    {
        while (true)
        {
            foreach (var skill in skills)
            {
                PassiveSkillSO data = skill.data;

                if (data.tickInterval > 0f &&
                    Time.time - skill.lastActivationTime >= data.tickInterval)
                {
                    skill.lastActivationTime = Time.time;
                    data.Activate(gameObject, transform.position, skill.currentLevel);
                }
            }

            yield return new WaitForSeconds(0.2f); // 간격 체크
        }
    }

    public void AddSkill(PassiveSkill newSkill)
    {
        skills.Add(newSkill);
    }
}
