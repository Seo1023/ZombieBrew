using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// �κ��丮�� UI ������ �����ϰ� ������ �����͸� ����/�����ϴ� �Ŵ��� Ŭ����.
/// </summary>
public class InventoryManager : MonoBehaviour
{
    // �ν����Ϳ��� ������ UI ���� ����Ʈ (ItemSlotUI�� background + icon�� ������ ���� ����)
    public List<ItemSlotUI> itemSlots;

    // ���Ը��� ����� ������ ������ (ScriptableObject ���)
    private IngredientSO[] savedItems;


    /// <summary>
    /// �ʱ�ȭ �Լ�: savedItems �迭 ���� �� UI �ʱ� ���·� ����
    /// </summary>
    void Awake()
    {
        // ���� ������ŭ ���� ���� Ȯ��
        savedItems = new IngredientSO[itemSlots.Count];

        // UI ���ΰ�ħ (�ʱ⿡�� ��� �� ���·� ����)
        RefreshUI();
    }

    /// <summary>
    /// �κ��丮 UI�� ����. �������� ������ ������ ǥ��, ������ ������ ����.
    /// ��� �̹����� �׻� ���̰� ����.
    public void RefreshUI()
    {
        for (int i = 0; i < itemSlots.Count; i++)
        {
            if (savedItems[i] != null)
            {
                // �������� �ִ� ���: ������ ���� �� ǥ��
                itemSlots[i].icon.sprite = savedItems[i].icon;
                //itemSlots[i].icon.color = Color.white; // ���� �������ϰ�
                itemSlots[i].icon.enabled = true;
            }
            else
            {
                // �������� ���� ���: ������ ����, ���� ó�� (Raycast�� ���� enabled�� true ����)
                itemSlots[i].icon.sprite = null;
                //itemSlots[i].icon.color = new Color(1, 1, 1, 0); // ���� �����ϰ�
                itemSlots[i].icon.enabled = true;
                itemSlots[i].icon.gameObject.SetActive(true);
            }
            // ��� �̹����� �׻� ǥ�� (��� �־ ���� ���°� �����ǵ���)
            if (itemSlots[i].background != null)
                itemSlots[i].background.enabled = true;
        }
    }

    /// <summary>
    /// Ư�� ���Կ� ������(IngredientSO)�� �߰��ϰ� UI�� ����.
    /// </summary>
    public void AddIngredient(int slotIndex, IngredientSO newIngredient)
    {
        // �߸��� ���� �ε��� �Ǵ� null �������� ����
        if (slotIndex < 0 || slotIndex >= itemSlots.Count || newIngredient == null)
            return;

        // ������ ������ ����
        savedItems[slotIndex] = newIngredient;

        // UI ����
        RefreshUI();
    }

    /// <summary>
    /// Ư�� ������ �������� �����ϰ� UI�� ����.
    /// </summary>
    public void ClearSlot(int slotIndex)
    {
        // �߸��� ���� �ε����� ����
        if (slotIndex < 0 || slotIndex >= itemSlots.Count)
            return;

        // �ش� ���� ������ ����
        savedItems[slotIndex] = null;

        // UI ����
        RefreshUI();
    }

    /// <summary>
    /// Ư�� ���Կ� ����� ������ ������ ��ȯ (���� ��� Ȱ�� ����).
    /// </summary>
    public IngredientSO GetIngredient(int slotIndex)
    {
        // ���� üũ �� ��ȯ
        if (slotIndex < 0 || slotIndex >= itemSlots.Count)
            return null;

        return savedItems[slotIndex];
    }

    /// <summary>
    /// �� ������ ������ ������ ���� ��ȯ�մϴ�.
    /// </summary>
    /// <param name="from">��ȯ�� ù ��° ���� �ε���</param>
    /// <param name="to">��ȯ�� �� ��° ���� �ε���</param>
    public void SwapIngredients(int from, int to)
    {
        // ���� �ε����� ��ȿ���� Ȯ��
        if (from < 0 || from >= savedItems.Length || to < 0 || to >= savedItems.Length) return;

        // �� ������ �������� ��ȯ
        var temp = savedItems[from];
        savedItems[from] = savedItems[to];
        savedItems[to] = temp;

        // UI ����
        RefreshUI();
    }

    /// <summary>
    /// ������ ���Կ� ���ο� ������ ������ �����մϴ�.
    /// </summary>
    /// <param name="slotIndex">������ ������ �ε���</param>
    /// <param name="ingredient">���Կ� ���� IngredientSO</param>
    public void SetIngredient(int slotIndex, IngredientSO ingredient)
    {
        // ���� �ε����� ��ȿ���� Ȯ��
        if (slotIndex < 0 || slotIndex >= savedItems.Length)
            return;

        // ���Կ� ������ ����
        savedItems[slotIndex] = ingredient;
    }


}
