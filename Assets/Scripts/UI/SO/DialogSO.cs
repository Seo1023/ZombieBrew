using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialog", menuName = "Dialog System/Dialog")]
public class DialogSO : ScriptableObject
{
    public int id;
    public string text;
    public string characterName;
    public int nextId;
    public int? choiceNextId;
    public Sprite portrait;

    [Tooltip("초상화 리소스 경로 (Resources 폴더 내의 경로)")]
    public string portraitPath;
}
