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

        private const float DeathThreshold = 0f;
        public Transform GetTransform => hitTransform;

        public FloorBase myFloor;
        public float DamageTaken { get; set; }

        public bool Died { get; set; }

        public bool IsVulnerable { get; set; } = true;

        public event Action OnDamage;
        public event Action OnHeal;
        public event Action OnFull;

        public void Damage(float amount)
        {
            if (ReachedThreshold(DeathThreshold)) return;

            if (IsVulnerable) Decrease(amount);

            DamageTaken = amount;

            OnDamage?.Invoke();

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

            OnHeal?.Invoke();

            if (!ReachedInitial()) return;

            Current = Initial;
            OnFull?.Invoke();
        }

        public void SetupHealth(float hp)
        {
            Current = initial = hp;

        }
    }
}