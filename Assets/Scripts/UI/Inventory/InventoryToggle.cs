using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryToggle : MonoBehaviour
{
    public GameObject inventoryPanel; // �ν����Ϳ��� ������ UI �г�
    public ToggleImage toggleImageScript;

    private bool isInventoryOpen = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            isInventoryOpen = !isInventoryOpen;
            inventoryPanel.SetActive(isInventoryOpen);

            if (!isInventoryOpen && toggleImageScript != null && toggleImageScript.hiddenImage != null)
            {
                toggleImageScript.hiddenImage.SetActive(false);
            }
        }
    }
}
