// Written By Asaf Benjaminov @ JStudios 2022

using System;
using System.Collections.Generic;
using System.Linq;
using JStudios.QuestSystem.Scripts.Interfaces;
using JStudios.Scripts.Attributes;
using UnityEngine;
using Object = UnityEngine.Object;

namespace JStudios.QuestSystem.Scripts.Behaviours
{
    /// <summary>
    /// A connecting behaviour between some object that is meant to be a context for a quest and a quest (or objective)
    /// </summary>
    [ExecuteAlways]
    public class QuestContextBehaviour : MonoBehaviour, IQuestContext
    {
        [ReadOnly] [SerializeField] private string UniqueId;
        
        [SerializeField]
        [RequireInterface(typeof(IObjective))]
        private List<Object> _objectives = new List<Object>();
        public IEnumerable<IObjective> Objectives => _objectives.OfType<IObjective>();

        private void Awake()
        {
            if(string.IsNullOrEmpty(UniqueId))
                GenerateUniqueId();

            if (_objectives == null) return;
            
            SetObjectivesContext();
        }

        private void Update()
        {
            SetObjectivesContext();
        }

        private void SetObjectivesContext()
        {
            foreach (var objective in Objectives)
            {
                objective.SetContext(this);
            }
        }

        [ContextMenu("JStudios/Generate unique id")]
        public void GenerateUniqueId()
        {
            UniqueId = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Set the GameObject as context for a given objective
        /// </summary>
        /// <param name="objective">The objective that progresses when using this as context</param>
        public void SetAsContextFor(IObjective objective)
        {
            _objectives ??= new List<Object>();
            _objectives.Add(objective as Object);
            objective.SetContext(this);
        }
        
        public string GetUniqueId()
        {
            return UniqueId;
        }
    }
}