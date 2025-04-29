using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    public Slider slider;

    public void SetMaxHealth(int value)
    {
        slider.maxValue = value;
        slider.value = value;
    }

    public void SetHealth(int value)
    {
        slider.value = value;
    }

    void LateUpdate()
    {
        if (Camera.main != null)
        {
            transform.rotation = Camera.main.transform.rotation;
        }
    }
}

