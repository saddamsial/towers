using System.Collections.Generic;
using JStudios.QuestSystem.Scripts.Behaviours;
using JStudios.QuestSystem.Scripts.ScriptableObjects.References;
using JStudios.QuestSystemExample.Scripts.ScriptableObjects;
using JStudios.Scripts.Attributes;
using UnityEngine;

namespace JStudios.QuestSystemExample.Scripts.Snake
{
    public class SnakeBody : MonoBehaviour
    {
        [ReadOnly][SerializeField] private int BodyLength;
        public PlayerStats PlayerStats;
        public SnakePart SnakeHead;
        private readonly List<SnakePart> _bodyParts = new();
        public SnakePart SnakePartPrefab;
        [SerializeField] private QuestSystemChannelRef QuestSystemChannel;

        private void Awake()
        {
            PlayerStats.OnPointsGainedEvent += OnPointsGainedEvent;
        }

        private void Update()
        {
            SnakeHead.CurrentDirection = SnakeHead.Movement.Direction;
            
            foreach (var snakePart in _bodyParts)
            {
                snakePart.UpdatePosition();
            }
        }

        private void OnPointsGainedEvent()
        {
            BodyLength = PlayerStats.Level;
            
            UpdateBody();
        }

        private void UpdateBody()
        {
            if (BodyLength == 0 || BodyLength == _bodyParts.Count) return;

            var newSnakePart = Instantiate(SnakePartPrefab);

            if (BodyLength == 1)
            {
                SnakeHead.PlaceNextPart(newSnakePart);
            }
            else
            {
                _bodyParts[^1].PlaceNextPart(newSnakePart);
            }
            
            _bodyParts.Add(newSnakePart);
            var context = newSnakePart.GetComponent<QuestContextBehaviour>();
            
            QuestSystemChannel.Ref.Progress(context);
        }
    }
}