// Written By Asaf Benjaminov @ JStudios 2022

using System.ComponentModel;
using JStudios.QuestSystem.Scripts.Enums;
using JStudios.QuestSystem.Scripts.Interfaces;
using JStudios.Scripts.Attributes;
using JStudios.Scripts.Helpers;
using UnityEngine;
using Object = UnityEngine.Object;

namespace JStudios.QuestSystem.Scripts.ScriptableObjects.Objectives
{
    /// <summary>
    /// The base class for objectives in the QuestSystem
    /// </summary>
    [DisplayName("Objective")]
    [CreateAssetMenu(fileName = "New Objective", menuName = "JStudios/Quest System/Objectives/Objective")]
    public class Objective : QuestSystemAsset<Objective>, IObjective
    {
        [OrderInEditor(1)]
        [SerializeField]
        [RequireInterface(typeof(IQuestContext), true)]
        private Object _context;
        public IQuestContext Context => _context as IQuestContext;

        [SerializeField] private float _rawTarget;
        
        [OrderInEditor(5)]
        [Tooltip("Change the objective ActiveState to ActiveState.Completed once the target is obtained.")]
        public bool CompleteInstantly;
        
        public float PreviousRawCompleted { get; private set; }

        public override float RawTarget
        {
            get => _rawTarget;
            internal set => _rawTarget = value;
        }

        public string GetUniqueId()
        {
            return UniqueId;
        }
        
        /// <summary>
        /// Sets the context for this objective which will make it progress.
        /// </summary>
        /// <param name="context">The new context</param>
        public void SetContext(IQuestContext context)
        {
            if(_context != null)
                if (HasContext(context)) return;
                else QuestSystemCore.Log.Info(JMessages.Info.Quests.ContextHasBeenReplaced, Name);
            
            _context = context as Object;
        }

        /// <summary>
        /// Clears the context for the objective, if there is a different context then sent, it will not be cleared
        /// </summary>
        /// <param name="context">The context to be cleared</param>
        public void ClearContext(IQuestContext context)
        {
            if (!HasContext(context)) return;

            _context = null;
        }
        
        /// <summary>
        /// Checks if the context is supported by this objective
        /// </summary>
        /// <param name="context">The context to check</param>
        /// <returns>Indication if the context is supported</returns>
        public bool HasContext(IQuestContext context)
        {
            var hasContext = context != null
                             && Context != null
                             && context.GetUniqueId() == Context.GetUniqueId();

            return hasContext;
        }

        private void Progress(float amountOfProgress = 1)
        {
            if (State != ActiveState.Active) return;
            
            PreviousRawCompleted = RawCompleted;

            var min = RawTarget < 0 ? RawTarget : 0;
            var max = RawTarget < 0 ? 0 : RawTarget;
            
            RawCompleted = Mathf.Clamp(RawCompleted + amountOfProgress, min, max);
            
            OnProgressEvent?.Invoke(this);
            
            if (RawTarget - RawCompleted < Mathf.Epsilon)
            {
                if (CompleteInstantly)
                {
                    Complete();    
                }
                else
                {
                    ReadyForComplete();
                }
            }
        }
        
        /// <summary>
        /// Pass a context that is supported by this objective and it will progress with the amount specified
        /// </summary>
        /// <param name="context">The context to check</param>
        /// <param name="amountOfProgress">Default is 1</param>
        public override void Progress(IQuestContext context, float amountOfProgress = 1)
        {
            if (State != ActiveState.Active 
                || !HasContext(context)) return;

            Progress(amountOfProgress);
        }

        [ContextMenu("JStudios/Soft Reset")]
        public override void SoftReset()
        {
            RawCompleted = 0;
            SetState(ActiveState.PendingActive);
        }
        
        /// <summary>
        /// Mark this objective as ready for complete
        /// </summary>
        public void ReadyForComplete()
        {
            PreviousRawCompleted = RawCompleted;
            RawCompleted = RawTarget;
            
            SetState(ActiveState.PendingCompleted);
        }

        /// <summary>
        /// Mark this objective as completed.
        /// Set the previous completed raw value to the current raw value and executes completion actions if any exist.
        /// </summary>
        public override void Complete()
        {
            PreviousRawCompleted = RawCompleted;
            
            base.Complete();
        }
    }
}