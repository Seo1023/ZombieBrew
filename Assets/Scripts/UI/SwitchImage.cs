using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchImage : MonoBehaviour
{
    public GameObject ingredientButton; // Inspector에서 넣기
    public GameObject[] scrolleViews; // 모든 ScrollView들을 넣기

    public void OnButtonClicked(GameObject clickedButton)
    {
        string targetName = clickedButton.name.Replace("Button", "Scrolleview");

        foreach (GameObject view in scrolleViews)
        {
            view.SetActive(view.name == targetName);
        }
    }

    void Start()
    {
        OnButtonClicked(ingredientButton); // 시작 시 기본 선택
    }
}
