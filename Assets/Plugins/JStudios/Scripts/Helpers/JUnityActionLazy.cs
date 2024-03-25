// Written By Asaf Benjaminov @ JStudios 2022

using UnityEngine.Events;

namespace JStudios.Scripts.Helpers
{
    /// <summary>
    /// Lazy action, invokes only when the action is ready
    /// </summary>
    public class JUnityActionLazy
    {
        private UnityAction _event;
        private bool _isReady;
        
        public void Subscribe(UnityAction callback)
        {
            _event += callback;
            
            if (_isReady)
            {
                callback?.Invoke();
            }
        }

        public void Unsubscribe(UnityAction callback)
        {
            _event -= callback;
        }

        public void Invoke()
        {
            _isReady = true;
            _event?.Invoke();
        }

        public void Reset()
        {
            _isReady = false;
        }
    }
}