// Written By Asaf Benjaminov @ JStudios 2022

using System.ComponentModel;
using JStudios.QuestSystem.Scripts.Interfaces;
using JStudios.Scripts.Attributes;
using UnityEngine;
using Object = UnityEngine.Object;

namespace JStudios.QuestSystem.Scripts.ScriptableObjects.Objectives
{
    /// <summary>
    /// An objective that completed once an action is performed
    /// </summary>
    [DisplayName("Action Objective")]
    public class ActionObjective : Objective
    {
        [SerializeField]
        [RequireInterface(typeof(IRequirement))]
        private Object _completionRequirement;
        
        private IRequirement CompletionRequirement => _completionRequirement as IRequirement;
        
        private void OnEnable()
        {
            JQuestSystemEvents.OnFixCoreCompletedEvent.Subscribe(OnFixCompleted);
            RawTarget = 1;
        }

        private void OnDestroy()
        {
            JQuestSystemEvents.OnFixCoreCompletedEvent.Unsubscribe(OnFixCompleted);
        }

        private void OnFixCompleted()
        {
            SetContext(QuestSystemCore.GlobalContextInstance);
        }

        /// <summary>
        /// Pass a context that is supported by this objective and it will progress with the amount specified
        /// </summary>
        /// <param name="context">The context to check</param>
        /// <param name="amountOfProgress">Default is 1</param>
        public override void Progress(IQuestContext context, float amountOfProgress = 1)
        {
            if (CompletionRequirement == null || !CompletionRequirement.IsMet()) return;
            
            base.Progress(context, amountOfProgress);
        }

        protected override void StartInternal()
        {
            Progress(Context);
        }
    }
}