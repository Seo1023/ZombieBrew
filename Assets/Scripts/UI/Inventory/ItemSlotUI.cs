using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour
{
    public Image background;
    public Image icon;

    [HideInInspector] public IngredientSO currentIngredient;

    public void SetItem(IngredientSO ingredient)
    {
        currentIngredient = ingredient;

        if (ingredient != null && ingredient.icon != null)
        {
            icon.sprite = ingredient.icon;
            icon.enabled = true;
        }
        else
        {
            icon.sprite = null;
            icon.enabled = false;
        }
    }

    public void Clear()
    {
        currentIngredient = null;
        icon.sprite = null;
        icon.enabled = false;
    }
}