using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cup : MonoBehaviour
{
    public Transform coffeeLiquid;
    private float maxFillHeight = 0.15f;
    private Vector3 initialScale;

    void Start()
    {
        initialScale = coffeeLiquid.localScale;
        HideCoffee();
    }

    public void FillCoffee(float fillAmount)
    {
        float newHeight = Mathf.Clamp(coffeeLiquid.localScale.y + fillAmount * maxFillHeight, 0, maxFillHeight);
        coffeeLiquid.localScale = new Vector3(initialScale.x, newHeight, initialScale.z);
        coffeeLiquid.position = new Vector3(coffeeLiquid.position.x, Mathf.Max(coffeeLiquid.position.y, -0.18f), coffeeLiquid.position.z);
    }

    public void HideCoffee()
    {
        coffeeLiquid.gameObject.SetActive(false);
    }

    public void ShowCoffee()
    {
        coffeeLiquid.gameObject.SetActive(true);
    }
}

