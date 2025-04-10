using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// �������� �巡�׵Ǿ��� �� ���Կ� ��ӵǴ� ���� ó���ϴ� Ŭ����.
/// </summary>
public class InventorySlotDrop : MonoBehaviour, IDropHandler
{
    /// <summary>
    /// �� ������ �ε��� ��ȣ (InventoryManager�� ���� �迭 ����).
    /// </summary>
    public int slotIndex;

    /// <summary>
    /// �κ��丮 �Ŵ��� ����.
    /// </summary>
    public InventoryManager inventoryManager;

    /// <summary>
    /// �巡�׵� �������� �� ���Կ� ��ӵ� �� ȣ���.
    /// ������ ���� ��ġ�� ��ȯ�ϰ� UI�� �����Ѵ�.
    /// </summary>
    /// <param name="eventData">��� �̺�Ʈ ������</param>
    public void OnDrop(PointerEventData eventData)
    {
        // �巡�� ���� �������� �ڵ鷯 ��������
        var dragged = eventData.pointerDrag.GetComponent<InventoryDragHandler>();

        // �ڱ� �ڽŰ��� ��ȯ�� ����
        if (dragged != null && dragged.slotIndex != slotIndex)
        {
            // �巡�� ����� �� ������ �ε���
            var fromIndex = dragged.slotIndex;
            var toIndex = slotIndex;

            // ��ȯ ��� ������ ��������
            var fromItem = inventoryManager.GetIngredient(fromIndex);
            var toItem = inventoryManager.GetIngredient(toIndex);

            // ���� �� ������ ��ȯ
            inventoryManager.SetIngredient(fromIndex, toItem);
            inventoryManager.SetIngredient(toIndex, fromItem);

            inventoryManager.RefreshUI(); // <- ��ȯ �� UI ����
        }
    }
}
