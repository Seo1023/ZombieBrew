using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleMenuToggle : MonoBehaviour
{
    public GameObject menuPanel;     // �޴� UI �г�
    public Button closeButton;       // �ݱ� ��ư

    void Start()
    {
        if (closeButton != null)
        {
            closeButton.onClick.AddListener(CloseMenu); // ��ư�� �Լ� ����
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

