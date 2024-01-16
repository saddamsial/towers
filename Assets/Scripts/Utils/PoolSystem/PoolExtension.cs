using Lean.Pool;
using UnityEngine;

namespace Utils.PoolSystem
{
    public static class PoolSystemExtensions
    {
        public static Pool _pool;

        public static Pool Pool
        {
            get
            {
                if (_pool == null)
                    _pool = Object.FindObjectOfType<Pool>();
                return _pool;
            }
        }

        public static GameObject DelayDeSpawn(this GameObject gameObject, float delay)
        {
            Pool.AddAutoDespawn(new Despawn(gameObject, delay));
            return gameObject;
        }

        public static GameObject Spawn(this GameObject prefab, Vector3 position, Quaternion rotation)
        {
            return LeanPool.Spawn(prefab, position, rotation);
        }

        public static GameObject Spawn(this GameObject prefab, Vector3 position, Quaternion rotation, Transform parent)
        {
            return LeanPool.Spawn(prefab, position, rotation, parent);
        }

        public static void Despawn(this GameObject prefab)
        {
            LeanPool.Despawn(prefab);
        }

        public static void Despawn(this GameObject prefab, float delay)
        {
            LeanPool.Despawn(prefab, delay);
        }

        public static GameObject[] SpawnFor(this GameObject prefab, int count)
        {
            var result = new GameObject[count];
            for (var i = 0; i < count; i++)
            {
                result[i] = LeanPool.Spawn(prefab);
            }

            return result;
        }
    }
}