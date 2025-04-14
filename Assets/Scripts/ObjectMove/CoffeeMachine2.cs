using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeMachine2 : MonoBehaviour
{
    public bool isBrewing = false; //Ŀ�Ǹӽ� �۵� ����
    public GameObject inventory; //�κ��丮 UI
    public GameObject combineUI; //���� UI
    public Transform player;  //�÷��̾� ��ġ

    //���� �� UI ��Ȱ��ȭ
    void Start()
    {
        inventory.SetActive(false);
        combineUI.SetActive(false);
    }

    public void Inventory()
    {
        
    }

    //���� ��ư ������ �� ���� �Ǹ鼭 �κ��丮 �ȿ� �ִ� ������ ���� �� Ŀ�� ����
    public void Brewing()
    {
        
    }

    // F�� ��ȣ�ۿ� �� UI Ű�� ��
    void Update()
    {
        float distance = Vector3.Distance(transform.position , player.position);
        if(distance < 2 && Input.GetKeyDown(KeyCode.F))
        {
            inventory.SetActive(!inventory.activeSelf);
            combineUI.SetActive(!combineUI.activeSelf);
        }
    }


}
