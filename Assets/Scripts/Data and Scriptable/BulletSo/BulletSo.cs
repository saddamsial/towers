using UnityEngine;

namespace Data_and_Scriptable.BulletSo
{
    public enum BulletTypes
    {
        MachineGun = 0,
        Cannon = 1,
        FlameGun = 2,
        Rocket = 3,
        Laser = 4,
        IceCannon = 5,
        BombLauncher = 6,
        Tesla = 7,
        Shockwave = 8,
        Plasma = 9
    }

    [CreateAssetMenu(menuName = "Bullet")]
    public class BulletSo : ScriptableObject
    {
        public BulletTypes bulletType;
        public GameObject prefab;
        public float speed;
        public float despawnTime;
        public float damage;
        public GameObject effect;
    }
}