using NaughtyAttributes;
using UnityEngine;

namespace Data_and_Scriptable.GunSo
{
    [CreateAssetMenu(menuName = "Gun")]
    public class GunSo : ScriptableObject
    {
        public GameObject myPrefab;
        public BulletSo.BulletSo myBullet;
        public bool isLaser;
        [HideIf("isLaser")]
        public float frequency;
        [HideIf("isLaser")]
        public float coolDownTime;
        [HideIf("isLaser")]
        public int ammoCount;
        [HideIf("isLaser")]
        public GameObject muzzleEffect;

    }
}