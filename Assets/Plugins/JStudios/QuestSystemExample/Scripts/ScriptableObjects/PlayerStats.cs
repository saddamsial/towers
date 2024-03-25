using UnityEngine;
using UnityEngine.Events;

namespace JStudios.QuestSystemExample.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Player Stats", menuName = "JStudios/Quest System Example/Player Stats")]
    public class PlayerStats : ScriptableObject
    {
        public UnityAction OnPointsGainedEvent;
        
        public int PointsPerLevel = 5;
        public int Level;
        public int Points;

        private void OnEnable()
        {
            DetermineLevel();
        }

        public void GrantPoints(int points)
        {
            Points += points;

            DetermineLevel();
            
            OnPointsGainedEvent?.Invoke();
        }

        private void DetermineLevel()
        {
            Level = (Points / PointsPerLevel);
        }
    }
}