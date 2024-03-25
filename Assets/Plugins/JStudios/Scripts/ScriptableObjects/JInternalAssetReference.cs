// Written By Asaf Benjaminov @ JStudios 2022

using JStudios.Scripts.Attributes;
using JStudios.Scripts.Helpers;
using UnityEngine;

namespace JStudios.Scripts.ScriptableObjects
{
    /// <summary>
    /// A base for a reference for a JAsset
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class JInternalAssetReference<T> : ScriptableObject where T : JAsset
    {
        [SerializeField] [ReadOnly] private T Reference;

        public T Ref
        {
            get
            {
                if (Reference != null) return Reference;
            
                Reference = JStudiosAssetsHelper.FindInternalAsset<T>();

                if (Reference == null)
                {
                    JStudiosAssetsHelper.OnInternalAssetMissing(typeof(T));
                }

                return Reference;
            }
        }
    }
}