#if UNITY_EDITOR
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using Unity.VisualScripting;
//using UnityEngine.Windows;
using static IngredientsType;
using static UnityEditor.Progress;
using static StructuresType;
using static DrinksType;
using static CharactersType;
using static DisplaysType;

public enum ConversionType
{
    Ingredients,
    Structures,
    Dialogs,
    Drinks,
    Characters,
    Displays,
}

[SerializeField]
public class DialogRowData
{
    public int? id;
    public string characterName;
    public string text;
    public int? nextId;
    public string portraitPath;
    public string choiceaction;
    public int? choiceNextId;
}

public class JsonToScriptableConverter : EditorWindow
{
    private string jsonFilePath = "";
    private string outputFolder = "Assets/ScriptableObjects";
    private bool createDatabase = true;
    private ConversionType conversionType = ConversionType.Ingredients;
    
    [MenuItem("Tools/JSON to Scriptable Objects")]
    public static void ShowWindow()
    {
        GetWindow<JsonToScriptableConverter>("JSON to Scriptable Ojects");
    }

    private void OnGUI()
    {
        GUILayout.Label("JSON to scriptable Object Converter", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        if (GUILayout.Button("Sellect JSON File"))
        {
            jsonFilePath = EditorUtility.OpenFilePanel("Select JSON File", "", "json");
        }

        EditorGUILayout.LabelField("Select File : ", jsonFilePath);
        EditorGUILayout.Space();

        conversionType = (ConversionType)EditorGUILayout.EnumPopup("Conversion Type :", conversionType);
        if (conversionType == ConversionType.Ingredients)
        {
            outputFolder = "Assets/ScriptableOjects/Ingredients";
        }
        else if (conversionType == ConversionType.Dialogs)
        {
            outputFolder = "Assets/ScriptableOjects/Dialogs";
        }
        else if(conversionType == ConversionType.Structures)
        {
            outputFolder = "Assets/ScriptableOjects/Structures";
        }
        else if(conversionType == ConversionType.Drinks)
        {
            outputFolder = "Assets/ScriptableOjects/Drinks";
        }
        else if(conversionType == ConversionType.Characters)
        {
            outputFolder = "Assets/ScriptableOjects/Characters";
        }
        else if(conversionType == ConversionType.Displays)
        {
            outputFolder = "Assets/ScriptableOjects/Displays";
        }

        outputFolder = EditorGUILayout.TextField("Output Folder : ", outputFolder);
        createDatabase = EditorGUILayout.Toggle("Create Database Asset", createDatabase);
        EditorGUILayout.Space();

        if (GUILayout.Button("Convert to Scriptable Object"))
        {
            if (string.IsNullOrEmpty(jsonFilePath))
            {
                EditorUtility.DisplayDialog("Error", "Please select a json file firest!", "OK");
                return;
            }

            switch (conversionType)
            {
                case ConversionType.Ingredients:
                    ConvertJsonToIngredientScriptableObjects();
                    break;
                case ConversionType.Structures:
                    ConvertJsonToStructureScriptableObjects();
                    break;
                case ConversionType.Dialogs:
                    ConvertJsonToDialogScriptableObject();
                    break;
                case ConversionType.Drinks:
                    ConvertJsonToDrinkScriptableObjects();
                    break;
                case ConversionType.Characters:
                    ConvertJsonToCharacterScriptableObjects();
                    break;
                case ConversionType.Displays:
                    ConvertJsonToDisplayScriptableObjects();
                    break;
            }
            //ConvertJsonToItemScriptableObjects();
        }
    }

    //재료
    private void ConvertJsonToIngredientScriptableObjects()
    {
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }
        string jsonText = File.ReadAllText(jsonFilePath);
        try
        {
            List<IngredientData> IngredientDatasList = JsonConvert.DeserializeObject<List<IngredientData>>(jsonText);
            List<IngredientSO> createdIngredients = new List<IngredientSO>();

            foreach (var ingredientData in IngredientDatasList)
            {
                IngredientSO ingredientSO = ScriptableObject.CreateInstance<IngredientSO>();

                ingredientSO.id = ingredientData.id;
                ingredientSO.name = ingredientData.name;
                ingredientSO.nameEng = ingredientData.nameEng;
                ingredientSO.description = ingredientData.description;

                if (System.Enum.TryParse(ingredientData.type, out ingredientsType parsetType))
                {
                    ingredientSO.ingredientsType = parsetType;
                }
                else
                {
                    Debug.LogWarning($"아이템'{ingredientData.name}'의 유허하지 않은 타입 : {ingredientData.type}");
                }

                ingredientSO.level = ingredientData.level;
                ingredientSO.Quantity = ingredientData.Quantity;

                if (!string.IsNullOrEmpty(ingredientData.iconPath))
                {
                    ingredientSO.icon = AssetDatabase.LoadAssetAtPath<Sprite>($"Assets/Resources/{ingredientData.iconPath}.png");
                    if (ingredientSO.icon == null)
                    {
                        Debug.LogWarning($"아이템 '{ingredientData.nameEng}'의 아이콘을 찾을 수 없습니다 : {ingredientData.iconPath}");
                    }
                }

                string assetPath = $"{outputFolder}/ingredient_{ingredientData.id.ToString("D4")}_{ingredientData.nameEng}.asset";
                AssetDatabase.CreateAsset(ingredientSO, assetPath);

                ingredientData.name = $"ingredient_{ingredientData.id.ToString("D4")} + {ingredientData.nameEng}";
                createdIngredients.Add(ingredientSO);

                EditorUtility.SetDirty(ingredientSO);
            }

            if (createDatabase && createdIngredients.Count > 0)
            {
                IngredientDatabaseSO database = ScriptableObject.CreateInstance<IngredientDatabaseSO>();
                database.ingredients = createdIngredients;

                AssetDatabase.CreateAsset(database, $"(outputFolder)/ingredientDatabase.asset");
                EditorUtility.SetDirty(database);
            }
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
        catch (System.Exception e)
        {
            EditorUtility.DisplayDialog("Error", $"Failed to Convert JSON : {e.Message}", "OK");
            Debug.LogError($"JSON 변환 오류: {e}");
        }
    }

    //구조물
    private void ConvertJsonToStructureScriptableObjects()
    {
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }
        string jsonText = File.ReadAllText(jsonFilePath);
        try
        {
            List<StructureData> StructuerDatasList = JsonConvert.DeserializeObject<List<StructureData>>(jsonText);
            List<StructureSO> createdstructure = new List<StructureSO>();

            foreach (var structureData in StructuerDatasList)
            {
                StructureSO structureSO = ScriptableObject.CreateInstance<StructureSO>();

                structureSO.id = structureData.id;
                structureSO.name = structureData.name;
                structureSO.nameEng = structureData.nameEng;
                structureSO.description = structureData.description;

                if (System.Enum.TryParse(structureData.Category, out structuresType parsetType))
                {
                    structureSO.structurestype = parsetType;
                }
                else
                {
                    Debug.LogWarning($"아이템'{structureData.name}'의 유허하지 않은 타입 : {structureData.Category}");
                }

                structureSO.level = structureData.level;
                //structureSO.Quantity = structureData.Quantity;

                /*if (!string.IsNullOrEmpty(structureData.iconPath))
                {
                    structureSO.icon = AssetDatabase.LoadAssetAtPath<Sprite>($"Assets/Resources/{structureData.iconPath}.png");
                    if (structureSO.icon == null)
                    {
                        Debug.LogWarning($"아이템 '{structureData.nameEng}'의 아이콘을 찾을 수 없습니다 : {structureData.iconPath}");
                    }
                }*/

                string assetPath = $"{outputFolder}/structure_{structureData.id.ToString("D4")}_{structureData.nameEng}.asset";
                AssetDatabase.CreateAsset(structureSO, assetPath);

                structureData.name = $"structure_{structureData.id.ToString("D4")} + {structureData.nameEng}";
                createdstructure.Add(structureSO);

                EditorUtility.SetDirty(structureSO);
            }

            if (createDatabase && createdstructure.Count > 0)
            {
                StructureDatabaseSO database = ScriptableObject.CreateInstance<StructureDatabaseSO>();
                database.structures = createdstructure;

                AssetDatabase.CreateAsset(database, $"(outputFolder)/structureDatabase.asset");
                EditorUtility.SetDirty(database);
            }
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
        catch (System.Exception e)
        {
            EditorUtility.DisplayDialog("Error", $"Failed to Convert JSON : {e.Message}", "OK");
            Debug.LogError($"JSON 변환 오류: {e}");
        }
    }

    //음료
    private void ConvertJsonToDrinkScriptableObjects()
    {
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }
        string jsonText = File.ReadAllText(jsonFilePath);
        try
        {
            List<DrinkData> DrinkDatasList = JsonConvert.DeserializeObject<List<DrinkData>>(jsonText);
            List<DrinkSO> createdrink = new List<DrinkSO>();

            foreach (var drinkData in DrinkDatasList)
            {
                DrinkSO drinkSO = ScriptableObject.CreateInstance<DrinkSO>();

                drinkSO.id = drinkData.id;
                drinkSO.name = drinkData.name;
                drinkSO.nameEng = drinkData.nameEng;
                drinkSO.description = drinkData.description;

                if (System.Enum.TryParse(drinkData.type, out drinksType parsetType))
                {
                    drinkSO.drinksType = parsetType;
                }
                else
                {
                    Debug.LogWarning($"아이템'{drinkData.name}'의 유허하지 않은 타입 : {drinkData.type}");
                }

                drinkSO.curedlevel = drinkData.curedlevel;
                drinkSO.price = drinkData.price;

                if (!string.IsNullOrEmpty(drinkData.iconPath))
                {
                    drinkSO.iconPath = AssetDatabase.LoadAssetAtPath<Sprite>($"Assets/Resources/{drinkData.iconResourcesPath}.png");
                    if (drinkSO.iconPath == null)
                    {
                        Debug.LogWarning($"아이템 '{drinkData.nameEng}'의 아이콘을 찾을 수 없습니다 : {drinkData.iconResourcesPath}");
                    }
                }

                string assetPath = $"{outputFolder}/drink_{drinkData.id.ToString("D4")}_{drinkData.nameEng}.asset";
                AssetDatabase.CreateAsset(drinkSO, assetPath);

                drinkData.name = $"drink_{drinkData.id.ToString("D4")} + {drinkData.nameEng}";
                createdrink.Add(drinkSO);

                EditorUtility.SetDirty(drinkSO);
            }

            if (createDatabase && createdrink.Count > 0)
            {
                DrinkDatabaseSO database = ScriptableObject.CreateInstance<DrinkDatabaseSO>();
                database.drinks = createdrink;

                AssetDatabase.CreateAsset(database, $"(outputFolder)/drinkDatabase.asset");
                EditorUtility.SetDirty(database);
            }
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
        catch (System.Exception e)
        {
            EditorUtility.DisplayDialog("Error", $"Failed to Convert JSON : {e.Message}", "OK");
            Debug.LogError($"JSON 변환 오류: {e}");
        }
    }

    //캐릭터
    private void ConvertJsonToCharacterScriptableObjects()
    {
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }
        string jsonText = File.ReadAllText(jsonFilePath);
        try
        {
            List<CharactersData> CharacterDatasList = JsonConvert.DeserializeObject<List<CharactersData>>(jsonText);
            List<CharacterSO> createdcharacter = new List<CharacterSO>();

            foreach (var characterData in CharacterDatasList)
            {
                CharacterSO characterSO = ScriptableObject.CreateInstance<CharacterSO>();

                characterSO.id = characterData.id;
                characterSO.name = characterData.name;
                characterSO.nameEng = characterData.nameEng;
                characterSO.description = characterData.description;

                if (System.Enum.TryParse(characterData.type, out charactersType parsetType))
                {
                    characterSO.charactersType = parsetType;
                }
                else
                {
                    Debug.LogWarning($"아이템'{characterData.name}'의 유허하지 않은 타입 : {characterData.type}");
                }

                characterSO.level = characterData.level;
                characterSO.speed = characterData.speed;
                characterSO.hp = characterData.hp;

                string assetPath = $"{outputFolder}/character_{characterData.id.ToString("D4")}_{characterData.nameEng}.asset";
                AssetDatabase.CreateAsset(characterSO, assetPath);

                characterData.name = $"character_{characterData.id.ToString("D4")} + {characterData.nameEng}";
                createdcharacter.Add(characterSO);

                EditorUtility.SetDirty(characterSO);
            }

            if (createDatabase && createdcharacter.Count > 0)
            {
                CharacterDatabaseSO database = ScriptableObject.CreateInstance<CharacterDatabaseSO>();
                database.structures = createdcharacter;

                AssetDatabase.CreateAsset(database, $"(outputFolder)/characterDatabase.asset");
                EditorUtility.SetDirty(database);
            }
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
        catch (System.Exception e)
        {
            EditorUtility.DisplayDialog("Error", $"Failed to Convert JSON : {e.Message}", "OK");
            Debug.LogError($"JSON 변환 오류: {e}");
        }
    }

    //UI
    private void ConvertJsonToDisplayScriptableObjects()
    {
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }
        string jsonText = File.ReadAllText(jsonFilePath);
        try
        {
            List<DisplayData> DisplayDatasList = JsonConvert.DeserializeObject<List<DisplayData>>(jsonText);
            List<DisplaySO> createddisplay = new List<DisplaySO>();

            foreach (var displayData in DisplayDatasList)
            {
                DisplaySO displaySO = ScriptableObject.CreateInstance<DisplaySO>();

                displaySO.id = displayData.id;
                displaySO.name = displayData.name;
                displaySO.nameEng = displayData.nameEng;
                displaySO.description = displayData.description;

                if (System.Enum.TryParse(displayData.type, out displaysType parsetType))
                {
                    displaySO.displaysType = parsetType;
                }
                else
                {
                    Debug.LogWarning($"아이템'{displayData.name}'의 유허하지 않은 타입 : {displayData.type}");
                }

                displaySO.interaction = displayData.interaction;

                string assetPath = $"{outputFolder}/display_{displayData.id.ToString("D4")}_{displayData.nameEng}.asset";
                AssetDatabase.CreateAsset(displaySO, assetPath);

                displayData.name = $"display_{displayData.id.ToString("D4")} + {displayData.nameEng}";
                createddisplay.Add(displaySO);

                EditorUtility.SetDirty(displaySO);
            }

            if (createDatabase && createddisplay.Count > 0)
            {
                DisplayDatabaseSO database = ScriptableObject.CreateInstance<DisplayDatabaseSO>();
                database.displays = createddisplay;

                AssetDatabase.CreateAsset(database, $"(outputFolder)/displayDatabase.asset");
                EditorUtility.SetDirty(database);
            }
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
        catch (System.Exception e)
        {
            EditorUtility.DisplayDialog("Error", $"Failed to Convert JSON : {e.Message}", "OK");
            Debug.LogError($"JSON 변환 오류: {e}");
        }
    }

    //대화
    private void ConvertJsonToDialogScriptableObject()
    {
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }
        string jsonText = File.ReadAllText(jsonFilePath);

        try
        {
            //JSON 파싱
            List<DialogRowData> rowDataList = JsonConvert.DeserializeObject<List<DialogRowData>>(jsonText);

            //대화 데이터 재구성
            Dictionary<int, DialogSO> dialogMap = new Dictionary<int, DialogSO>();
            List<DialogSO> creatDialog = new List<DialogSO>();

            //1단계 : 대화 항목 생성
            foreach (var rowData in rowDataList)
            {
                //id있는 행은 대화로 처리
                if (rowData.id.HasValue)
                {
                    DialogSO dialogSO = ScriptableObject.CreateInstance<DialogSO>();

                    //데이터 복사
                    dialogSO.id = rowData.id.Value;
                    dialogSO.characterName = rowData.characterName;
                    dialogSO.text = rowData.text;
                    dialogSO.nextId = rowData.nextId.HasValue ? rowData.nextId.Value : -1;
                    dialogSO.portraitPath = rowData.portraitPath;
                    //dialogSO.choice = new List<DialogChoiceSO>();

                    if (!string.IsNullOrEmpty(rowData.portraitPath))
                    {
                        dialogSO.portrait = Resources.Load<Sprite>(rowData.portraitPath);
                        if (dialogSO.portrait == null)
                        {
                            Debug.LogWarning($"대화 {rowData.id}의 초상화를 찾을 수 없습니다");
                        }
                    }
                    dialogMap[dialogSO.id] = dialogSO;
                    creatDialog.Add(dialogSO);
                }
            }

            //2단계 : 선택지 항목 처리 및 연결
            foreach (var rowData in rowDataList)
            {
                if (!rowData.id.HasValue && !string.IsNullOrEmpty(rowData.choiceaction) && rowData.choiceNextId.HasValue)
                {
                    int parentId = -1;

                    int currentIndex = rowDataList.IndexOf(rowData);
                    for (int i = currentIndex - 1; i >= 0; i--)
                    {
                        if (rowDataList[i].id.HasValue)
                        {
                            parentId = rowDataList[i].id.Value;
                            break;
                        }
                    }

                    if (parentId == -1)
                    {
                        Debug.LogWarning($"선택지 '{rowData.choiceaction}'의 부모 대화를 찾을 수 없습니다");
                    }
                    if (dialogMap.TryGetValue(parentId, out DialogSO parentDialog))
                    {
                        DialogChoiceSO choiceSO = ScriptableObject.CreateInstance<DialogChoiceSO>();
                        choiceSO.text = rowData.choiceaction;
                        //choiceSO.nextId = rowData.choiceaction.HasValue;

                        //string choiceAssetPath = $"{outputFolder}/Choice_{parentId}_{parentDialog.choice.Count + 1}.asset";
                        //AssetDatabase.CreateAsset(choiceSO, choiceAssetPath);
                        EditorUtility.SetDirty(choiceSO);

                        //parentDialog.choice.Add(choiceSO);
                    }
                    else
                    {
                        Debug.LogWarning($"선택지 '{rowData.choiceaction}'를 연결할 대화 (ID : {parentId})룰 찾을 수 없습니다");
                    }
                }
            }

            //3단계
            foreach (var dialog in creatDialog)
            {
                string assetPath = $"{outputFolder}/Dialog_{dialog.id.ToString("D4")}.asset";
                AssetDatabase.CreateAsset(dialog, assetPath);

                dialog.name = $"Dialog_{dialog.id.ToString("D4")}";

                EditorUtility.SetDirty(dialog);
            }

            //데이터 베이스 생성 
            if (createDatabase && creatDialog.Count > 0)
            {
                DialogDatabaseSO database = ScriptableObject.CreateInstance<DialogDatabaseSO>();
                database.dialogs = creatDialog;

                AssetDatabase.CreateAsset(database, $"{outputFolder}/DialogDatabase.assets");
                EditorUtility.SetDirty(database);
            }
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            EditorUtility.DisplayDialog("Success", $"Creatd {creatDialog.Count} dialog scriptable objects!", "OK");
        }
        catch (System.Exception e)
        {
            EditorUtility.DisplayDialog("Error", $"Failed to convert JSON: {e.Message}", "OK");
            Debug.LogError($"JSON 변환 오류 : {e}");
        }
    }
}

#endif