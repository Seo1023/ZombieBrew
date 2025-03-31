using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class ItemDataLoader : MonoBehaviour
{
    [SerializeField]
    private string jsonFileName = "itemData";  // Resources 폴더 내 JSON 파일 이름

    private List<ItemData> itemList;

    void Start()
    {
        LoadItemData();
    }

    void LoadItemData()
    {
        // Resources 폴더에서 itemData.json 파일을 로드
        TextAsset jsonFile = Resources.Load<TextAsset>(jsonFileName);

        if (jsonFile != null)
        {
            // JSON 파일의 텍스트를 읽어서 List<ItemData>로 역직렬화
            itemList = JsonConvert.DeserializeObject<List<ItemData>>(jsonFile.text);

            // itemList가 null이면 빈 리스트로 초기화
            if (itemList == null)
            {
                Debug.LogError("JSON 파일을 읽었지만 itemList가 null입니다. 데이터를 확인하세요.");
                itemList = new List<ItemData>();  // 빈 리스트 할당
            }
        }
        else
        {
            Debug.LogError($"JSON 파일을 찾을 수 없습니다: {jsonFileName}");  // 파일이 없을 경우 에러 출력
            itemList = new List<ItemData>();  // 빈 리스트 할당
        }

        // itemList가 null이 아닌지 확인 후 Count 접근
        if (itemList != null && itemList.Count > 0)
        {
            Debug.Log($"로드된 아이템 수: {itemList.Count}");
            foreach (var item in itemList)
            {
                Debug.Log($"아이템: {EncodeKorean(item.itemName)}, 설명: {EncodeKorean(item.description)}");
            }
        }
        else
        {
            Debug.LogError("itemList가 비어있습니다. 데이터를 확인하세요.");
        }
    }

    private string EncodeKorean(string text)
    {
        if (string.IsNullOrEmpty(text)) return "";
        byte[] bytes = Encoding.Default.GetBytes(text);
        return Encoding.UTF8.GetString(bytes);
    }
}
