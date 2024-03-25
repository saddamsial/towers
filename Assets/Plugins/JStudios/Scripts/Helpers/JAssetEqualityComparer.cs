// Written By Asaf Benjaminov @ JStudios 2022

using System;
using System.Collections.Generic;
using JStudios.Scripts.ScriptableObjects;

namespace JStudios.Scripts.Helpers
{
    /// <summary>
    /// Equality comparer for any JAsset
    /// </summary>
    /// <typeparam name="T">Type of the JAsset</typeparam>
    public class JAssetEqualityComparer<T> : IEqualityComparer<T> where T : JAsset
    {
        public bool Equals(T x, T y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            if (x.GetType() != y.GetType()) return false;
            return x.UniqueId == y.UniqueId && x.IsInternal == y.IsInternal;
        }

        public int GetHashCode(T obj)
        {
            return HashCode.Combine(obj.UniqueId, obj.IsInternal);
        }
    }
}