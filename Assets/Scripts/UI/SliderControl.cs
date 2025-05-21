using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderControl : MonoBehaviour
{
    public Slider slider; // 프리팹에서 연결할 슬라이더 컴포넌트
    public float speed = 0.5f; // 슬라이더가 증가하는 속도 (1초에 얼마나 증가할지)

    private float currentValue = 0f;

    void Start()
    {
        if (slider == null)
        {
            slider = GetComponent<Slider>();
        }

        slider.value = 0f; // 시작 시 0부터
    }

    void Update()
    {
        if (currentValue < 1f)
        {
            currentValue += Time.deltaTime * speed;
            currentValue = Mathf.Clamp01(currentValue); // 1 이상 올라가지 않게
            slider.value = currentValue;
        }
    }
}
