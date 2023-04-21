using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Raises events.  Listeners can subscribe to this event to be notified that something happened.
/// </summary>
[CreateAssetMenu(fileName = "New Game Event", menuName = "Game Event", order = 52)]
public class GameEvent : ScriptableObject
{
    /// <summary>
    /// The list of GameEventListeners that will subscribe to this GameEvent.
    /// </summary>
    private List<GameEventListener> listeners = new();

    /// <summary>
    /// Invokes all of the subscribers to this GameEvent.
    /// The last GameEventListener to subscribe will be the first to get invoked (last in, first out).
    /// </summary>
    public void Raise()
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnEventRaised();
        }
    }

    /// <summary>
    /// Allows GameEventListeners to subscribe to this GameEvent.
    /// </summary>
    /// <param name="listener">The listener to subscribe.</param>
    public void RegisterListener(GameEventListener listener)
    {
        listeners.Add(listener);
    }

    /// <summary>
    /// Allows GameEventListeners to unsubscribe from this GameEvent.
    /// </summary>
    /// <param name="listener">The listener to unsubscribe.</param>
    public void UnregisterListener(GameEventListener listener)
    {
        listeners.Remove(listener);
    }
}
