using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerPickupSystem : MonoBehaviour
{
    public int exp = 0;
    public int coin = 0;

    public TextMeshProUGUI expText;
    public TextMeshProUGUI coinText;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger with: " + other.gameObject.name);

        if (other.CompareTag("Exp"))
        {
            exp += 1;
            UpdateUI();
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Coin"))
        {
            coin += 1;
            UpdateUI();
            Destroy(other.gameObject);
        }
    }


    void UpdateUI()
    {
        expText.text = "EXP: " + exp;
        coinText.text = "COIN: " + coin;
    }
}
