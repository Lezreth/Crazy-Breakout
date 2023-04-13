using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Subscribes to GameEvents to respond to events raised.
/// </summary>
public class GameEventListener : MonoBehaviour
{
    /// <summary>
    /// The GameEvent this listener will subscribe to.
    /// </summary>
    [SerializeField] private GameEvent gameEvent;

    /// <summary>
    /// The UnityEvent response that will be invoked when the GameEvent raises this listener.
    /// </summary>
    [SerializeField] private UnityEvent response;

    /// <summary>
    /// Register the listener to the GameEvent when this GameObject is enabled.
    /// </summary>
    private void OnEnable()
    {
        gameEvent.RegisterListener(this);
    }

    /// <summary>
    /// Unregister the listener from the GameEvent when this GameObject is disabled.
    /// </summary>
    private void OnDisable()
    {
        gameEvent.UnregisterListener(this);
    }

    /// <summary>
    /// Called when a GameEvent is raised, causing the listener to invoke the UnityEvent.
    /// </summary>
    public void OnEventRaised()
    {
        response.Invoke();
    }
}
