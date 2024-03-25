// Written By Asaf Benjaminov @ JStudios 2022

using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using JStudios.QuestSystem.Scripts.Enums;
using JStudios.QuestSystem.Scripts.Interfaces;
using JStudios.QuestSystem.Scripts.ScriptableObjects.Objectives;
using JStudios.Scripts.Helpers;
using UnityEngine;

namespace JStudios.QuestSystem.Scripts.ScriptableObjects.Quests
{
    /// <summary>
    /// A quest that supported multiple objectives, this in specific is an async objective quest.
    /// All objectives are listening to progress asynchronously. 
    /// </summary>
    [DisplayName("Async M.O.Q")]
    [CreateAssetMenu(fileName = "New Async Multiple Objective Quest", 
        menuName = "JStudios/Quest System/Quests/Multiple Objective Quests/Async Quest")]
    public class MultipleObjectiveQuest : Quest
    {
        public List<Objective> Objectives = new();
        public int ObjectiveCount => Objectives.Count;
        public int CompletedObjectivesCount { get; protected set; }
        
        protected override void OnEnable()
        {
            foreach (var objective in Objectives)
            {
                SubscribeObjectiveEvents(objective);
            }
        }

        protected override void OnDisable()
        {
            foreach (var objective in Objectives)
            {
                UnsubscribeObjectiveEvents(objective);
            }
        }

        protected override void StartInternal()
        {
            RawTarget = 0;
            
            foreach (var objective in Objectives)
            {
                RawTarget += objective.RawTarget;
                objective.Activate();
            }
        }

        /// <summary>
        /// Pass a context that is supported by this objective and it will progress with the amount specified
        /// </summary>
        /// <param name="context">The context to check</param>
        /// <param name="amountOfProgress">Default is 1</param>
        public override void Progress(IQuestContext context, float amountOfProgress = 1)
        {
            var objectiveWithContext = GetObjectivesWithContext(context);

            foreach (var objective in objectiveWithContext)
            {
                objective.Progress(context, amountOfProgress);
            }
        }

        private IEnumerable<Objective> GetObjectivesWithContext(IQuestContext context)
        {
            var objectiveWithContext = Objectives.Where(x => x.HasContext(context));
            return objectiveWithContext;
        }

        /// <summary>
        /// Checks if the context is supported by this quest
        /// </summary>
        /// <param name="context">The context to check</param>
        /// <returns>Indication if the context is supported</returns>
        public override bool HasContext(IQuestContext context)
        {
            if (Objectives == null) return false;

            var any = GetObjectivesWithContext(context).Any();

            return any;
        }

        internal override void Validate()
        {
            if (Objectives == null)
            {
                QuestSystemCore.Log.Error(JMessages.Errors.Quests.QuestHasNoObjectives);
            }
            else
            {
                var hasAnyNulls = Objectives.Any(x => x == null);

                if (hasAnyNulls)
                {
                    QuestSystemCore.Log.Error(JMessages.Errors.Quests.MultipleQuestMissingObjectiveInList, Name);
                }
            }
        }

        protected override void OnObjectiveStateChangedEvent(Objective objective)
        {
            var pendingActiveCount = Objectives.Count(x => x.State == ActiveState.PendingActive);
            var activeCount = Objectives.Count(x => x.State == ActiveState.Active);
            var pendingCompletedCount = Objectives.Count(x => x.State == ActiveState.PendingCompleted);
            var completedCount = Objectives.Count(x => x.State == ActiveState.Completed);
        
            if (pendingActiveCount == Objectives.Count)
            {
                SetState(ActiveState.PendingActive);
            }
            else if (activeCount > 0)
            {
                SetState(ActiveState.Active);
            }
            else if (pendingActiveCount == 0 
                     && activeCount == 0 
                     && pendingCompletedCount > 0)
            {
                SetState(ActiveState.PendingCompleted);
            }
        }

        protected override void OnObjectiveProgressEvent(Objective objective)
        {
            var delta = objective.RawCompleted - objective.PreviousRawCompleted;
            RawCompleted += delta;
            OnProgressEvent?.Invoke(this);
        }

        protected override void OnObjectiveCompletedEvent(Objective objective)
        {
            CompletedObjectivesCount = Objectives.Count(x => x.State == ActiveState.Completed);
        
            if (CompletedObjectivesCount == ObjectiveCount)
            {
                Complete();
            }
        }

        [ContextMenu("JStudios/Soft Reset")]
        public override void SoftReset()
        {
            base.SoftReset();
            
            if (Objectives == null) return;
            
            foreach (var objective in Objectives)
            {
                objective.SoftReset();
            }
            
            SoftResetInternal();
        }

        protected virtual void SoftResetInternal()
        {
            
        }
    }
}