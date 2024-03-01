using System.Collections.Generic;
using GameStates;
using Tower.Floor;
using UnityEngine;

namespace Utils
{
    public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;
        public static T Instance
        {
            get
            {
                if (_instance == null)
                    _instance = FindObjectOfType<T>();

                return _instance;
            }
        }
        protected virtual void OnDisable()
        {
            _instance = null;
        }


    }
}