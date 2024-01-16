using UnityEngine;

namespace Utils.PoolSystem
{
    [System.Serializable]
    public class PoolData
    {
        public GameObject prefab;

        public int preload;

        public int capacity;
    }
}