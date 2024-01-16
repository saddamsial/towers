using UnityEngine;

namespace Utils.PoolSystem
{
    public class Despawn
    {
        public float Delay;
        public GameObject Prefab;

        public Despawn(GameObject prefab, float delay)
        {
            Delay = delay;
            Prefab = prefab;
        }
    }
}