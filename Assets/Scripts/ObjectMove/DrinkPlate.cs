using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DrinkPlate : MonoBehaviour
{
    public DrinkSO placedDrink;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Zombie") && Input.GetKeyDown(KeyCode.F))
        {
            ZombieCustomer zombie = other.GetComponent<ZombieCustomer>();

            if (zombie != null && zombie.orderedDrink == placedDrink)
            {
                Debug.Log("음료 일치! 보상 지급");
                GiveReward(zombie.orderedDrink.price);
                Destroy(other.gameObject);
            }
            else
            {
                Debug.Log("음료가 틀림");
            }
        }
    }

    void GiveReward(int amount)
    {
        // 플레이어 재화 증가
        Debug.Log($"{amount} 골드 획득!");
        FindObjectOfType<PlayerMoney>().AddGold(amount);
    }
}