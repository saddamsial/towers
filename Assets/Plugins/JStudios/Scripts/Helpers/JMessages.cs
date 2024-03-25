// Written By Asaf Benjaminov @ JStudios 2022

namespace JStudios.Scripts.Helpers
{
    public static class JMessages
    {
        public static JErrors Errors = new();
        public static JInfoMessages Info = new();
    }
    
    public class JInfoMessages
    {
        public string AssetAtPathNull = "JEI_G001 - Asset at {0} can not load";

        public readonly QuestSystemInfoMessages Quests = new();
    }
    
    public class QuestSystemInfoMessages
    {
        public string CreatedObjectiveForSoq = "JEI_Q001 - Created objective for {0}";
        public string ContextHasBeenReplaced = "JEI_Q002 - Context has been replaced on {0}";
    }
    
    public class JErrors
    {
        public readonly QuestSystemErrors Quests = new();
    }

    public class QuestSystemErrors
    {
        public string QuestHasNoObjectives = "JER_Q001 - Quest has no objectives";
        public string MultipleQuestMissingObjectiveInList = "JER_Q002 - Quest {0} is missing objective/s";
        public string ContextIsNullForIObjective = "JER_Q003 - Objective or Single Objective Quest {0} has either no context attached (null) or an incorrect type, make sure you use IQuestContext when using IObjective.SetContext";
    }
}