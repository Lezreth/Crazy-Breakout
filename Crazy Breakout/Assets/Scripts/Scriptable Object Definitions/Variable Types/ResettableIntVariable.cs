using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Create a new ResettableIntVariable that supports resetting its value to a specified default.
/// </summary>
[CreateAssetMenu(fileName = "New ResettableIntVariable", menuName = "Scriptable Variables/Resettable Int Variable", order = 51)]
public class ResettableIntVariable : ScriptableObject
{
    /// <summary>
    /// The default value of this variable.
    /// </summary>
    [SerializeField] private int DefaultValue = 0;

    /// <summary>
    /// The value of this variable.
    /// </summary>
    [HideInInspector] public int Value;

    /// <summary>
    /// Resets the value to the default value.
    /// </summary>
    public void ResetValue()
    {
        Value = DefaultValue;
    }
}
