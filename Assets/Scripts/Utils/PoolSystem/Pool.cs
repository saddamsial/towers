using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using UnityEngine;
using Utils.PoolSystem;

namespace Utils.PoolSystem
{
    public class Pool : MonoBehaviour
    {
        public List<PoolData> pools = new List<PoolData>();
        public List<Despawn> despawn = new List<Despawn>();

        public void Start()
        {
            CreatePools();
            Debug.Log("<color=yellow>PoolSystem Initialized</color>");
        }

        private void CreatePools()
        {
            foreach (var pool in pools)
            {
                var newPool = new GameObject();
                newPool.name = pool.prefab.name;
                // newPool.transform.parent = transform;

                var poolComponent = newPool.AddComponent<LeanGameObjectPool>();
                poolComponent.Prefab = pool.prefab;
                poolComponent.Preload = pool.preload;
                poolComponent.Capacity = pool.capacity;
                poolComponent.PreloadAll();
            }
        }

        public void AddAutoDespawn(Despawn despawn)
        {
            StartCoroutine(DelayedDespawn(despawn.Prefab, despawn.Delay));
        }

        private IEnumerator DelayedDespawn(GameObject obj, float time)
        {
            yield return new WaitForSeconds(time);
            obj.Despawn();
        }
    }
}