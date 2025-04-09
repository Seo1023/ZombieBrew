using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClockHand : MonoBehaviour
{
    public RectTransform handTransform;         // ��ħ
    public GameObject yellowBackground;         // ��� 1
    public GameObject orangeBackground;         // ��� 2
    public GameObject redBackground;            // ��� 3

    public float gameTimeStep = 2f;             // ���� �ð� ���� ���� (2��)
    public float realTimeTotal = 300f;          // ���� �ð� 5��
    public int maxGameHours = 10;               // 10�ð�
    public string nextSceneName = "NextScene";

    [Range(0f, 1f)] public float orangeThreshold = 0.33f; // 1/3 ����
    [Range(0f, 1f)] public float redThreshold = 0.66f;    // 2/3 ����

    private float gameTime = 0f;
    private bool orangeSet = false;
    private bool redSet = false;

    void Start()
    {
        // �ʱ� ����: ��� ��游 �ѱ�
        yellowBackground.SetActive(true);
        orangeBackground.SetActive(false);
        redBackground.SetActive(false);

        StartCoroutine(RotateClockHand());
    }

    IEnumerator RotateClockHand()
    {
        float totalGameMinutes = maxGameHours * 60f;
        int steps = (int)(totalGameMinutes / gameTimeStep);
        float realInterval = realTimeTotal / steps;

        while (gameTime < totalGameMinutes)
        {
            yield return new WaitForSeconds(realInterval);

            gameTime += gameTimeStep;

            // ��ħ ȸ��
            float rotation = (gameTime / totalGameMinutes) * 360f;
            handTransform.rotation = Quaternion.Euler(0, 0, -rotation);

            // ��� ��ü
            float progress = gameTime / totalGameMinutes;

            if (!orangeSet && progress >= orangeThreshold)
            {
                yellowBackground.SetActive(false);
                orangeBackground.SetActive(true);
                orangeSet = true;
            }
            else if (!redSet && progress >= redThreshold)
            {
                orangeBackground.SetActive(false);
                redBackground.SetActive(true);
                redSet = true;
            }
        }

        Debug.Log("�ð� ����, �� �̵�");
        SceneManager.LoadScene(nextSceneName);
    }
}
