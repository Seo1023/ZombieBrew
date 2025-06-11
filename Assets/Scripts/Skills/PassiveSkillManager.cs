using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PassiveSkillManager : MonoBehaviour
{
    public List<PassiveSkill> skills = new();

    void Start()
    {
        Debug.Log("스킬 발동 코루틴 시작");
        StartCoroutine(ActivateSkillsRoutine());
    }

    IEnumerator ActivateSkillsRoutine()
    {
        while (true)
        {
            foreach (var skill in skills)
            {
                var data = skill.data;
                var levelData = data.GetLevelData(skill.currentLevel);
                float cooldown = levelData.cooldown;

                if (cooldown > 0f && Time.time - skill.lastActivationTime >= cooldown)
                {
                    skill.lastActivationTime = Time.time;
                    data.Activate(gameObject, transform.position, skill.currentLevel);
                }
            }

            yield return new WaitForSeconds(0.2f);
        }
    }

    public void AddSkill(PassiveSkill newSkill)
    {
        Debug.Log("스킬 추가됨");
        skills.Add(newSkill);
    }

    public PassiveSkill GetSkill(PassiveSkillSO data)
    {
        return skills.Find(s => s.data == data);
    }
}
