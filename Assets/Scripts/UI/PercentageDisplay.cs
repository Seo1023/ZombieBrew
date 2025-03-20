using System.Collections;
using TMPro;
using UnityEngine;

public class PercentageDisplay : MonoBehaviour
{
    public TMP_Text percentageText;  // UI에 표시할 TextMeshPro
    public float currentPercentage = 100f; // 100% 시작
    public float decreaseAmount = 5f; // 5%씩 감소
    public float interval = 4f; // 20초마다 감소

    void Start()
    {
        UpdatePercentageText();
        StartCoroutine(DecreasePercentage());
    }

    IEnumerator DecreasePercentage()
    {
        while (currentPercentage > 0)
        {
            yield return new WaitForSeconds(interval); // 20초 대기
            currentPercentage -= decreaseAmount; // 5% 감소

            if (currentPercentage < 0)
                currentPercentage = 0; // 최소값 제한

            UpdatePercentageText();
        }

        Debug.Log("0% 도달! 감소 종료.");
    }

    void UpdatePercentageText()
    {
        percentageText.text = $"{currentPercentage}%";
    }
}
