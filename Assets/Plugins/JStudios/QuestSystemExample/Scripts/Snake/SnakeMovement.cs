using System.Collections;
using JStudios.QuestSystem.Scripts.Behaviours;
using JStudios.QuestSystem.Scripts.ScriptableObjects.References;
using UnityEngine;
using UnityEngine.Events;

namespace JStudios.QuestSystemExample.Scripts.Snake
{
    public class SnakeMovement : MonoBehaviour
    {
        public UnityAction<ChangeDirectionCommand> OnDirectionChangedEvent;
        
        public QuestSystemChannelRef QuestSystemChannel;
        public float Speed;
        
        public Vector3 Direction;
        private Quaternion _rotation;
        
        private float _totalDelta;

        private QuestContextBehaviour _movementContext;

        private bool _directionChangeLocked = false;
        
        private void Awake()
        {
            _movementContext = GetComponent<QuestContextBehaviour>();
        }

        private void Start()
        {
            StartCoroutine(ReportDistanceTraveled());
        }

        private void OnDestroy()
        {
            StopCoroutine(ReportDistanceTraveled());
        }

        private void Update()
        {
            if (!_directionChangeLocked)
            {
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    SetDirectionAndRotation(Vector2.right, Quaternion.Euler(0, 0, -90));
                }
                else if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    SetDirectionAndRotation(Vector2.up, Quaternion.Euler(0, 0, 0));
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    SetDirectionAndRotation(Vector2.left, Quaternion.Euler(0, 0, 90));
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    SetDirectionAndRotation(Vector2.down, Quaternion.Euler(0, 0, 180));
                }
            }

            var delta = Direction * (Speed * Time.deltaTime);
            
            var snakeTransform = transform;
            snakeTransform.rotation = _rotation;
            snakeTransform.position += delta;
            _totalDelta += delta.magnitude;
        }

        private void SetDirectionAndRotation(Vector2 direction, Quaternion rotation)
        {
            _rotation = rotation;
            Direction = direction;
            OnDirectionChangedEvent?.Invoke(new ChangeDirectionCommand()
            {
                Direction = Direction,
                Position = transform.position
            });
        }

        IEnumerator ReportDistanceTraveled()
        {
            while (true)
            {
                QuestSystemChannel.Ref.Progress(_movementContext, _totalDelta);
                _totalDelta = 0;
                yield return new WaitForSeconds(1f);
            }
            // ReSharper disable once IteratorNeverReturns
        }
    }
    
    public class ChangeDirectionCommand
    {
        public Vector3 Direction;
        public Vector3 Position;
    }
}