using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public GameObject expPrefab;
    public GameObject coinPrefab;

    public int expValue = 10;
    public int goldValue = 5;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Die();
            DropItems();
            DropItems();
            DropItems();
            DropItems();
            DropItems();
            DropItems();
            DropItems();
            DropItems();
            DropItems();
            DropItems();
            DropItems();
        }
    }

    public void Die()
    {
        Debug.Log("���� ����");
        DropItems();
        Destroy(gameObject); // ���� ����
    }

    void DropItems()
    {
        Debug.Log("������ ��� ����");

        if (expPrefab == null) Debug.LogWarning("expPrefab�� ��� ����!");
        if (coinPrefab == null) Debug.LogWarning("coinPrefab�� ��� ����!");

        Vector3 basePosition = transform.position;

        // ����ġ ���
        if (expPrefab != null)
        {
            Vector3 expOffset = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));
            GameObject expDrop = Instantiate(expPrefab, basePosition + expOffset, Quaternion.identity);

            PickupItem expItem = expDrop.GetComponent<PickupItem>();
            if (expItem != null)
                expItem.value = expValue;

            expDrop.tag = "Exp";
        }

        // ���� ���
        if (coinPrefab != null)
        {
            Vector3 coinOffset = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));
            GameObject coinDrop = Instantiate(coinPrefab, basePosition + coinOffset, Quaternion.identity);

            PickupItem coinItem = coinDrop.GetComponent<PickupItem>();
            if (coinItem != null)
                coinItem.value = goldValue;

            coinDrop.tag = "Coin";
        }
    }
Vector3 GetGroundPosition(Vector3 origin)
    {
        Ray ray = new Ray(origin + Vector3.up * 10f, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, 20f))
        {
            return hit.point;
        }
        return origin;
    }
}

