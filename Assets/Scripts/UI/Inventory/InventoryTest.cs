using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������ ���۵Ǹ� �κ��丮�� �̸� ���ص� �������� �ִ� �׽�Ʈ ��ũ��Ʈ.
/// �κ��丮 �ý����� ����� �۵��ϴ��� Ȯ���ϱ� ���� ���.
/// </summary>
public class InventoryTest : MonoBehaviour
{
    // �ν����Ϳ��� ������ InventoryManager (�κ��丮�� ������ �����ϴ� Ŭ����)
    public InventoryManager inventoryManager;

    // �ν����Ϳ��� ������ �׽�Ʈ�� Ingredient ScriptableObject (�̸� ����� �� ������)
    public IngredientSO testIngredient;
    public IngredientSO testIngredient2;

    // �������� ���� ���� ��ȣ (0������ ����)
    public int slotIndex = 0; 
    public int slotIndex2 = 0; 

    void Start()
    {
        Debug.Log("InventoryManager: " + inventoryManager);
        Debug.Log("Test Ingredient: " + testIngredient);

        // ���� ���� �� �ش� ���Կ� �׽�Ʈ �������� �߰�
        inventoryManager.AddIngredient(slotIndex, testIngredient);
        inventoryManager.AddIngredient(slotIndex2, testIngredient2);
    }
}
