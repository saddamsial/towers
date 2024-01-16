using System;
using UnityEngine;

namespace Data_and_Scriptable.Resources
{
    [CreateAssetMenu(menuName = "Economy/Resource")]
    public class ResourceSo : ScriptableObject
    {
        [Header("Start Amount")]
        [SerializeField] private int initial;

        [SerializeField] private int currentDebug;

        [SerializeField] private Sprite resourceSprite;
        public event Action OnValueChanged;
    
        public int CurrentValue
        {
            get => ES3.Load($"{name}_currency", initial);
            set
            {
                ES3.Save($"{name}_currency", value);
                OnValueChanged?.Invoke();
            }
        }

        public Sprite ResourceSprite => resourceSprite;

        private void OnValidate()
        {
            currentDebug = CurrentValue;
        }

        public void Increase(int amount = 1)
        {
            CurrentValue += amount;
        }

        public void Decrease(int amount = 1)
        {
            CurrentValue -= amount;
        }
    
        public bool HasEnoughAmount(int amount)
        {
            return CurrentValue >= amount;
        }

        public bool HasResource()
        {
            return CurrentValue > 0;
        }
    
    }
}
