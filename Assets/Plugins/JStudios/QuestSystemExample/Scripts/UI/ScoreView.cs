using JStudios.QuestSystemExample.Scripts.ScriptableObjects;
using TMPro;
using UnityEngine;

namespace JStudios.QuestSystemExample.Scripts.UI
{
    public class ScoreView : MonoBehaviour
    {
        private TextMeshProUGUI _text;
        [SerializeField] private PlayerStats PlayerStats;

        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
            PlayerStats.OnPointsGainedEvent += OnPointsGainedEvent;
        }

        private void OnPointsGainedEvent()
        {
            _text.SetText($"Score: {PlayerStats.Points}");
        }
    }
}