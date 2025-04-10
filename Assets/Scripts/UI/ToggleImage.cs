using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleImage : MonoBehaviour
{
    public GameObject hiddenImage; // Inspector에서 연결할 이미지

    void Start()
    {
        if (hiddenImage != null)
            hiddenImage.SetActive(false); // 시작할 때 숨김
    }

    public void ToggleImageVisibility()
    {
        if (hiddenImage != null)
            hiddenImage.SetActive(!hiddenImage.activeSelf); // 상태 반전
    }
}
