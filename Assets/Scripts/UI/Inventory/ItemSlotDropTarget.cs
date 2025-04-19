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

        // Ŀ�Ǹӽ� ���� ���Կ� ��� ��ġ
        targetSlot.SetItem(draggedIngredient);

        // �κ��丮���� ����
        inventory.ClearSlot(dragHandler.slotIndex);
    }
}
