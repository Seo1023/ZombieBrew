#if UNITY_EDITOR
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using static ActiveSkillSO;
using static PassiveSkillSO;
using static CharacterSO;
using static WeaponSO;

public enum ConversionType
{
    Weapons,
    Characters,
    ActiveSkills,
    PassiveSkills,
}

public class JsonToScriptableConverter : EditorWindow
{
    private string jsonFilePath = "";
    private string outputFolder = "Assets/ScriptableObjects/";
    private bool createDatabase = true;
    private ConversionType conversionType = ConversionType.Weapons;

    [MenuItem("Tools/JSON to Scriptable Objects")]

    public static void ShowWindow()
    {
        GetWindow<JsonToScriptableConverter>("JSON to Scriptable Objects");
    }

    private void OnGUI()
    {
        GUILayout.Label("JSON to scriptable Object Converter", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        if (GUILayout.Button("Sellect JSON File"))
        {
            jsonFilePath = EditorUtility.OpenFilePanel("Sellect JSON File", "", "json");
        }

        EditorGUILayout.LabelField("Select File : ", jsonFilePath);
        EditorGUILayout.Space();

        conversionType = (ConversionType)EditorGUILayout.EnumPopup("Conversion Type :", conversionType);
        if (conversionType == ConversionType.Weapons)
        {
            outputFolder = "Assets/ScriptableObjects/Weapons";
        }
        else if (conversionType == ConversionType.Characters)
        {
            outputFolder = "Assets/ScriptableObjects/Character";
        }
        else if(conversionType == ConversionType.ActiveSkills)
        {
            outputFolder = "Assets/ScriptableObjects/ActiveSkills";
        }
        else if(conversionType == ConversionType.PassiveSkills)
        {
            outputFolder = "Assets/ScriptableObjects/PassiveSkills";
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
                case ConversionType.Weapons:
                    ConvertJsonToWeaponScriptableObjects();
                    break;
                case ConversionType.Characters:
                    ConvertJsonToCharacterScriptableObject();
                    break;
                case ConversionType.ActiveSkills:
                    ConvertJsonToActiveSkilllScriptableObject();
                    break;
                case ConversionType.PassiveSkills:
                    ConvertJsonToPassiveSkillsScriptableObject();
                    break;
            }
            //ConvertJsonToItemScriptableObjects();
        }
    }
    private void ConvertJsonToWeaponScriptableObjects()
    {
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        string jsonText = File.ReadAllText(jsonFilePath);

        try
        {
            List<WeaponData> weaponDataList = JsonConvert.DeserializeObject<List<WeaponData>>(jsonText);
            List<WeaponSO> createdWeapons = new List<WeaponSO>();

            foreach (var weaponData in weaponDataList)
            {
                WeaponSO weaponSO = ScriptableObject.CreateInstance<WeaponSO>();

                weaponSO.id = weaponData.id;
                weaponSO.weaponName = weaponData.weaponName;
                weaponSO.description = weaponData.description;

                if (System.Enum.TryParse(weaponData.type, out WeaponType parsedType))
                {
                    weaponSO.weaponType = parsedType;
                }
                else
                {
                    Debug.LogWarning($"무기'{weaponData.weaponName}'의 유허하지 않은 타입 : {weaponData.type}");
                }

                weaponSO.level = weaponData.level;
                weaponSO.damage = weaponData.damage;
                weaponSO.maxAmmo = weaponData.maxAmmo;
                weaponSO.currentAmmo = weaponData.currentAmmo;
                weaponSO.fireRate = weaponData.fireRate;

                //아이콘 로드(경로가 있는경우)
                if (!string.IsNullOrEmpty(weaponData.iconpath))
                {
                    weaponSO.icon = AssetDatabase.LoadAssetAtPath<Sprite>($"Assets/Resources/{weaponData.iconpath}.png");
                    if (weaponSO.icon == null)
                    {
                        Debug.LogWarning($"무기 '{weaponData.weaponName}'의 아이콘을 찾을 수 없습니다 : {weaponData.iconpath}");
                    }
                }

                string assetPath = $"{outputFolder}/weapon_{weaponData.id.ToString("D4")}_{weaponData.weaponName}.asset";
                AssetDatabase.CreateAsset(weaponSO, assetPath);

                weaponSO.name = $"weapon_{weaponData.id.ToString("D4")}+{weaponData.weaponName}";
                createdWeapons.Add(weaponSO);

                EditorUtility.SetDirty(weaponSO);
            }

            if (createDatabase && createdWeapons.Count > 0)
            {
                WeaponDatabaseSO database = ScriptableObject.CreateInstance<WeaponDatabaseSO>();
                database.weapons = createdWeapons;

                AssetDatabase.CreateAsset(database, $"{outputFolder}/WeaponDatabase.asset");
                EditorUtility.SetDirty(database);
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
        catch (System.Exception e)
        {
            EditorUtility.DisplayDialog("Error", $"Failed to Convert Json : {e.Message}", "OK");
            Debug.LogError($"JSON 변환 오류 : {e}");
        }
    }

    private void ConvertJsonToCharacterScriptableObject()
    {
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        string jsonText = File.ReadAllText(jsonFilePath);

        try
        {
            List<CharacterData> characterDataList = JsonConvert.DeserializeObject<List<CharacterData>>(jsonText);
            List<CharacterSO> createdcharacters = new List<CharacterSO>();

            foreach (var characterData in characterDataList)
            {
                CharacterSO characterSO = ScriptableObject.CreateInstance<CharacterSO>();

                characterSO.id = characterData.id;
                characterSO.characterName = characterData.characterName;
                characterSO.description = characterData.description;

                if (System.Enum.TryParse(characterData.type, out CharacterType parsedType))
                {
                    characterSO.characterType = parsedType;
                }
                else
                {
                    Debug.LogWarning($"캐릭터'{characterData.characterName}'의 유허하지 않은 타입 : {characterData.type}");
                }

                

                //아이콘 로드(경로가 있는경우)
                if (!string.IsNullOrEmpty(characterData.iconpath))
                {
                    characterSO.icon = AssetDatabase.LoadAssetAtPath<Sprite>($"Assets/Resources/{characterData.iconpath}.png");
                    if (characterSO.icon == null)
                    {
                        Debug.LogWarning($"캐릭터 '{characterData.characterName}'의 아이콘을 찾을 수 없습니다 : {characterData.iconpath}");
                    }
                }

                string assetPath = $"{outputFolder}/character_{characterData.id.ToString("D4")}_{characterData.characterName}.asset";
                AssetDatabase.CreateAsset(characterSO, assetPath);

                characterSO.name = $"character_{characterData.id.ToString("D4")}+{characterData.characterName}";
                createdcharacters.Add(characterSO);

                EditorUtility.SetDirty(characterSO);
            }

            if (createDatabase && createdcharacters.Count > 0)
            {
                CharacterDatabaseSO database = ScriptableObject.CreateInstance<CharacterDatabaseSO>();
                database.characters = createdcharacters;

                AssetDatabase.CreateAsset(database, $"{outputFolder}/CharacterDatabase.asset");
                EditorUtility.SetDirty(database);
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
        catch (System.Exception e)
        {
            EditorUtility.DisplayDialog("Error", $"Failed to Convert Json : {e.Message}", "OK");
            Debug.LogError($"JSON 변환 오류 : {e}");
        }
    }

    private void ConvertJsonToActiveSkilllScriptableObject()
    {
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        string jsonText = File.ReadAllText(jsonFilePath);

        try
        {
            List<ActiveSkillData> activeskillDataList = JsonConvert.DeserializeObject<List<ActiveSkillData>>(jsonText);
            List<ActiveSkillSO> createdactiveskill = new List<ActiveSkillSO>();

            foreach (var activeskillData in activeskillDataList)
            {
                ActiveSkillSO activeskillSO = ScriptableObject.CreateInstance<ActiveSkillSO>();

                activeskillSO.id = activeskillData.id;
                activeskillSO.skillName = activeskillData.skillName;
                activeskillSO.description = activeskillData.description;
                activeskillSO.cooldownTime = activeskillData.cooldownTime;
                activeskillSO.damage = activeskillData.damage;
                activeskillSO.range = activeskillData.range;
                activeskillSO.effectValue = activeskillData.effectValue;

                if (System.Enum.TryParse(activeskillData.type, out ActiveSkillType parsedType))
                {
                    activeskillSO.activeSkillType = parsedType;
                }
                else
                {
                    Debug.LogWarning($"액티브 스킬'{activeskillData.skillName}'의 유허하지 않은 타입 : {activeskillData.type}");
                }



                //아이콘 로드(경로가 있는경우)
                if (!string.IsNullOrEmpty(activeskillData.iconpath))
                {
                    activeskillSO.icon = AssetDatabase.LoadAssetAtPath<Sprite>($"Assets/Resources/{activeskillData.iconpath}.png");
                    if (activeskillSO.icon == null)
                    {
                        Debug.LogWarning($"액티브 스킬 '{activeskillData.skillName}'의 아이콘을 찾을 수 없습니다 : {activeskillData.iconpath}");
                    }
                }

                createdactiveskill.Add(activeskillSO);

                string assetPath = $"{outputFolder}/Activeskill_{activeskillData.id.ToString("D4")}.asset";
                AssetDatabase.CreateAsset(activeskillSO, assetPath);

                // ScriptableObject 이름도 깔끔하게 ID만 붙이기
                activeskillSO.name = $"Activeskill_{activeskillData.id.ToString("D4")}";

                EditorUtility.SetDirty(activeskillSO);

                EditorUtility.SetDirty(activeskillSO);
            }

            if (createDatabase && createdactiveskill.Count > 0)
            {
                ActiveSkillDatabaseSO database = ScriptableObject.CreateInstance<ActiveSkillDatabaseSO>();
                database.activeskills = createdactiveskill;

                AssetDatabase.CreateAsset(database, $"{outputFolder}/ActiveDatabase.asset");
                EditorUtility.SetDirty(database);
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
        catch (System.Exception e)
        {
            EditorUtility.DisplayDialog("Error", $"Failed to Convert Json : {e.Message}", "OK");
            Debug.LogError($"JSON 변환 오류 : {e}");
        }
    }

    private void ConvertJsonToPassiveSkillsScriptableObject()
    {
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        string jsonText = File.ReadAllText(jsonFilePath);

        try
        {
            List<PassiveSkillData> passiveskillDataList = JsonConvert.DeserializeObject<List<PassiveSkillData>>(jsonText);
            List<PassiveSkillSO> createdpassiveskill = new List<PassiveSkillSO>();

            foreach (var passiveskillData in passiveskillDataList)
            {
                PassiveSkillSO passiveskillSO = ScriptableObject.CreateInstance<PassiveSkillSO>();

                passiveskillSO.id = passiveskillData.id;
                passiveskillSO.skillName = passiveskillData.skillName;
                passiveskillSO.description = passiveskillData.description;
                passiveskillSO.cooldownTime = passiveskillData.cooldownTime;
                passiveskillSO.damage = passiveskillData.damage;
                passiveskillSO.range = passiveskillData.range;
                passiveskillSO.effectValue = passiveskillData.effectValue;

                if (System.Enum.TryParse(passiveskillData.type, out PassiveSkillType parsedType))
                {
                    passiveskillSO.passiveSkillType = parsedType;
                }
                else
                {
                    Debug.LogWarning($"패시브 스킬'{passiveskillData.skillName}'의 유허하지 않은 타입 : {passiveskillData.type}");
                }



                //아이콘 로드(경로가 있는경우)
                if (!string.IsNullOrEmpty(passiveskillData.iconpath))
                {
                    passiveskillSO.icon = AssetDatabase.LoadAssetAtPath<Sprite>($"Assets/Resources/{passiveskillData.iconpath}.png");
                    if (passiveskillSO.icon == null)
                    {
                        Debug.LogWarning($"패시브 스킬 '{passiveskillData.skillName}'의 아이콘을 찾을 수 없습니다 : {passiveskillData.iconpath}");
                    }
                }

                //passiveskillSO.name = $"passiveskill_{passiveskillData.id.ToString("D4")}+{passiveskillData.skillName}";
                createdpassiveskill.Add(passiveskillSO);

                string assetPath = $"{outputFolder}/Passiveskill_{passiveskillData.id.ToString("D4")}.asset";
                AssetDatabase.CreateAsset(passiveskillSO, assetPath);

                // ScriptableObject 이름도 깔끔하게 ID만 붙이기
                passiveskillSO.name = $"Passiveskill_{passiveskillData.id.ToString("D4")}";

                EditorUtility.SetDirty(passiveskillSO);
            }

            if (createDatabase && createdpassiveskill.Count > 0)
            {
                PassiveSkillDatabaseSO database = ScriptableObject.CreateInstance<PassiveSkillDatabaseSO>();
                database.passiveskills = createdpassiveskill;

                AssetDatabase.CreateAsset(database, $"{outputFolder}/PassiveDatabase.asset");
                EditorUtility.SetDirty(database);
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
        catch (System.Exception e)
        {
            EditorUtility.DisplayDialog("Error", $"Failed to Convert Json : {e.Message}", "OK");
            Debug.LogError($"JSON 변환 오류 : {e}");
        }
    }
}
#endif
