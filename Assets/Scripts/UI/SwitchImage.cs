using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchImage : MonoBehaviour
{
    public GameObject SelectChracterButton; 
    public GameObject[] scrolleViews; 

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
        OnButtonClicked(SelectChracterButton); 
    }
}
