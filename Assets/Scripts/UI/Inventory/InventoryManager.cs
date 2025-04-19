using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// 인벤토리의 UI 슬롯을 관리하고 아이템 데이터를 저장/갱신하는 매니저 클래스.
/// </summary>
public class InventoryManager : MonoBehaviour
{
    // 인스펙터에서 설정할 UI 슬롯 리스트 (ItemSlotUI는 background + icon을 포함한 슬롯 구조)
    public List<ItemSlotUI> itemSlots;

    // 슬롯마다 저장된 아이템 데이터 (ScriptableObject 기반)
    private IngredientSO[] savedItems;


    /// <summary>
    /// 초기화 함수: savedItems 배열 생성 및 UI 초기 상태로 세팅
    /// </summary>
    void Awake()
    {
        // 슬롯 개수만큼 저장 공간 확보
        savedItems = new IngredientSO[itemSlots.Count];

        // UI 새로고침 (초기에는 모두 빈 상태로 시작)
        RefreshUI();
    }

    /// <summary>
    /// 인벤토리 UI를 갱신. 아이템이 있으면 아이콘 표시, 없으면 아이콘 숨김.
    /// 배경 이미지는 항상 보이게 유지.
    public void RefreshUI()
    {
        for (int i = 0; i < itemSlots.Count; i++)
        {
            if (savedItems[i] != null)
            {
                // 아이템이 있는 경우: 아이콘 설정 및 표시
                itemSlots[i].icon.sprite = savedItems[i].icon;
                //itemSlots[i].icon.color = Color.white; // 완전 불투명하게
                itemSlots[i].icon.enabled = true;
            }
            else
            {
                // 아이템이 없는 경우: 아이콘 제거, 투명 처리 (Raycast를 위해 enabled는 true 유지)
                itemSlots[i].icon.sprite = null;
                //itemSlots[i].icon.color = new Color(1, 1, 1, 0); // 완전 투명하게
                itemSlots[i].icon.enabled = true;
                itemSlots[i].icon.gameObject.SetActive(true);
            }
            // 배경 이미지는 항상 표시 (비어 있어도 슬롯 형태가 유지되도록)
            if (itemSlots[i].background != null)
                itemSlots[i].background.enabled = true;
        }
    }

    /// <summary>
    /// 특정 슬롯에 아이템(IngredientSO)을 추가하고 UI를 갱신.
    /// </summary>
    public void AddIngredient(int slotIndex, IngredientSO newIngredient)
    {
        // 잘못된 슬롯 인덱스 또는 null 아이템은 무시
        if (slotIndex < 0 || slotIndex >= itemSlots.Count || newIngredient == null)
            return;

        // 아이템 데이터 저장
        savedItems[slotIndex] = newIngredient;

        // UI 갱신
        RefreshUI();
    }

    /// <summary>
    /// 특정 슬롯의 아이템을 제거하고 UI를 갱신.
    /// </summary>
    public void ClearSlot(int slotIndex)
    {
        // 잘못된 슬롯 인덱스는 무시
        if (slotIndex < 0 || slotIndex >= itemSlots.Count)
            return;

        // 해당 슬롯 아이템 제거
        savedItems[slotIndex] = null;

        // UI 갱신
        RefreshUI();
    }

    /// <summary>
    /// 특정 슬롯에 저장된 아이템 정보를 반환 (툴팁 등에서 활용 가능).
    /// </summary>
    public IngredientSO GetIngredient(int slotIndex)
    {
        // 범위 체크 후 반환
        if (slotIndex < 0 || slotIndex >= itemSlots.Count)
            return null;

        return savedItems[slotIndex];
    }

    /// <summary>
    /// 두 슬롯의 아이템 정보를 서로 교환합니다.
    /// </summary>
    /// <param name="from">교환할 첫 번째 슬롯 인덱스</param>
    /// <param name="to">교환할 두 번째 슬롯 인덱스</param>
    public void SwapIngredients(int from, int to)
    {
        // 슬롯 인덱스가 유효한지 확인
        if (from < 0 || from >= savedItems.Length || to < 0 || to >= savedItems.Length) return;

        // 두 슬롯의 아이템을 교환
        var temp = savedItems[from];
        savedItems[from] = savedItems[to];
        savedItems[to] = temp;

        // UI 갱신
        RefreshUI();
    }

    /// <summary>
    /// 지정된 슬롯에 새로운 아이템 정보를 설정합니다.
    /// </summary>
    /// <param name="slotIndex">설정할 슬롯의 인덱스</param>
    /// <param name="ingredient">슬롯에 넣을 IngredientSO</param>
    public void SetIngredient(int slotIndex, IngredientSO ingredient)
    {
        // 슬롯 인덱스가 유효한지 확인
        if (slotIndex < 0 || slotIndex >= savedItems.Length)
            return;

        // 슬롯에 아이템 설정
        savedItems[slotIndex] = ingredient;
    }


}
