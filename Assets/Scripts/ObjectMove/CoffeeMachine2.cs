using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeMachine2 : MonoBehaviour
{
    public bool isBrewing = false; //커피머신 작동 상태
    public GameObject inventoryPanel; //인벤토리 UI
    public GameObject craftingPanel; //조합 UI
    public Transform player;  //플레이어 위치

    public CoffeeMachineLogic logic;

    //시작 시 UI 비활성화
    void Start()
    {
        inventoryPanel.SetActive(false);
        craftingPanel.SetActive(false);
    }

    public void Inventory()
    {
        
    }

    //추출 버튼 눌렀을 때 추출 되면서 인벤토리 안에 있는 아이템 제거 및 커피 제작
    public void Brewing()
    {
        if (logic.TryExtract())
        {
            Debug.Log("커피 제작 완료");
            ToggleCraftingUI(false);
        }
        else
        {
            Debug.Log("올바른 재료가 아닙니다.");
        }
    }

    public void Combine()
    {

    }

    // F로 상호작용 시 UI 키고 끔
    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        // F 키로 열고 닫기
        if (Input.GetKeyDown(KeyCode.F))
        {
            // 플레이어가 가까울 때만 열 수 있음
            if (distance < 1.3f)
            {
                bool showUI = !inventoryPanel.activeSelf;
                ToggleCraftingUI(showUI);
            }
        }

        // UI가 켜져 있고, 거리가 멀어졌을 때만 자동으로 닫기
        if (inventoryPanel.activeSelf && distance > 1.8f)
        {
            ToggleCraftingUI(false);
        }
    }



    void ToggleCraftingUI(bool isActive)
    {
        inventoryPanel.SetActive(isActive);
        craftingPanel.SetActive(isActive);
    }

}
