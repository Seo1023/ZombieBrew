using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInitializer : MonoBehaviour
{
    void Start()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.player = this.transform;
            GameManager.Instance.skillManager = GetComponent<PassiveSkillManager>();
            GameManager.Instance.InitPlayerAndWeapon();
        }
    }
}
