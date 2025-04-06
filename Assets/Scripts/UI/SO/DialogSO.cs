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

    [Tooltip("�ʻ�ȭ ���ҽ� ��� (Resources ���� ���� ���)")]
    public string portraitPath;
}
