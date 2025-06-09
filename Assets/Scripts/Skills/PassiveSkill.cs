[System.Serializable]
public class PassiveSkill
{
    public PassiveSkillSO data;
    public int currentLevel = 1;
    public float lastActivationTime = 0f;

    public bool CanLevelUp => currentLevel < data.levelDataList.Count;

    public void LevelUp()
    {
        if (CanLevelUp)
            currentLevel++;
    }
}
