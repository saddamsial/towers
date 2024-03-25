using System.Collections;
using System.Collections.Generic;
using JStudios.QuestSystemExample.Scripts.ScriptableObjects;
using UnityEngine;
using Random = UnityEngine.Random;

namespace JStudios.QuestSystemExample.Scripts
{
    public class CollectablesSpawn : MonoBehaviour
    {
        public float TimeBetweenSpawns;
        [SerializeField] private List<Collectable> Collectables;
        [SerializeField] private CollectableItem Prefab;
        [SerializeField] private BoxCollider2D Bounds;

        private Bounds _bounds;
        
        private bool isSpawning = true;
        
        private void Awake()
        {
            StartCoroutine(nameof(SpawnCollectables));
            _bounds = Bounds.bounds;
        }

        IEnumerator SpawnCollectables()
        {
            while (isSpawning)
            {
                yield return new WaitForSeconds(TimeBetweenSpawns);
                
                var position = GetRandomPosition();
                var collectable = GetRandomCollectable();
                
                if(collectable == null) continue;
                
                var collectableItem = GameObject.Instantiate(Prefab);
                collectableItem.transform.position = position;
                collectableItem.SetCollectable(collectable);
            }
        }

        Collectable GetRandomCollectable()
        {
            if (Collectables == null) return null;
            
            var index = Random.Range(0, Collectables.Count);
            return Collectables[index];
        }
        
        Vector2 GetRandomPosition()
        {
            var randomX = Random.Range(_bounds.min.x -.5f, _bounds.max.x -.5f);
            var randomY = Random.Range(_bounds.min.y -.5f, _bounds.max.y -.5f);
            
            return new Vector2(randomX, randomY);
        }
    }
}