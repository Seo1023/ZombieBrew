using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickupSystem : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Exp"))
        {
            var pickup = other.GetComponent<PickupItem>();
            if (pickup != null)
                GameManager.Instance.AddExp(pickup.value);

            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Coin"))
        {
            var pickup = other.GetComponent<PickupItem>();
            if (pickup != null)
                GameManager.Instance.AddGold(pickup.value);

            Destroy(other.gameObject);
        }
    }
}

