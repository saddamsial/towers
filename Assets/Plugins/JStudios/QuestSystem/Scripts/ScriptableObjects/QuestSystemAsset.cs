// Written By Asaf Benjaminov @ JStudios 2022

using System.Collections.Generic;
using System.Linq;
using JStudios.QuestSystem.Scripts.Enums;
using JStudios.QuestSystem.Scripts.Interfaces;
using JStudios.Scripts.Attributes;
using JStudios.Scripts.ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;

namespace JStudios.QuestSystem.Scripts.ScriptableObjects
{
    /// <summary>
    /// The base class for an asset in the quest system
    /// </summary>
    /// <typeparam name="T">The type of the asset</typeparam>
    public abstract class QuestSystemAsset<T>: JListAsset, IQuestSystemAsset where T : QuestSystemAsset<T> 
    {
        public UnityAction<T> OnStateChangedEvent;
        public UnityAction<T> OnProgressEvent;
        public UnityAction<T> OnActiveEvent;
        public UnityAction<T> OnPendingCompletedEvent;
        public UnityAction<T> OnCompletedEvent;
        
        [Header("General")]
        [OrderInEditor(1)]
        public string Name;
        
        [OrderInEditor(2)]
        [RequireInterface(typeof(ICompletionAction))]
        [SerializeField] protected List<Object> _completionActions;
        public IEnumerable<ICompletionAction> CompletionActions => _completionActions.OfType<ICompletionAction>();
        
        public abstract float RawTarget { get; internal set; }
        
        [OrderInEditor(4)]
        [ReadOnly] public float RawCompleted;
        public float PercentageCompleted => Mathf.Clamp01(RawCompleted / RawTarget);

        private T ThisAsT => this as T;
        
        [ReadOnly]
        [SerializeField]
        private ActiveState ActiveState = ActiveState.PendingActive;
        
        public ActiveState State => ActiveState;

        protected void SetState(ActiveState state)
        {
            var previousValue = ActiveState;
            ActiveState = state;
                
            if (previousValue != ActiveState)
            {
                OnStateChanged();
            }
        }
        
        private void OnStateChanged()
        {
            OnStateChangedEvent?.Invoke(ThisAsT);

            if (State == ActiveState.Active)
            {
                OnActiveEvent?.Invoke(ThisAsT);    
            }
            else if (State == ActiveState.PendingCompleted)
            {
                OnPendingCompletedEvent?.Invoke(ThisAsT);
            }
            else if (State == ActiveState.Completed)
            {
                OnCompletedEvent?.Invoke(ThisAsT);
            }
            
            OnStateChangedInternal(); 
        }
        
        protected virtual void OnStateChangedInternal() {}

        public abstract void Progress(IQuestContext context, float amountOfProgress = 1);


        public abstract void SoftReset();
        
        public void Activate()
        {
            if (State != ActiveState.PendingActive) return; 
            
            SetState(ActiveState.Active);
            
            StartInternal();
        }
        
        protected virtual void StartInternal()
        {
        }
        
        public virtual void Complete()
        {
            RawCompleted = RawTarget;

            if (_completionActions != null)
            {
                foreach (var completionAction in CompletionActions)
                {
                    completionAction.Execute();
                }    
            }
            
            SetState(ActiveState.Completed);
            CompleteInternal();
        }

        protected virtual void CompleteInternal() { }
    }
}