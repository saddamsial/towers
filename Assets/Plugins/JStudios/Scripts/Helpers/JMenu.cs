// Written By Asaf Benjaminov @ JStudios 2022

using UnityEditor;

namespace JStudios.Scripts.Helpers
{
    /// <summary>
    /// Menu for JStudios assets
    /// </summary>
    public static class JMenu
    {
        private const string WriteDebugLOG = "Tools/JStudios/Logs/Write Debug log";
        private const string WriteFileLOG = "Tools/JStudios/Logs/Write File log";

        static JMenu()
        {
            Menu.SetChecked(WriteDebugLOG, JStudiosLog.WriteDebugLog);
            Menu.SetChecked(WriteFileLOG, JStudiosLog.WriteFileLog);  
        }
        
        [MenuItem(WriteDebugLOG)]
        private static void ToggleWriteDebugLog()
        {
            JStudiosLog.WriteDebugLog = !JStudiosLog.WriteDebugLog;
            
            Menu.SetChecked(WriteDebugLOG, JStudiosLog.WriteDebugLog);   
        }
        
        [MenuItem(WriteFileLOG)]
        private static void ToggleWriteFileLog()
        {
            JStudiosLog.WriteFileLog = !JStudiosLog.WriteFileLog;
            
            Menu.SetChecked(WriteFileLOG, JStudiosLog.WriteFileLog);   
        }
    }
}