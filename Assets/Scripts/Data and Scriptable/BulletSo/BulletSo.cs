using NaughtyAttributes;
using UnityEngine;

namespace Data_and_Scriptable.BulletSo
{
    public enum BulletTypes
    {
        MachineGun = 0,
        Cannon = 1,
        Rocket = 2,
        Laser = 3,
        IceCannon = 4,
        BombLauncher = 5,
        Tesla = 6,
        Shockwave = 7,
        Plasma = 8,
        FlameGun = 9
    }

    [CreateAssetMenu(menuName = "Bullet")]
    public class BulletSo : ScriptableObject
    {
        public BulletTypes bulletType;
        public GameObject prefab;
        public bool isLaser;
        [HideIf("isLaser")]
        public float speed;
        [HideIf("isLaser")]
        public float despawnTime;
        public float damage;
        [HideIf("isLaser")]
        public GameObject effect;
    }
}