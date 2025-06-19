using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Linq;

public class PassiveSkillSelectorUI : MonoBehaviour
{
    public static PassiveSkillSelectorUI Instance;

    public GameObject panel;
    public Transform buttonContainer;
    public GameObject skillButtonPrefab;
    public PassiveSkillSO[] allPassiveSkills;

    public static bool IsSelectingSkill => Instance != null && Instance.panel.activeSelf;

    void Awake()
    {
        Instance = this;
        panel.SetActive(false);
    }

    public void OpenRandomChoices()
    {
        panel.SetActive(true);

        foreach (Transform child in buttonContainer)
            Destroy(child.gameObject);

        var choices = allPassiveSkills
        .Where(skill =>
        {
            var owned = GameManager.Instance.ownedPassiveSkills.Find(s => s.data == skill);
            return owned == null || owned.currentLevel < 5;
        })
        .OrderBy(x => Random.value)
        .Take(3);

        foreach (var skill in choices)
        {
            GameObject btn = Instantiate(skillButtonPrefab, buttonContainer);
            var owned = GameManager.Instance.ownedPassiveSkills.Find(s => s.data == skill);
            int displayLevel = owned == null ? 1 : owned.currentLevel + 1;

            btn.GetComponentsInChildren<TextMeshProUGUI>()[0].text = $"{skill.skillName} (Lv.{displayLevel})";
            //btn.GetComponentsInChildren<Image>().sprite = skill.icon;
            foreach(Image buttonIcon in btn.GetComponentsInChildren<Image>())
            {
                if(buttonIcon.name == "Image")
                {
                    buttonIcon.sprite = skill.icon;
                }
            }
            btn.GetComponentsInChildren<TextMeshProUGUI>()[1].text = $"{skill.description}";

            btn.GetComponent<Button>().onClick.AddListener(() =>
            {
                GameManager.Instance.AddOrUpgradePassiveSkill(skill);
                panel.SetActive(false);
                GameManager.Instance.ResumeGame();
            });
        }
    }
}
