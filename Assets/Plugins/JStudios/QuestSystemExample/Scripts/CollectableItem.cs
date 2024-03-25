using System.Collections;
using JStudios.QuestSystemExample.Scripts.ScriptableObjects;
using UnityEngine;

namespace JStudios.QuestSystemExample.Scripts
{
    public class CollectableItem : MonoBehaviour
    {
        private Collectable _collectable;
        private SpriteRenderer _renderer;
        [SerializeField] private float TimeToLive;

        private void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
        }

        public Collectable GetCollectable()
        {
            return _collectable;
        }
        
        public void SetCollectable(Collectable collectable)
        {
            _collectable = collectable;
            _renderer.sprite = _collectable.Image;

            StartCoroutine(nameof(Live));
        }

        IEnumerator Live()
        {
            yield return new WaitForSeconds(TimeToLive);
            Destroy(gameObject);
        }
    }
}