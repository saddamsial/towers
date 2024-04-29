using UnityEngine;

[CreateAssetMenu(menuName = "Quest Item")]
public class QuestItem : ScriptableObject
{
    public string detail;
    public LootItem itemType;
    public int earnedExperience = 1;

}
