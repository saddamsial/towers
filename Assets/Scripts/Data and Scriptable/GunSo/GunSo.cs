using NaughtyAttributes;
using UnityEngine;

namespace Data_and_Scriptable.GunSo
{
    [CreateAssetMenu(menuName = "Gun")]
    public class GunSo : ScriptableObject
    {
        public string gunName;
        public GameObject myPrefab;
        public bool isEmpty;
        [HideIf("isEmpty")]
        public BulletSo.BulletSo myBullet;
        [HideIf("isEmpty")]
        public bool isLaser;
        [HideIf(EConditionOperator.Or, "isLaser", "isEmpty")]
        public float frequency;
        [HideIf(EConditionOperator.Or, "isLaser", "isEmpty")]
        public float coolDownTime;
        [HideIf(EConditionOperator.Or, "isLaser", "isEmpty")]
        public int ammoCount;
    }
}