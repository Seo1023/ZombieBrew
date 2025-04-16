using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class FarmingObject : MonoBehaviour
{
    public bool isTake = false;
    public GameObject[] ingredient = new GameObject[4];
    public Transform player;

    public InventoryManager inventoryManager;

    // 인스펙터에서 연결할 테스트용 Ingredient ScriptableObject (미리 만들어 둔 아이템)
    public IngredientSO chocolate;
    public IngredientSO mint;
    public IngredientSO uniqueflower;
    public IngredientSO butter;

    // 아이템을 넣을 슬롯 번호 (0번부터 시작)
    public int slotIndex = 0;
    public int slotIndex2 = 0;
    public int slotIndex3 = 0;
    public int slotIndex4 = 0;

    public GameObject errorUI;
    public TextMeshProUGUI text;

    void Start()
    {
        errorUI.SetActive(false);
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        if(distance < 1.3 && Input.GetKeyDown(KeyCode.F))
        {
            isTake = true;
            float value = Random.value;
            if(value < 0.25f)
            {
                inventoryManager.AddIngredient(slotIndex, chocolate);
            } 
            //else if(value < 0.5f)
            //{
            //    inventoryManager.AddIngredient(slotIndex2.mint);
            //} 
            //else if(value < 0.75f)
            //{
            //    inventoryManager.AddIngredient(slotIndex3.uniqueflower);
            //} 
            //else if(value < 1f)
            //{
            //    inventoryManager.AddIngredient(slotIndex4.butter);
            //}
        }
        if (isTake)
        {
            errorUI.SetActive(true);
            text.text = "이 요소는 이미 파밍된 요소입니다.";
        } 
        
    }

    public void CloseUI()
    {
        errorUI.SetActive(false);
    }
}
