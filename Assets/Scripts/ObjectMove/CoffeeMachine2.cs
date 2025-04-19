using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeMachine2 : MonoBehaviour
{
    public bool isBrewing = false; //Ŀ�Ǹӽ� �۵� ����
    public GameObject inventoryPanel; //�κ��丮 UI
    public GameObject craftingPanel; //���� UI
    public Transform player;  //�÷��̾� ��ġ

    public CoffeeMachineLogic logic;

    //���� �� UI ��Ȱ��ȭ
    void Start()
    {
        inventoryPanel.SetActive(false);
        craftingPanel.SetActive(false);
    }

    public void Inventory()
    {
        
    }

    //���� ��ư ������ �� ���� �Ǹ鼭 �κ��丮 �ȿ� �ִ� ������ ���� �� Ŀ�� ����
    public void Brewing()
    {
        if (logic.TryExtract())
        {
            Debug.Log("Ŀ�� ���� �Ϸ�");
            ToggleCraftingUI(false);
        }
        else
        {
            Debug.Log("�ùٸ� ��ᰡ �ƴմϴ�.");
        }
    }

    public void Combine()
    {

    }

    // F�� ��ȣ�ۿ� �� UI Ű�� ��
    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        // F Ű�� ���� �ݱ�
        if (Input.GetKeyDown(KeyCode.F))
        {
            // �÷��̾ ����� ���� �� �� ����
            if (distance < 1.3f)
            {
                bool showUI = !inventoryPanel.activeSelf;
                ToggleCraftingUI(showUI);
            }
        }

        // UI�� ���� �ְ�, �Ÿ��� �־����� ���� �ڵ����� �ݱ�
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
