using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleImage : MonoBehaviour
{
    public GameObject hiddenImage; // Inspector���� ������ �̹���

    void Start()
    {
        if (hiddenImage != null)
            hiddenImage.SetActive(false); // ������ �� ����
    }

    public void ToggleImageVisibility()
    {
        if (hiddenImage != null)
            hiddenImage.SetActive(!hiddenImage.activeSelf); // ���� ����
    }
}
