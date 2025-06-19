using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Passive/Molotov")]
public class MolotovSkillSO : PassiveSkillSO
{
    public GameObject molotovPrefab;
    public float spawnRadius = 10f;

    public override void Activate(GameObject caster, Vector3 targetPosition, int level)
    {
        if (molotovPrefab == null) return;

        Vector3 randomPos = caster.transform.position + new Vector3(
            Random.Range(-spawnRadius, spawnRadius), 0, Random.Range(-spawnRadius, spawnRadius));

        GameObject molotov = Instantiate(molotovPrefab, randomPos + Vector3.up * 10f, Quaternion.identity);

        var controller = molotov.GetComponent<MolotovController>();
        if (controller != null)
        {
            controller.SetData(GetLevelData(level));
        }

        Debug.Log($"[Molotov] Lv{level} »ý¼ºµÊ ¡æ À§Ä¡: {randomPos}");
    }
}
