using NaughtyAttributes;
using UnityEngine;

namespace Data_and_Scriptable.BulletSo
{
    public enum BulletTypes
    {
        MachineGun = 0,
        BombLauncher = 1,
        Shotgun = 2,
        Lightning = 3,
        Cannon = 4,
        IceCannon = 5,
        Rocket = 6,
        Laser = 7,
        Plasma = 8,//flame gun

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