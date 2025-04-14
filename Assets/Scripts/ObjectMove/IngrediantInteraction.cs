using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngrediantInteraction : MonoBehaviour
{
    public InventoryManager inventoryManager;

    // 인스펙터에서 연결할 테스트용 Ingredient ScriptableObject (미리 만들어 둔 아이템)
    public IngredientSO coffeebean;
    public IngredientSO ice;
    public IngredientSO milk;
    public IngredientSO sugar;

    // 아이템을 넣을 슬롯 번호 (0번부터 시작)
    public int slotIndex = 0;
    public int slotIndex2 = 0;
    public int slotIndex3 = 0;
    public int slotIndex4 = 0;

    public GameObject ingredientUI;
    public Transform player;

    void Start()
    {
        ingredientUI.SetActive(false);
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance < 1.3 && Input.GetKeyDown(KeyCode.F))
        {
            ingredientUI.SetActive(!ingredientUI.activeSelf);
        }
        else if (distance > 1.3)
        {
            ingredientUI.SetActive(false);
        }
    }


    public void Addcoffeebean()
    {
        inventoryManager.AddIngredient(slotIndex, coffeebean);
    }

    public void AddIce()
    {
        inventoryManager.AddIngredient(slotIndex2, ice);
    }

    public void AddMilk()
    {
        inventoryManager.AddIngredient(slotIndex3, milk);
    }

    public void AddSugar()
    {
        inventoryManager.AddIngredient(slotIndex4, sugar);
    }
}
