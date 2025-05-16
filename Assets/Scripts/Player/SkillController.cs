using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkillController : MonoBehaviour
{
    public ActiveSkillSO activeSkill;
    private float cooldownRemaining;

    void Update()
    {
        if (activeSkill == null || cooldownRemaining > 0)
        {
            if(cooldownRemaining > 0)
            {
                cooldownRemaining -= Time.deltaTime;
                return;
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            switch (activeSkill.skillType)
            {
                case ActiveSkillSO.SkillType.Area:
                case ActiveSkillSO.SkillType.Buff:
                case ActiveSkillSO.SkillType.Spawn:
                    ExecuteSkill(transform.position);
                break;

                case ActiveSkillSO.SkillType.MouseClick:
                    StartCoroutine(WaitForMouseClick());
                break;

                case ActiveSkillSO.SkillType.Target:
                    Debug.Log("타겟팅 미구현");
                break;
            }
                

        }

        IEnumerator WaitForMouseClick()
        {
            Debug.Log("마우스 클릭 대기중...");
            while (!Input.GetMouseButtonDown(0))
            {
                yield return null;
            }

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out RaycastHit hit))
            {
                ExecuteSkill(hit.point);
            }
        }

        void ExecuteSkill(Vector3 targetPosition)
        {
            activeSkill.Activate(gameObject, targetPosition);
            cooldownRemaining = activeSkill.cooldownTime;
        }
    }
}
