// Written By Asaf Benjaminov @ JStudios 2022

using UnityEngine;

namespace JStudios.Scripts.Attributes
{
    /// <summary>
    /// Sets the order of the property in the editor
    /// </summary>
    public class OrderInEditor : PropertyAttribute
    {
        public int Order { get; set; }

        public OrderInEditor(int order)
        {
            Order = order;
        }
    }
}