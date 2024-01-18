using System;
using UnityEngine;

namespace Utils
{
    public interface IDamageable
    {
        public float DamageTaken { get; set; }
        public bool Died { get; set; }

        public bool IsVulnerable { get; set; }
        public event Action OnDamage;
        void Damage(float amount);
    }
}