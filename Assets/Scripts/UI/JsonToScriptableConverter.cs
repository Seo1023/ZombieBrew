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

public enum ConversionType
{
    Ingredients,
    Dialogs,
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
                case ConversionType.Dialogs:
                    ConvertJsonToDialogScriptableObject();
                    break;

            }
            //ConvertJsonToItemScriptableObjects();
        }
    }

    private void ConvertJsonToIngredientScriptableObjects()
    {
        if(!Directory.Exists(outputFolder))
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
                    Debug.LogWarning($"������'{ingredientData.name}'�� �������� ���� Ÿ�� : {ingredientData.type}");
                }

                ingredientSO.level = ingredientData.level;
                ingredientSO.Quantity = ingredientData.Quantity;

                if (!string.IsNullOrEmpty(ingredientData.iconPath))
                {
                    ingredientSO.icon = AssetDatabase.LoadAssetAtPath<Sprite>($"Assets/Resources/{ingredientData.iconPath}.png");
                    if (ingredientSO.icon == null)
                    {
                        Debug.LogWarning($"������ '{ingredientData.nameEng}'�� �������� ã�� �� �����ϴ� : {ingredientData.iconPath}");
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
            Debug.LogError($"JSON ��ȯ ����: {e}");
        }
    }
    //��ȭ
    private void ConvertJsonToDialogScriptableObject()
    {
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }
        string jsonText = File.ReadAllText(jsonFilePath);

        try
        {
            //JSON �Ľ�
            List<DialogRowData> rowDataList = JsonConvert.DeserializeObject<List<DialogRowData>>(jsonText);

            //��ȭ ������ �籸��
            Dictionary<int, DialogSO> dialogMap = new Dictionary<int, DialogSO>();
            List<DialogSO> creatDialog = new List<DialogSO>();

            //1�ܰ� : ��ȭ �׸� ����
            foreach (var rowData in rowDataList)
            {
                //id�ִ� ���� ��ȭ�� ó��
                if (rowData.id.HasValue)
                {
                    DialogSO dialogSO = ScriptableObject.CreateInstance<DialogSO>();

                    //������ ����
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
                            Debug.LogWarning($"��ȭ {rowData.id}�� �ʻ�ȭ�� ã�� �� �����ϴ�");
                        }
                    }
                    dialogMap[dialogSO.id] = dialogSO;
                    creatDialog.Add(dialogSO);
                }
            }

            //2�ܰ� : ������ �׸� ó�� �� ����
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
                        Debug.LogWarning($"������ '{rowData.choiceaction}'�� �θ� ��ȭ�� ã�� �� �����ϴ�");
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
                        Debug.LogWarning($"������ '{rowData.choiceaction}'�� ������ ��ȭ (ID : {parentId})�� ã�� �� �����ϴ�");
                    }
                }
            }

            //3�ܰ�
            foreach (var dialog in creatDialog)
            {
                string assetPath = $"{outputFolder}/Dialog_{dialog.id.ToString("D4")}.asset";
                AssetDatabase.CreateAsset(dialog, assetPath);

                dialog.name = $"Dialog_{dialog.id.ToString("D4")}";

                EditorUtility.SetDirty(dialog);
            }

            //������ ���̽� ���� 
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
            Debug.LogError($"JSON ��ȯ ���� : {e}");
        }
    }
}

#endif