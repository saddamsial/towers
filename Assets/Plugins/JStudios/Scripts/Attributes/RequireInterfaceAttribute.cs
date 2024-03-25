// Written By Asaf Benjaminov @ JStudios 2022

using UnityEngine;

namespace JStudios.Scripts.Attributes
{
    /// <summary>
    /// Makes an object box to require an interface in the editor
    /// </summary>
    public class RequireInterfaceAttribute : PropertyAttribute
    {
        public System.Type RequiredType { get; private set; }
        public bool ShowInfo { get; set; }
        public RequireInterfaceAttribute(System.Type type, bool showInfo = false)
        {
            RequiredType = type;
            ShowInfo = showInfo;
        }
    }
}