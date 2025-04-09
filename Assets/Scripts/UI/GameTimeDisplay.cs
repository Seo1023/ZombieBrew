using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClockHand : MonoBehaviour
{
    public RectTransform handTransform;         // 초침
    public GameObject yellowBackground;         // 배경 1
    public GameObject orangeBackground;         // 배경 2
    public GameObject redBackground;            // 배경 3

    public float gameTimeStep = 2f;             // 게임 시간 증가 단위 (2분)
    public float realTimeTotal = 300f;          // 현실 시간 5분
    public int maxGameHours = 10;               // 10시간
    public string nextSceneName = "NextScene";

    [Range(0f, 1f)] public float orangeThreshold = 0.33f; // 1/3 지점
    [Range(0f, 1f)] public float redThreshold = 0.66f;    // 2/3 지점

    private float gameTime = 0f;
    private bool orangeSet = false;
    private bool redSet = false;

    void Start()
    {
        // 초기 상태: 노란 배경만 켜기
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

            // 초침 회전
            float rotation = (gameTime / totalGameMinutes) * 360f;
            handTransform.rotation = Quaternion.Euler(0, 0, -rotation);

            // 배경 교체
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

        Debug.Log("시간 종료, 씬 이동");
        SceneManager.LoadScene(nextSceneName);
    }
}
