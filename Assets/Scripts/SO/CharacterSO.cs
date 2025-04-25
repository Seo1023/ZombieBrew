using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Characters/CharacterSO")]
public class CharacterSO : ScriptableObject
{
    public string characterName;
    public GameObject characterPrefab;
    public WeaponSO defaultWeapon;
    public ActiveSkillSO activeSkill;
}
