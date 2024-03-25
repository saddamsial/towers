using JStudios.QuestSystem.Scripts.ScriptableObjects.References;
using JStudios.QuestSystemExample.Scripts.ScriptableObjects;
using UnityEngine;

namespace JStudios.QuestSystemExample.Scripts.Snake
{
    public class SnakeCollector : MonoBehaviour
    {
        [SerializeField] private PlayerStats PlayerStats;
        [SerializeField] private QuestSystemChannelRef QuestSystemChannel;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.TryGetComponent(typeof(CollectableItem), out var component)) return;
            
             if (component is CollectableItem item)
             {
                 QuestSystemChannel.Ref.Progress(item.GetCollectable());
                 PlayerStats.GrantPoints(item.GetCollectable().Points);
                 
                 Destroy(other.gameObject);
             }
        }
    }
}