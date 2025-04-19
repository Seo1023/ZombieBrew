using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMoney : MonoBehaviour
{
    public int gold = 0;
    public TextMeshProUGUI goldText;

    // Start is called before the first frame update
    void Start()
    {
        UpdateGoldUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddGold(int amount)
    {
        gold += amount;
        UpdateGoldUI();
    }

    public bool SpendGold(int amount)
    {
        if (gold >= amount)
        {
            gold -= amount;
            UpdateGoldUI();
            return true;
        } else
        {
            Debug.Log("돈이 부족합니다.");
            return false;
        }
    }

    void UpdateGoldUI()
    {
        if(goldText != null)
        {
            goldText.text = $"Gold : {gold}";
        }
    }
}
