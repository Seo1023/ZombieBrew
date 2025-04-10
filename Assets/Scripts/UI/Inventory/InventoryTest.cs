using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 게임이 시작되면 인벤토리에 미리 정해둔 아이템을 넣는 테스트 스크립트.
/// 인벤토리 시스템이 제대로 작동하는지 확인하기 위해 사용.
/// </summary>
public class InventoryTest : MonoBehaviour
{
    // 인스펙터에서 연결할 InventoryManager (인벤토리를 실제로 관리하는 클래스)
    public InventoryManager inventoryManager;

    // 인스펙터에서 연결할 테스트용 Ingredient ScriptableObject (미리 만들어 둔 아이템)
    public IngredientSO testIngredient;
    public IngredientSO testIngredient2;

    // 아이템을 넣을 슬롯 번호 (0번부터 시작)
    public int slotIndex = 0; 
    public int slotIndex2 = 0; 

    void Start()
    {
        Debug.Log("InventoryManager: " + inventoryManager);
        Debug.Log("Test Ingredient: " + testIngredient);

        // 게임 시작 시 해당 슬롯에 테스트 아이템을 추가
        inventoryManager.AddIngredient(slotIndex, testIngredient);
        inventoryManager.AddIngredient(slotIndex2, testIngredient2);
    }
}
