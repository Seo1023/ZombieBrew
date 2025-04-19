using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeMachine2 : MonoBehaviour
{
    public bool isBrewing = false; //커피머신 작동 상태
    public GameObject inventory; //인벤토리 UI
    public GameObject combineUI; //조합 UI
    public Transform player;  //플레이어 위치

    //시작 시 UI 비활성화
    void Start()
    {
        inventory.SetActive(false);
        combineUI.SetActive(false);
    }

    public void Inventory()
    {
        
    }

    //추출 버튼 눌렀을 때 추출 되면서 인벤토리 안에 있는 아이템 제거 및 커피 제작
    public void Brewing()
    {
        
    }

    public void Combine()
    {

    }

    // F로 상호작용 시 UI 키고 끔
    void Update()
    {
        float distance = Vector3.Distance(transform.position , player.position);
        if(distance < 1.3 && Input.GetKeyDown(KeyCode.F))
        {
            inventory.SetActive(!inventory.activeSelf);
            combineUI.SetActive(!combineUI.activeSelf);
        } else if (distance > 1.3)
        {
            inventory.SetActive(false);
            combineUI.SetActive(false);
        }
    }


}
