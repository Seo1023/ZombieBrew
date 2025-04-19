using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlotDropTarget : MonoBehaviour, IDropHandler
{
    public ItemSlotUI targetSlot;

    public void OnDrop(PointerEventData eventData)
    {
        var dragHandler = eventData.pointerDrag.GetComponent<InventoryDragHandler>();
        if (dragHandler == null) return;

        var inventory = FindObjectOfType<InventoryManager>();
        if (inventory == null) return;

        IngredientSO draggedIngredient = inventory.GetIngredient(dragHandler.slotIndex);
        if (draggedIngredient == null) return;

        // 커피머신 조합 슬롯에 재료 배치
        targetSlot.SetItem(draggedIngredient);

        // 인벤토리에서 제거
        inventory.ClearSlot(dragHandler.slotIndex);
    }
}
