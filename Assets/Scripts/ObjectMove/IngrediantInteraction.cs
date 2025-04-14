using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngrediantInteraction : MonoBehaviour
{
    public InventoryManager inventoryManager;

    // �ν����Ϳ��� ������ �׽�Ʈ�� Ingredient ScriptableObject (�̸� ����� �� ������)
    public IngredientSO coffeebean;
    public IngredientSO ice;
    public IngredientSO milk;
    public IngredientSO sugar;

    // �������� ���� ���� ��ȣ (0������ ����)
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
