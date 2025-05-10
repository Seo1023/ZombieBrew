using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterSelectButton : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text descriptionText;
    public Image iconImage;
    public GameObject highlight;

    [HideInInspector] public CharacterSO characterData;
    private SelectManager manager;

    public void Init(CharacterSO data, SelectManager manager)
    {
        this.characterData = data;
        this.manager = manager;

        nameText.text = data.characterName;
        descriptionText.text = data.description;
        iconImage.sprite = data.icon;
        highlight.SetActive(false);
    }

    public void OnClick()
    {
        manager.SetCharacter(characterData);
        manager.SetHighlight(this);
    }

    public void SetHighlight(bool on)
    {
        highlight.SetActive(on);
    }
}
