using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public GameObject expPrefab;
    public GameObject coinPrefab;

    public int expValue = 10;
    public int goldValue = 5;

    public void DropItems()
    {
        Vector3 basePosition = transform.position;

        if (expPrefab != null)
        {
            Vector3 expOffset = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));
            GameObject expDrop = Instantiate(expPrefab, basePosition + expOffset, Quaternion.identity);
            PickupItem expItem = expDrop.GetComponent<PickupItem>();
            if (expItem != null)
                expItem.value = expValue;

            expDrop.tag = "Exp";
        }

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
}


