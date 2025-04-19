using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ZombieCustomer : MonoBehaviour
{
    public DrinkSO orderedDrink;
    public DrinkSO[] possibleDrinks;

    public Transform plate;
    public Transform spawnPoint;
    public GameObject customerPrefab;
    public GameObject orderUI;
    public TextMeshProUGUI orderText;

    public float speed = 5f;
    private Rigidbody rb;
    public bool isCustomer = false;
    public bool isOrder = false;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        orderUI.SetActive(false);
    }

    void Update()
    {
        if (isCustomer == false )
        {
            float distance = Vector3.Distance(transform.position, plate.position);
            if(distance > 1.3f)
            {
                rb.MovePosition(transform.position + transform.forward * speed * Time.deltaTime);
            }
            if (distance < 1.3f)
            {
                isCustomer = true;
                isOrder = true;
                Instantiate(customerPrefab, spawnPoint.position, Quaternion.identity);
                transform.position = transform.position;
                float value = Random.value;
                if(value < 0.25f)
                    orderedDrink = possibleDrinks[0]; // Americano
                else if (value < 0.5f)
                    orderedDrink =  possibleDrinks[1]; // Latte
                else
                    orderedDrink =  possibleDrinks[2]; // Espresso
                ShowOrderUI(orderedDrink);
            }
        }
    }
    void ShowOrderUI(DrinkSO drink)
    {
        orderUI.SetActive(true);
        orderText.text = $"좀비가 {drink.name} 주문함!";
        Debug.Log($"좀비가 {drink.name} 주문함!");
    }
}
