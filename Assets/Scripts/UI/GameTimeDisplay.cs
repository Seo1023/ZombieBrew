using System.Collections;
using TMPro;
using UnityEngine;

public class GameTimeDisplay : MonoBehaviour
{
    public TMP_Text timeText;  // 게임 화면에 표시할 TextMeshPro UI
    public float gameTime = 480f; // 08:00 AM (480분)
    public float gameHourIncrease = 60f; // 게임에서 1시간 (60분)
    public float realTimeInterval = 2.4f; // 현실 2.4초마다 1시간 증가
    public int maxHours = 12; // 12시간(08:00 → 20:00) 표시 후 종료

    void Start()
    {
        UpdateTimeText();
        StartCoroutine(UpdateGameTime());
    }

    IEnumerator UpdateGameTime()
    {
        for (int i = 0; i < maxHours; i++)
        {
            yield return new WaitForSeconds(realTimeInterval); // 2.4초 대기
            gameTime += gameHourIncrease; // 게임 시간 1시간 증가
            UpdateTimeText();
        }

        Debug.Log("시간 흐름 종료.");
    }

    void UpdateTimeText()
    {
        int hours = (int)(gameTime / 60);
        int minutes = (int)(gameTime % 60);
        timeText.text = $"{hours:D2}:{minutes:D2}";
    }
}
