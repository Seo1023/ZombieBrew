using System.Collections;
using TMPro;
using UnityEngine;

public class PercentageDisplay : MonoBehaviour
{
    public TMP_Text percentageText;  // UI�� ǥ���� TextMeshPro
    public float currentPercentage = 100f; // 100% ����
    public float decreaseAmount = 5f; // 5%�� ����
    public float interval = 4f; // 20�ʸ��� ����

    void Start()
    {
        UpdatePercentageText();
        StartCoroutine(DecreasePercentage());
    }

    IEnumerator DecreasePercentage()
    {
        while (currentPercentage > 0)
        {
            yield return new WaitForSeconds(interval); // 20�� ���
            currentPercentage -= decreaseAmount; // 5% ����

            if (currentPercentage < 0)
                currentPercentage = 0; // �ּҰ� ����

            UpdatePercentageText();
        }

        Debug.Log("0% ����! ���� ����.");
    }

    void UpdatePercentageText()
    {
        percentageText.text = $"{currentPercentage}%";
    }
}
