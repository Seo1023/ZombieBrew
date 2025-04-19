using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoffeeMachineLogic : MonoBehaviour
{
    public RecipeSO[] recipes;
    public ItemSlotUI[] craftingSlots;
    public Image resultImage;
    public GameObject bubbleUI;

    public bool TryExtract()
    {
        IngredientSO[] currentIngredients = new IngredientSO[craftingSlots.Length];

        for(int i = 0; i < craftingSlots.Length; i++)
        {
            currentIngredients[i] = craftingSlots[i].currentIngredient;
            if (currentIngredients[i] == null)
            {
                Debug.Log("빈 슬롯이 있습니다.");
                return false;
            }
        }

        foreach(var recipe in recipes)
        {
            if (recipe.Match(currentIngredients))
            {
                Debug.Log($"{recipe.resultDrink.DisplayName} 제작 성공!");
                ShowResult(recipe.resultDrink.iconPath);
                ClearSlots();
            }
        }

        Debug.Log("레시피 불일치. 제작 실패.");
        return false;
    }

    void ShowResult(Sprite drinkIcon)
    {
        if(bubbleUI != null && resultImage != null)
        {
            bubbleUI.SetActive(true);
            resultImage.sprite = drinkIcon;
        }
    }

    void ClearSlots()
    {
        foreach(var slot in craftingSlots)
        {
            slot.Clear();
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
