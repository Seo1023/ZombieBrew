using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// �κ��丮 ���Կ��� �������� �巡���� �� �ְ� ���ִ� ������Ʈ.
/// </summary>
public class InventoryDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    /// <summary>
    /// ���� �� �ڵ鷯�� ����� ������ �ε���.
    /// </summary>
    public int slotIndex;

    private InventoryManager inventoryManager;
    private RectTransform canvasRect;
    private Image draggedIcon;// �巡�� ���� �ӽ� ������

    /// <summary>
    /// �ʱ�ȭ: �κ��丮 �Ŵ����� ĵ���� RectTransform ������ ������.
    /// </summary>
    void Awake()
    {
        inventoryManager = FindObjectOfType<InventoryManager>();
        canvasRect = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
    }

    /// <summary>
    /// �巡�� ���� �� ȣ���. �巡���� �������� �ӽ� �������� ������.
    /// </summary>
    /// <param name="eventData">�̺�Ʈ ������</param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        var item = inventoryManager.GetIngredient(slotIndex);
        if (item == null) return;

        // �巡�׿� �ӽ� ������ ����
        draggedIcon = new GameObject("DraggedIcon").AddComponent<Image>();
        draggedIcon.transform.SetParent(canvasRect); // ĵ���� �ȿ� ����
        draggedIcon.sprite = item.icon;// �ٸ� UI �̺�Ʈ�� ���ص��� �ʵ��� ����
        draggedIcon.rectTransform.sizeDelta = new Vector2(50, 50); // ������ ũ�� ����
    }

    /// <summary>
    /// �巡�� �� ȣ���. �������� ���콺�� ����ٴ�.
    /// </summary>
    /// <param name="eventData">�̺�Ʈ ������</param>
    public void OnDrag(PointerEventData eventData)
    {
        if (draggedIcon != null)
        {
            draggedIcon.transform.position = eventData.position;
        }
    }

    /// <summary>
    /// �巡�� ���� �� ȣ���. ���� ���� ��ӵǸ� �������� ��ȯ�ϰ�, �ƴϸ� ������ ����.
    /// </summary>
    /// <param name="eventData">�̺�Ʈ ������</param>
    public void OnEndDrag(PointerEventData eventData)
    {
        Destroy(draggedIcon?.gameObject);// �巡�� ������ ����

        // ���� ���� ����ߴ��� �˻�
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        foreach (var result in results)
        {
            var drop = result.gameObject.GetComponent<InventorySlotDrop>();
            if (drop != null)
            {
                // ��ӵ� ���԰� ���� ������ �������� ��ȯ
                inventoryManager.SwapIngredients(slotIndex, drop.slotIndex);
                return;
            }
        }

        // �ƹ� ���Կ��� �� ���������� �ƹ� �ϵ� ���� ���� (���� �ڸ� ����)
    }
}

