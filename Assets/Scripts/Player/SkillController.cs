using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkillController : MonoBehaviour
{
    public ActiveSkillSO activeSkill;
    private float cooldownRemaining;
    public bool isSkillActivate = false;

    public List<PassiveSkillSO> passiveSkills = new List<PassiveSkillSO>(2);

    void Update()
    {
        HandleCooldown();
        HandleInput();
    }

    void HandleCooldown()
    {
        if(cooldownRemaining > 0f)
        {
            cooldownRemaining -= Time.deltaTime;
        }
    }

    void HandleInput()
    {
        if (activeSkill == null || cooldownRemaining > 0f)
            return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            switch (activeSkill.activeSkillType)
            {
                case ActiveSkillSO.ActiveSkillType.Area:
                case ActiveSkillSO.ActiveSkillType.Buff:
                case ActiveSkillSO.ActiveSkillType.Spawn:
                    ExecuteSkill(transform.position);
                    break;

                case ActiveSkillSO.ActiveSkillType.MouseClick:
                    StartCoroutine(WaitForMouseClick());
                    break;

                case ActiveSkillSO.ActiveSkillType.Target:
                    Debug.LogWarning("타겟팅 스킬은 구현 안됨.");
                    break;

                default:
                    Debug.LogWarning($"[스킬] 정의되지 않은 스킬 타입 : {activeSkill.activeSkillType}");
                    break;
            }
        }
    }

    IEnumerator WaitForMouseClick()
    {
        Debug.Log("마우스 클릭 대기중");
        isSkillActivate = true;
        while (!Input.GetMouseButtonDown(0))
        {
            yield return null;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit hit))
        {
            ExecuteSkill(hit.point);
        }

        isSkillActivate = false;
    }

    void ExecuteSkill(Vector3 targetPosition)
    {
        SkillExecutor.Execute(activeSkill, gameObject, targetPosition);
        cooldownRemaining = activeSkill.cooldownTime;
    }
}
