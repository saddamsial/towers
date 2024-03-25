// Written By Asaf Benjaminov @ JStudios 2022

using System.Collections.Generic;
using System.Linq;
using JStudios.QuestSystem.Scripts.Enums;
using JStudios.QuestSystem.Scripts.Interfaces;
using JStudios.QuestSystem.Scripts.ScriptableObjects.Objectives;
using JStudios.Scripts.Attributes;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;

namespace JStudios.QuestSystem.Scripts.ScriptableObjects.Quests
{
    /// <summary>
    /// The base class for all quests in the QuestSystem.
    /// </summary>
    public abstract class Quest : QuestSystemAsset<Quest>
    {
        public UnityAction<Quest> OnAvailabilityChangedEvent;
        
        private bool _isAvailable;
        
        [OrderInEditor(3)]
        [RequireInterface(typeof(IRequirement))]
        [SerializeField] protected List<Object> _requireBeforeActive;
        
        public IEnumerable<IRequirement> RequireBeforeActive => _requireBeforeActive.OfType<IRequirement>();
        
        [ReadOnly]
        [SerializeField]
        [OrderInEditor(5)]
        protected float _rawTarget;
        public override float RawTarget
        {
            get => _rawTarget;
            internal set => _rawTarget = value;
        }

        /// <summary>
        /// Refresh the state of the quest, use this normally once per frame at the beginning of the frame
        /// </summary>
        public void Refresh()
        {
            var isAvailable = IsAvailable();

            if (_isAvailable != isAvailable)
            {
                OnAvailabilityChangedEvent?.Invoke(this);
            }

            _isAvailable = isAvailable;
        }
        
        /// <summary>
        /// Check if all the quest requirements are met.
        /// </summary>
        /// <returns>Indicator weather the quest is available or not</returns>
        public bool IsAvailable()
        {
            var result = State == ActiveState.PendingActive
                         && MeetsRequirements();

            return result;
        }
        
        /// <summary>
        /// Check if the quest meets all its requirements.
        /// </summary>
        /// <returns>Indicator weather the quest meets all the requirements, if there are no requirements - returns true.</returns>
        public virtual bool MeetsRequirements()
        {
            if (_requireBeforeActive == null) return true;

            var meetsRequirements = RequireBeforeActive.All(x => x.IsMet());

            return meetsRequirements;
        }
        
        protected void SubscribeObjectiveEvents(Objective objective)
        {
            objective.OnCompletedEvent += OnObjectiveCompletedEvent;
            objective.OnProgressEvent += OnObjectiveProgressEvent;
            objective.OnStateChangedEvent += OnObjectiveStateChangedEvent;
        }
        
        protected void UnsubscribeObjectiveEvents(Objective objective)
        {
            objective.OnCompletedEvent -= OnObjectiveCompletedEvent;
            objective.OnProgressEvent -= OnObjectiveProgressEvent;
            objective.OnStateChangedEvent -= OnObjectiveStateChangedEvent;
        }
        
        protected virtual void OnEnable()
        {
        }

        protected virtual void OnDisable()
        {
        }

        /// <summary>
        /// Resets all the properties for the QuestSystem
        /// </summary>
        public override void SoftReset()
        {
            RawCompleted = 0;
            SetState(ActiveState.PendingActive);
        }

        private void Reset()
        {
            Validate();
        }

        /// <summary>
        /// Check if the context specified is supported by the quest
        /// </summary>
        /// <param name="context">The context to check</param>
        /// <returns>Indication if the context is supported</returns>
        public abstract bool HasContext(IQuestContext context);
        internal abstract void Validate();
        protected abstract void OnObjectiveStateChangedEvent(Objective objective);
        protected abstract void OnObjectiveProgressEvent(Objective objective);
        protected abstract void OnObjectiveCompletedEvent(Objective objective);
    }
}
