using System.Collections;
using System.Collections.Generic;
using GameStates;
using UnityEngine;
using UnityEngine.Events;

public class AnimationEvents : MonoBehaviour
{
    public List<UnityEvent> eventsToTrigger = new();

    public void InvokeEvents()
    {

        if (GameStateManager.Instance && GameStateManager.Instance.IsMenuState())
        {
            eventsToTrigger[0]?.Invoke();
        }
        else if (GameStateManager.Instance && GameStateManager.Instance.IsEditState())
        {
            eventsToTrigger[1]?.Invoke();
        }
    }
}
