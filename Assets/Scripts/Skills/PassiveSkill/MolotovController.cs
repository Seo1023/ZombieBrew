using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MolotovController : MonoBehaviour
{
    public GameObject fireEffectPrefab;
    private SkillLevelData levelData;

    public void SetData(SkillLevelData data)
    {
        levelData = data;
    }

    private void Start()
    {
        StartCoroutine(FallAndExplode());
    }

    private IEnumerator FallAndExplode()
    {
        Vector3 start = transform.position;
        Vector3 end = new Vector3(start.x, 0.2f, start.z);
        float duration = 0.4f;
        float t = 0f;

        while (t < 1f)
        {
            transform.position = Vector3.Lerp(start, end, t);
            t += Time.deltaTime / duration;
            yield return null;
        }

        transform.position = end;

        if (fireEffectPrefab != null)
        {
            GameObject fire = Instantiate(fireEffectPrefab, end, Quaternion.Euler(270f, 0, 0));
            var fireArea = fire.GetComponent<MolotovEffect>();
            if (fireArea != null)
            {
                fireArea.Initialize(levelData.damage, levelData.range, levelData.duration);
            }
        }

        Destroy(gameObject); // 화염병 본체 제거
    }
}
