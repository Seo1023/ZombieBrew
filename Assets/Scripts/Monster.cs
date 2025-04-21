using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public GameObject expPrefab;
    public GameObject coinPrefab;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Die();
        }
    }

    public void Die()
    {
        DropItems();
        Destroy(gameObject); // ���� ����
    }

    void DropItems()
    {
        Vector3 basePosition = transform.position;

        // ����ġ ���
        Vector3 expOffset = new Vector3(Random.Range(-5f, 5f), 0f, Random.Range(-5f, 5f));
        Vector3 expDropPos = GetGroundPosition(basePosition + expOffset);
        expDropPos.y += 1f;
        Instantiate(expPrefab, expDropPos, Quaternion.identity);

        // ���� ���
        Vector3 coinOffset = new Vector3(Random.Range(-5f, 5f), 0f, Random.Range(-5f, 5f));
        Vector3 coinDropPos = GetGroundPosition(basePosition + coinOffset);
        coinDropPos.y += 1f;
        Instantiate(coinPrefab, coinDropPos, Quaternion.identity);
    }


    Vector3 GetGroundPosition(Vector3 origin)
    {
        Ray ray = new Ray(origin + Vector3.up * 10f, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, 20f))
        {
            return hit.point;
        }
        return origin; // Ȥ�� ������ ���� ��� �׳� ���� ��ġ
    }

}
