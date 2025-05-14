using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectManager : MonoBehaviour
{
    public List<CharacterSO> characterList;
    public GameObject characterButtonPrefab;
    public Transform buttonParent;

    private CharacterSelectButton selectedButton;
    public CharacterSO selectedCharacter;

    public List<string> mapSceneNames;
    public GameObject mapButtonPrefab;
    public Transform mapButtonParent;

    private MapSelectButton selectedMapButton;
    public string selectedMapName;

    public GameObject confirmPanel;
    public TMP_Text characterNameText;
    public TMP_Text mapNameText;

    void Start()
    {
        GenerateMapButtons();
        foreach(var character in characterList)
        {
            GameObject btn = Instantiate(characterButtonPrefab, buttonParent);
            CharacterSelectButton csb = btn.GetComponent<CharacterSelectButton>();
            csb.Init(character, this);
        }
    }

    public void OnStartButtonClicked()
    {
        if (selectedCharacter == null || string.IsNullOrEmpty(selectedMapName))
        {
            Debug.LogWarning("ĳ���� �Ǵ� ���� ���õ��� �ʾҽ��ϴ�.");
        }
        confirmPanel.SetActive(true);
        characterNameText.text = $"������ ĳ���� : {selectedCharacter.characterName}";
        mapNameText.text = $"������ �� : {selectedMapName}";
    }

    public void StartGame()
    {
        if(selectedCharacter == null || string.IsNullOrEmpty(selectedMapName))
        {
            Debug.LogWarning("ĳ���� �Ǵ� ���� ���õ��� �ʾҽ��ϴ�.");
        }

        GameManager.Instance.selectedCharacter = selectedCharacter;
        GameManager.Instance.selectedMap = selectedMapName;

        Debug.Log($"���� ����! ĳ���� : {selectedCharacter.characterName}, �� : {selectedMapName}");
        SceneManager.LoadScene(selectedMapName);
    }

    void GenerateMapButtons()
    {
        foreach(var sceneName in mapSceneNames)
        {
            GameObject btn = Instantiate(mapButtonPrefab, mapButtonParent);
            MapSelectButton msb = btn.GetComponent<MapSelectButton>();
            msb.Init(sceneName, this);
        }
    }

    public void SetCharacter(CharacterSO character)
    {
        selectedCharacter = character;
        Debug.Log($"���õ� ĳ���� : {character.characterName}");
    }

    public void SetMap(string mapSceneName)
    {
        selectedMapName = mapSceneName;
        Debug.Log($"[���� ��] : {mapSceneName}");
    }

    public void SetHighlight(CharacterSelectButton button)
    {
        if(selectedButton != null)
        {
            selectedButton.SetHighlight(false);
        }

        selectedButton = button;
        selectedButton.SetHighlight(true);
    }

    public void SetMapHighlight(MapSelectButton button)
    {
        if(selectedMapButton != null)
        {
            selectedMapButton.Sethighlight(false);
        }

        selectedMapButton = button;
        selectedMapButton.Sethighlight(true);
    }
}
