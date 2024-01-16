using System;
using UnityEngine;

namespace Utils
{
    public abstract class Progressive : MonoBehaviour
    {
        [SerializeField] protected float initial;

        public float _current;
        public float Current
        {
            get => _current;
            set
            {
                _current = value;
                OnValueChanged?.Invoke(Ratio);
            }
        }
        public float Ratio => Current / Initial;

        public float Initial => initial;

        public Action<float> OnValueChanged;

        protected virtual void Awake()
        {
            Current = Initial;
        }

        public void IncreaseInitial(float value)
        {
            initial += value;
            Current = Initial;
        }

        protected void Decrease(float amount)
        {
            Current -= amount;
        }

        protected void Increase(float amount)
        {
            Current += amount;
        }

        protected bool ReachedThreshold(float amount)
        {
            return Current <= amount;
        }

        public bool ReachedInitial()
        {
            return Current >= Initial;
        }

    }
}
