using System.Collections;
using TMPro;
using UnityEngine;

public class GameTimeDisplay : MonoBehaviour
{
    public TMP_Text timeText;  // ���� ȭ�鿡 ǥ���� TextMeshPro UI
    public float gameTime = 480f; // 08:00 AM (480��)
    public float gameHourIncrease = 60f; // ���ӿ��� 1�ð� (60��)
    public float realTimeInterval = 2.4f; // ���� 2.4�ʸ��� 1�ð� ����
    public int maxHours = 12; // 12�ð�(08:00 �� 20:00) ǥ�� �� ����

    void Start()
    {
        UpdateTimeText();
        StartCoroutine(UpdateGameTime());
    }

    IEnumerator UpdateGameTime()
    {
        for (int i = 0; i < maxHours; i++)
        {
            yield return new WaitForSeconds(realTimeInterval); // 2.4�� ���
            gameTime += gameHourIncrease; // ���� �ð� 1�ð� ����
            UpdateTimeText();
        }

        Debug.Log("�ð� �帧 ����.");
    }

    void UpdateTimeText()
    {
        int hours = (int)(gameTime / 60);
        int minutes = (int)(gameTime % 60);
        timeText.text = $"{hours:D2}:{minutes:D2}";
    }
}
