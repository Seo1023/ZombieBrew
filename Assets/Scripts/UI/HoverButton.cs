using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoverButton : MonoBehaviour
{
    public Button bt;
    public Color cl;
    private Color originalColor;
    private ColorBlock cb;

    // Start is called before the first frame update
    void Start()
    {
        cb = bt.colors;
        originalColor = cb.selectedColor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeWhenHover()
    {
        cb.selectedColor = cl;
        bt.colors = cb;
    }

    public void ChangeWhenLeaves()
    {
        cb.selectedColor = originalColor;
        bt.colors = cb;
    }
}
