using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleMenuToggle : MonoBehaviour
{
    public GameObject menuPanel;     // 메뉴 UI 패널
    public Button closeButton;       // 닫기 버튼

    void Start()
    {
        if (closeButton != null)
        {
            closeButton.onClick.AddListener(CloseMenu); // 버튼에 함수 연결
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
        }
    }

    public void ToggleMenu()
    {
        if (menuPanel != null)
        {
            bool isActive = menuPanel.activeSelf;
            menuPanel.SetActive(!isActive);
        }
    }

    public void CloseMenu()
    {
        if (menuPanel != null)
        {
            menuPanel.SetActive(false);
        }
    }
}

