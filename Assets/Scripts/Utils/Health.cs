using System;
using Guns;
using Managers;
using Tower.Floor;
using UnityEngine;

namespace Utils
{
    public class Health : Progressive, IDamageable, IHealable
    {
        [SerializeField] private Transform hitTransform;
        public float damageMultiplier;
        private const float DeathThreshold = 0f;
        public Transform GetTransform => hitTransform;
        public FloorBase myFloor;
        public float DamageTaken { get; set; }
        public bool Died { get; set; }
        public bool IsVulnerable { get; set; } = true;
        public void Damage(float amount)
        {
            if (ReachedThreshold(DeathThreshold)) return;
            amount *= damageMultiplier;
            if (IsVulnerable) Decrease(amount);

            DamageTaken = amount;

            if (!ReachedThreshold(DeathThreshold)) return;

            Died = true;
            Current = 0f;
            // if (!myFloor && TryGetComponent<GunBase>(out var gunbase)) myFloor = gunbase.myFloor;
            GameController.onDied?.Invoke(myFloor);
        }

        public void Heal(float amount)
        {
            if (ReachedInitial()) return;

            Increase(amount);

            if (!ReachedInitial()) return;

            Current = Initial;
        }

        public void SetupHealth(float hp) => Current = initial = 8 + hp * 2;
    }
}