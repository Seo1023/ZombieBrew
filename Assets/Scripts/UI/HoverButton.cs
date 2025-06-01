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

    public Sprite clickedImage;
    public Sprite defaultImage;

    private Image buttonImage;
    public HoverButton selectCharacterButton;
    private static HoverButton currentlySelectedButton = null;

    private void Awake()
    {
        buttonImage = bt.image;

        if (defaultImage == null && buttonImage != null)
            defaultImage = buttonImage.sprite;

        if (clickedImage == null)
            clickedImage = defaultImage;
    }

    void Start()
    {
        cb = bt.colors;
        originalColor = cb.selectedColor;

        //  씬 시작 시 지정된 버튼이 있다면 클릭 이미지 적용
        if (selectCharacterButton != null)
        {
            selectCharacterButton.SetClickedImage();
            currentlySelectedButton = selectCharacterButton;
        }
    }

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

    public void ChangeWhenClicked()
    {
        if (currentlySelectedButton != null && currentlySelectedButton != this)
        {
            currentlySelectedButton.ResetToDefault();
        }

        currentlySelectedButton = this;

        if (buttonImage != null)
        {
            buttonImage.sprite = clickedImage != null ? clickedImage : defaultImage;
        }
    }

    public void ResetToDefault()
    {
        if (buttonImage != null && defaultImage != null)
        {
            buttonImage.sprite = defaultImage;
        }
    }
    public void OnClick()
    {
        if (currentlySelectedButton != null && currentlySelectedButton != this)
        {
            currentlySelectedButton.ResetToDefault();
        }

        SetClickedImage();
        currentlySelectedButton = this;
    }

    public void SetClickedImage()
    {
        if (buttonImage != null && clickedImage != null)
        {
            buttonImage.sprite = clickedImage;
        }
    }
}
