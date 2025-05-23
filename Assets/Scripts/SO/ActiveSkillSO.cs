using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ActiveSkill", menuName = "ActiveSkills/ActiveSkillSO")]
public class ActiveSkillSO : ScriptableObject
{
    public int id;
    public string skillName;
    public string description;
    public int cooldownTime;
    public int damage;
    public int range;
    public int effectValue;
    public Sprite icon;
    public GameObject effectPrefab;
    public AudioClip audioClip;
    private AudioSource audioSource;
    public ActiveSkillType activeSkillType;

    public enum ActiveSkillType
    {
        Area,
        MouseClick,
        Target,
        Buff,
        Spawn
    }

    public virtual void Activate(GameObject caster, Vector3 target)
    {
        SkillExecutor.Execute(this, caster, target);
    }
}
