using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PassiveSkillManager : MonoBehaviour
{
    public List<PassiveSkill> skills = new();

    void Start()
    {
        Debug.Log("��ų �ߵ� �ڷ�ƾ ����");
        StartCoroutine(ActivateSkillsRoutine());
    }

    IEnumerator ActivateSkillsRoutine()
    {
        Debug.Log("PassiveSkillManager �ڷ�ƾ ���۵�");

        while (true)
        {
            foreach (var skill in skills)
            {
                var data = skill.data;

                if (data.tickInterval > 0f && Time.time - skill.lastActivationTime >= data.tickInterval)
                {
                    Debug.Log($"[�ߵ� ���� ���] {data.skillName} -> Activate() ȣ��");
                    skill.lastActivationTime = Time.time;
                    data.Activate(gameObject, transform.position, skill.currentLevel);
                }
            }

            yield return new WaitForSeconds(0.2f);
        }
    }


    public void AddSkill(PassiveSkill newSkill)
    {
        Debug.Log("��ų �߰���");
        skills.Add(newSkill);
    }

    public PassiveSkill GetSkill(PassiveSkillSO data)
    {
        return skills.Find(s => s.data == data);
    }
}
