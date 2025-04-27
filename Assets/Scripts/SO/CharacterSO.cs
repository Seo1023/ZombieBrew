using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Characters/CharacterSO")]
public class CharacterSO : ScriptableObject
{
    public int id;
    public string characterName;
    public string description;
    public Sprite icon;
    public GameObject characterPrefab;
    public WeaponSO defaultWeapon;
    public ActiveSkillSO activeSkill;
    public string iconpath;
    public CharacterType characterType;

    public enum CharacterType
    {
        Player,
        NormalZombie,
    }
}
