using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderControl : MonoBehaviour
{
    public Slider slider; // �����տ��� ������ �����̴� ������Ʈ
    public float speed = 0.5f; // �����̴��� �����ϴ� �ӵ� (1�ʿ� �󸶳� ��������)

    private float currentValue = 0f;

    void Start()
    {
        if (slider == null)
        {
            slider = GetComponent<Slider>();
        }

        slider.value = 0f; // ���� �� 0����
    }

    void Update()
    {
        if (currentValue < 1f)
        {
            currentValue += Time.deltaTime * speed;
            currentValue = Mathf.Clamp01(currentValue); // 1 �̻� �ö��� �ʰ�
            slider.value = currentValue;
        }
    }
}
