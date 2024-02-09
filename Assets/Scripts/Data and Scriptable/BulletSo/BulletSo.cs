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
        Shotgun = 6,
        Tesla = 7,
        Plasma = 8,//flame gun
        Shockwave = 9
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
    }
}