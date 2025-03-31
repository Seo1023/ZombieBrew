using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class ItemDataLoader : MonoBehaviour
{
    [SerializeField]
    private string jsonFileName = "itemData";  // Resources ���� �� JSON ���� �̸�

    private List<ItemData> itemList;

    void Start()
    {
        LoadItemData();
    }

    void LoadItemData()
    {
        // Resources �������� itemData.json ������ �ε�
        TextAsset jsonFile = Resources.Load<TextAsset>(jsonFileName);

        if (jsonFile != null)
        {
            // JSON ������ �ؽ�Ʈ�� �о List<ItemData>�� ������ȭ
            itemList = JsonConvert.DeserializeObject<List<ItemData>>(jsonFile.text);

            // itemList�� null�̸� �� ����Ʈ�� �ʱ�ȭ
            if (itemList == null)
            {
                Debug.LogError("JSON ������ �о����� itemList�� null�Դϴ�. �����͸� Ȯ���ϼ���.");
                itemList = new List<ItemData>();  // �� ����Ʈ �Ҵ�
            }
        }
        else
        {
            Debug.LogError($"JSON ������ ã�� �� �����ϴ�: {jsonFileName}");  // ������ ���� ��� ���� ���
            itemList = new List<ItemData>();  // �� ����Ʈ �Ҵ�
        }

        // itemList�� null�� �ƴ��� Ȯ�� �� Count ����
        if (itemList != null && itemList.Count > 0)
        {
            Debug.Log($"�ε�� ������ ��: {itemList.Count}");
            foreach (var item in itemList)
            {
                Debug.Log($"������: {EncodeKorean(item.itemName)}, ����: {EncodeKorean(item.description)}");
            }
        }
        else
        {
            Debug.LogError("itemList�� ����ֽ��ϴ�. �����͸� Ȯ���ϼ���.");
        }
    }

    private string EncodeKorean(string text)
    {
        if (string.IsNullOrEmpty(text)) return "";
        byte[] bytes = Encoding.Default.GetBytes(text);
        return Encoding.UTF8.GetString(bytes);
    }
}
