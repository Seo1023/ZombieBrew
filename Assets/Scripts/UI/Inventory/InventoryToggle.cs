using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryToggle : MonoBehaviour
{
    public GameObject inventoryPanel; // 인스펙터에서 연결할 UI 패널
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
