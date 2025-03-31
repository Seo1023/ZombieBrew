using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;

public class CoffeeMachine : MonoBehaviour
{
    public Transform Button;
    public ParticleSystem coffeeEffect;
    public Cup Cup;
    private bool isDispensing = false;
    private Coroutine fillCoroutine;

    void Start()
    {
        coffeeEffect.Stop();
        Cup.HideCoffee();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform == Button)
                {
                    ToggleDispensing();
                }
            }
        }
    }

    void ToggleDispensing()
    {
        if (!isDispensing)
        {
            coffeeEffect.Play();
            isDispensing = true;
            Cup.ShowCoffee();
            fillCoroutine = StartCoroutine(FillCup());
            Debug.Log("Ŀ�� ����� ����");
        }
        else
        {
            if (fillCoroutine != null)
            {
                StopCoroutine(fillCoroutine);
            }
            coffeeEffect.Stop();
            isDispensing = false;
            Debug.Log("Ŀ�� �ϼ�");
        }
    }

    IEnumerator FillCup()
    {
        float fillTime = 3f;
        float elapsedTime = 0f;

        while (elapsedTime < fillTime)
        {
            Cup.FillCoffee(Time.deltaTime / fillTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Debug.Log("���� ���� ��");
    }
}