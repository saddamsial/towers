using JStudios.QuestSystem.Scripts.Interfaces;
using UnityEngine;

namespace JStudios.QuestSystemExample.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Grant Points Actions", menuName = "JStudios/Quest System Example/Completion Actions/Grant Points Action")]
    public class GrantPointsAction : ScriptableObject, ICompletionAction
    {
        public PlayerStats PlayerStats;
        public int AmountOfPoints;
        public void Execute()
        {
            PlayerStats.GrantPoints(AmountOfPoints);
        }
    }
}