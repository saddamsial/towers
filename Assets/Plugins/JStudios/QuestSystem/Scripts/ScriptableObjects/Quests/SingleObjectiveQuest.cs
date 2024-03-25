// Written By Asaf Benjaminov @ JStudios 2022

using System.ComponentModel;
using JStudios.QuestSystem.Scripts.Interfaces;
using JStudios.QuestSystem.Scripts.ScriptableObjects.Objectives;
using JStudios.Scripts.Attributes;
using JStudios.Scripts.Helpers;
using UnityEditor;
using UnityEngine;

namespace JStudios.QuestSystem.Scripts.ScriptableObjects.Quests
{
    /// <summary>
    /// A quest that supports only a single objective.
    /// </summary>
    [DisplayName("S.O.Q")]
    public class SingleObjectiveQuest : Quest, IObjective
    {
        [SerializeField] [JStudios.Scripts.Attributes.ReadOnly] [ShowInlineInfo] protected Objective Objective;

        protected override void OnEnable()
        {
            VerifyObjective("On Enable");
            
            RawTarget = Objective.RawTarget;
            
            SubscribeObjectiveEvents(Objective);
        }

        internal override void OnAfterCreate()
        {
            VerifyObjective("On After Create");
        }

        private void VerifyObjective(string from = "")
        {
            if(from != "") JStudiosLog.Info("VerifyObjective " + from);

            if (Objective == null)
            {
                JStudiosLog.Info("Objective NULL on " + Name);
                Objective = TryCreateObjectiveInternal();   
                EditorUtility.SetDirty(this);
            }
            
            Objective.Name = "Objective for quest - " + Name;
            JStudiosAssetsHelper.Rename(Objective, UniqueId);
        }

        protected virtual Objective TryCreateObjectiveInternal()
        {
            return null;
        }
        
        protected override void OnDisable()
        {
            if (Objective == null) 
                return;
            
            UnsubscribeObjectiveEvents(Objective);
        }

        internal override bool OnBeforeDelete()
        {
            if (Objective == null) return true;

            var result = Objective.DeleteAsset();
            return result;
        }

        protected override void StartInternal()
        {
            if (Objective == null)
            {
                JStudiosLog.Error("Objective is null, please run a fix (Tools/JStudios/Quest System/Fix)");
                return;
            }

            RawTarget = Objective.RawTarget;
            Objective.Activate();
        }

        /// <summary>
        /// Check if the context specified is supported by the quest
        /// </summary>
        /// <param name="context">The context to check</param>
        /// <returns>Indication if the context is supported</returns>
        public override bool HasContext(IQuestContext context)
        {
            if (Objective == null)
            {
                JStudiosLog.Error("Objective is null, please run a fix (Tools/JStudios/Quest System/Fix)");
                return false;
            }
            else if (Objective.Context == null)
            {
                JStudiosLog.Error("Objective is missing a context, please run a fix (Tools/JStudios/Quest System/Fix)");
                return false;
            }
            
            var isSameContext = Objective.Context.GetUniqueId() == context.GetUniqueId();

            return isSameContext;
        }

        internal override void Validate()
        {
            VerifyObjective("Validate");
        }

        protected override void OnObjectiveStateChangedEvent(Objective objective)
        {
            if (objective.State == State) return;
            
            SetState(objective.State);
        }

        protected override void OnObjectiveProgressEvent(Objective objective)
        {
            RawCompleted = objective.RawCompleted;
            OnProgressEvent?.Invoke(this);
        }

        protected override void OnObjectiveCompletedEvent(Objective objective)
        {
            Complete();
        }

        /// <summary>
        /// Pass a context that is supported by this objective and it will progress with the amount specified
        /// </summary>
        /// <param name="context">The context to check</param>
        /// <param name="amountOfProgress">Default is 1</param>
        public override void Progress(IQuestContext context, float amountOfProgress = 1)
        {
            Objective.Progress(context, amountOfProgress);
        }

        /// <summary>
        /// Resets only the properties for the QuestSystem
        /// </summary>
        [ContextMenu("JStudios/Soft Reset")]
        public override void SoftReset()
        {
            base.SoftReset();
            
            if (Objective == null) return;
            
            RawTarget = Objective.RawTarget;
            Objective.SoftReset();
        }

        /// <summary>
        /// Set the supported context for this quest
        /// The old context will no longer be supported
        /// </summary>
        /// <param name="context">The context to support</param>
        public void SetContext(IQuestContext context)
        {
            Objective.SetContext(context);
        }

        /// <summary>
        /// Clear the context if it is supported
        /// </summary>
        /// <param name="context">The context to clear</param>
        public void ClearContext(IQuestContext context)
        {
            Objective.ClearContext(context);
        }

        public string GetUniqueId()
        {
            return UniqueId;
        }
    }
}