using System.Collections.Generic;
using UnityEngine;

namespace JStudios.QuestSystemExample.Scripts.Snake
{
    public class SnakePart : MonoBehaviour
    {
        public float Offset;
        [HideInInspector] public SnakeMovement Movement;
        private SnakePart _parent;
        [HideInInspector] public Vector3 CurrentDirection = Vector3.zero;
        [HideInInspector] public SpriteRenderer Sprite;
        private readonly Queue<ChangeDirectionCommand> _changeDirectionCommands = new();

        private void Awake()
        {
            Movement = GetComponent<SnakeMovement>();
            Sprite = GetComponent<SpriteRenderer>();
        }

        private void OnDirectionChangedEvent(ChangeDirectionCommand command)
        {
            _changeDirectionCommands.Enqueue(command);
        }

        public void PlaceNextPart(SnakePart newChild)
        {
            Movement.OnDirectionChangedEvent += newChild.OnDirectionChangedEvent;
            newChild.Movement = Movement;
            newChild.CurrentDirection = CurrentDirection;
            newChild._parent = this;

            foreach (var changeDirectionCommand in _changeDirectionCommands)
            {
                newChild._changeDirectionCommands.Enqueue(changeDirectionCommand);
            }

            newChild.transform.position = transform.position + ((CurrentDirection * -1).normalized * this.Offset);
        }

        public void UpdatePosition()
        {
            if (_changeDirectionCommands.Count != 0)
            {
                var nextChange = _changeDirectionCommands.Peek();
                
                var directionToPoint = 
                    (nextChange.Position - transform.position).normalized;
                
                if (directionToPoint != CurrentDirection)
                {
                    CurrentDirection = nextChange.Direction;
                    transform.position = _parent.transform.position + 
                                         ((nextChange.Direction * -1).normalized * _parent.Offset);
                    _changeDirectionCommands.Dequeue();
                }
            }

            transform.position += (Movement.Speed * CurrentDirection) * Time.deltaTime;
        }
    }
}