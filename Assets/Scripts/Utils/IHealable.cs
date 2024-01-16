using System;

namespace Utils
{
    public interface IHealable
    {
        public event Action OnHeal;
        public event Action OnFull;

        void Heal(float amount);
    }
}