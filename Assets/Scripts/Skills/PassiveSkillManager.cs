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
        Debug.Log("PassiveSkillManager 코루틴 시작됨");

        while (true)
        {
            foreach (var skill in skills)
            {
                var data = skill.data;

                if (data.tickInterval > 0f && Time.time - skill.lastActivationTime >= data.tickInterval)
                {
                    Debug.Log($"[발동 조건 통과] {data.skillName} -> Activate() 호출");
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
