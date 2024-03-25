// Written By Asaf Benjaminov @ JStudios 2022

using System;
using System.IO;
using JStudios.QuestSystem.Scripts.ScriptableObjects.Objectives;
using JStudios.QuestSystem.Scripts.ScriptableObjects.Quests;
using JStudios.Scripts.Attributes;
using JStudios.Scripts.Helpers;
using JStudios.Scripts.ScriptableObjects;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace JStudios.QuestSystem.Scripts.ScriptableObjects
{
    /// <summary>
    /// Core operations for the QuestSystem
    /// </summary>
    // [CreateAssetMenu(fileName = "Quest System Core", menuName = "JStudios/Quest System/Floaters/Quest Core", order = 96)]
    public class QuestSystemCore : ScriptableObject
    {
        private static UnityAction OnRefresh;
        [SerializeField] [ReadOnly] [ShowInlineInfo] public ObjectiveList ObjectiveList;
        [SerializeField] [ReadOnly] [ShowInlineInfo] public QuestList QuestList;
        /*[SerializeField]*/ [ReadOnly] [ShowInlineInfo] internal QuestSystemChannel Channel;
        [SerializeField] [ReadOnly] [ShowInlineInfo] public JSettings JSettings;
        
        /*[SerializeField]*/ [ReadOnly] [ShowInlineInfo] private DummyContext _dummyContext;
        
        internal static JLog Log;

        public static DummyContext GlobalContextInstance;

        public static void RefreshSystem()
        {
            OnRefresh.Invoke();
        } 
        
        public void Refresh()
        {
            Channel.Refresh();
        }

        private void OnDisable()
        {
            JModificationProcessor.OnJAssetsModified -= OnJAssetsModified;
            JModificationProcessor.OnJAssetsCreated -= OnJAssetsModified;
            JModificationProcessor.OnJAssetsDeleted -= OnJAssetsModified;
        }

        private void OnEnable()
        {
            OnRefresh -= Refresh;
            OnRefresh += Refresh;
            
            JModificationProcessor.OnJAssetsModified += OnJAssetsModified;
            JModificationProcessor.OnJAssetsCreated += OnJAssetsModified;
            JModificationProcessor.OnJAssetsDeleted += OnJAssetsModified;
            
            GlobalContextInstance = _dummyContext;
        }

        private void OnJAssetsModified(string path, Type type)
        {
            if (!type.IsSubclassOf(typeof(JAsset))) return;
            
            Fix($"{nameof(QuestSystemCore)}.{nameof(OnJAssetsModified)}");
            Log.Info($"Asset modified as {path}");
        }

        [ContextMenu("Fix package")]
        private void FixFromContextMenu()
        {
            Fix("Context Menu");
        }
        
        public void Fix(string from)
        {
            JQuestSystemEvents.OnFixCoreCompletedEvent.Reset();
            
            TryCreateQuestSystemSettings();
            TryCreateLogFile();
            TryCreateObjectiveList();
            TryCreateQuestList();
            TryCreateQuestSystemChannel();
            TryCreateDummyContext();
            
            Log.Info($"Fix done. (from {from})");
            
            AssetDatabase.SaveAssets();
            JQuestSystemEvents.OnFixCoreCompletedEvent.Invoke();
        }

        private void TryCreateLogFile()
        {
            Log = new JLog(JSettings.LogFilePath, "QuestSystem");
        }

        private void TryCreateDummyContext()
        {
            if (_dummyContext == null)
            {
                _dummyContext = JStudiosAssetsHelper.FindInternalAsset<DummyContext>();    
            }

            if (_dummyContext == null)
            {
                _dummyContext = JStudiosAssetsHelper.TryCreateInternalAsset<DummyContext>("Dummy Context");    
            }

            
        }
        
        private void TryCreateQuestSystemChannel()
        {
            if (Channel == null)
            {
                Channel = JStudiosAssetsHelper.FindInternalAsset<QuestSystemChannel>();    
            }

            if (Channel == null)
            {
                Channel = JStudiosAssetsHelper.TryCreateInternalAsset<QuestSystemChannel>("Quest System Channel");    
            }
            
            Channel.ObjectiveList = ObjectiveList;
            Channel.QuestList = QuestList;
            Channel.DummyContext = _dummyContext;
            
            Channel.Validate();
        }

        private void TryCreateQuestList()
        {
            if (QuestList == null)
            {
                QuestList = JStudiosAssetsHelper.FindInternalAsset<QuestList>();    
            }

            if (QuestList == null)
            {
                QuestList = JStudiosAssetsHelper.TryCreateInternalAsset<QuestList>("Quest List");    
            }
            
            QuestList.LoadAssets();
        }

        private void TryCreateObjectiveList()
        {
            if (ObjectiveList == null)
            {
                ObjectiveList = JStudiosAssetsHelper.FindInternalAsset<ObjectiveList>();    
            }

            if (ObjectiveList == null)
            {
                ObjectiveList = JStudiosAssetsHelper.TryCreateInternalAsset<ObjectiveList>("Objective List");    
            }
            
            ObjectiveList.LoadAssets();
        }

        private void TryCreateQuestSystemSettings()
        {
            if (JSettings == null)
            {
                JSettings = JStudiosAssetsHelper.FindInternalAsset<JSettings>();    
            }

            if (JSettings == null)
            {
                JSettings = JStudiosAssetsHelper.TryCreateInternalAsset<JSettings>("J Settings");    
            }

            if (string.IsNullOrEmpty(JSettings.LogFilePath))
            {
                var logFilePath = Path.Combine("Assets", "JStudios", "QuestSystem");
                JSettings.LogFilePath = logFilePath;    
            }
        }
    }
}