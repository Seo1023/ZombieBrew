using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// 인벤토리 슬롯에서 아이템을 드래그할 수 있게 해주는 컴포넌트.
/// </summary>
public class InventoryDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    /// <summary>
    /// 현재 이 핸들러가 연결된 슬롯의 인덱스.
    /// </summary>
    public int slotIndex;

    private InventoryManager inventoryManager;
    private RectTransform canvasRect;
    private Image draggedIcon;// 드래그 중인 임시 아이콘

    /// <summary>
    /// 초기화: 인벤토리 매니저와 캔버스 RectTransform 참조를 가져옴.
    /// </summary>
    void Awake()
    {
        inventoryManager = FindObjectOfType<InventoryManager>();
        canvasRect = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
    }

    /// <summary>
    /// 드래그 시작 시 호출됨. 드래그할 아이템의 임시 아이콘을 생성함.
    /// </summary>
    /// <param name="eventData">이벤트 데이터</param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        var item = inventoryManager.GetIngredient(slotIndex);
        if (item == null) return;

        // 드래그용 임시 아이콘 생성
        draggedIcon = new GameObject("DraggedIcon").AddComponent<Image>();
        draggedIcon.transform.SetParent(canvasRect); // 캔버스 안에 생성
        draggedIcon.sprite = item.icon;// 다른 UI 이벤트에 방해되지 않도록 설정
        draggedIcon.rectTransform.sizeDelta = new Vector2(50, 50); // 아이콘 크기 설정
    }

    /// <summary>
    /// 드래그 중 호출됨. 아이콘이 마우스를 따라다님.
    /// </summary>
    /// <param name="eventData">이벤트 데이터</param>
    public void OnDrag(PointerEventData eventData)
    {
        if (draggedIcon != null)
        {
            draggedIcon.transform.position = eventData.position;
        }
    }

    /// <summary>
    /// 드래그 종료 시 호출됨. 슬롯 위에 드롭되면 아이템을 교환하고, 아니면 아이콘 삭제.
    /// </summary>
    /// <param name="eventData">이벤트 데이터</param>
    public void OnEndDrag(PointerEventData eventData)
    {
        Destroy(draggedIcon?.gameObject);// 드래그 아이콘 제거

        // 슬롯 위에 드롭했는지 검사
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        foreach (var result in results)
        {
            var drop = result.gameObject.GetComponent<InventorySlotDrop>();
            if (drop != null)
            {
                // 드롭된 슬롯과 현재 슬롯의 아이템을 교환
                inventoryManager.SwapIngredients(slotIndex, drop.slotIndex);
                return;
            }
        }

        // 아무 슬롯에도 안 떨어졌으면 아무 일도 하지 않음 (원래 자리 유지)
    }
}

