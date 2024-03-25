using JStudios.QuestSystem.Scripts.ScriptableObjects.Objectives;
using TMPro;
using UnityEngine;

namespace JStudios.QuestSystemExample.Scripts.UI
{
    public class ObjectiveProgressView : MonoBehaviour
    {
        public ProgressBar ObjectiveProgressBar;
        [SerializeField] private TextMeshProUGUI ObjectiveName;

        private Objective _objective;
        
        public void SetObjective(Objective objective)
        {
            _objective = objective;
            _objective.OnProgressEvent += OnProgressEvent;
            _objective.OnCompletedEvent += OnCompletedEvent;
            _objective.OnPendingCompletedEvent += OnPendingCompleteEvent;

            ObjectiveName.SetText(_objective.Name);
        }

        private void OnPendingCompleteEvent(Objective objective)
        {
            ObjectiveProgressBar.SetColor(Color.yellow);
        }

        private void OnCompletedEvent(Objective objective)
        {
            ObjectiveProgressBar.SetColor(Color.gray);
        }

        private void OnProgressEvent(Objective objective)
        {
            ObjectiveProgressBar.SetPercentage(objective.PercentageCompleted);
        }
    }
}