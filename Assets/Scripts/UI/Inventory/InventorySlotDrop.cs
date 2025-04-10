using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 아이템이 드래그되었을 때 슬롯에 드롭되는 것을 처리하는 클래스.
/// </summary>
public class InventorySlotDrop : MonoBehaviour, IDropHandler
{
    /// <summary>
    /// 이 슬롯의 인덱스 번호 (InventoryManager의 슬롯 배열 기준).
    /// </summary>
    public int slotIndex;

    /// <summary>
    /// 인벤토리 매니저 참조.
    /// </summary>
    public InventoryManager inventoryManager;

    /// <summary>
    /// 드래그된 아이템이 이 슬롯에 드롭될 때 호출됨.
    /// 아이템 간의 위치를 교환하고 UI를 갱신한다.
    /// </summary>
    /// <param name="eventData">드롭 이벤트 데이터</param>
    public void OnDrop(PointerEventData eventData)
    {
        // 드래그 중인 아이템의 핸들러 가져오기
        var dragged = eventData.pointerDrag.GetComponent<InventoryDragHandler>();

        // 자기 자신과의 교환은 무시
        if (dragged != null && dragged.slotIndex != slotIndex)
        {
            // 드래그 출발지 및 도착지 인덱스
            var fromIndex = dragged.slotIndex;
            var toIndex = slotIndex;

            // 교환 대상 아이템 가져오기
            var fromItem = inventoryManager.GetIngredient(fromIndex);
            var toItem = inventoryManager.GetIngredient(toIndex);

            // 슬롯 간 아이템 교환
            inventoryManager.SetIngredient(fromIndex, toItem);
            inventoryManager.SetIngredient(toIndex, fromItem);

            inventoryManager.RefreshUI(); // <- 교환 후 UI 갱신
        }
    }
}
