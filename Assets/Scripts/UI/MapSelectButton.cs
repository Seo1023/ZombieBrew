using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MapSelectButton : MonoBehaviour
{
    public TMP_Text nameText;
    private SelectManager manager;
    private string sceneName;

    public GameObject highlight;

    public void Init(string sceneName, SelectManager manager)
    {
        this.sceneName = sceneName;
        this.manager = manager;

        nameText.text = sceneName;
        highlight.SetActive(false);

    }

    public void OnClick()
    {
        manager.SetMap(sceneName);
        manager.SetMapHighlight(this);
    }

    public void Sethighlight(bool on)
    {
        highlight.SetActive(on);
    }
}
