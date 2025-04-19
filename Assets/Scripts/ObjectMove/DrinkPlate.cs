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
                Debug.Log("���� ��ġ! ���� ����");
                GiveReward(zombie.orderedDrink.price);
                Destroy(other.gameObject);
            }
            else
            {
                Debug.Log("���ᰡ Ʋ��");
            }
        }
    }

    void GiveReward(int amount)
    {
        // �÷��̾� ��ȭ ����
        Debug.Log($"{amount} ��� ȹ��!");
        FindObjectOfType<PlayerMoney>().AddGold(amount);
    }
}