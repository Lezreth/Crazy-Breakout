using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Create a new BoolVariable.
/// </summary>
[CreateAssetMenu(fileName = "New BoolVariable", menuName = "Scriptable Variables/Bool Variable", order = 51)]
public class BoolVariable : ScriptableObject
{
    /// <summary>
    /// The default value of this variable.
    /// </summary>
    [SerializeField] private bool DefaultValue = false;

    /// <summary>
    /// The value of this variable.
    /// </summary>
    [HideInInspector] public bool Value;

    /// <summary>
    /// Resets the value to the default value.
    /// </summary>
    public void ResetValue()
    {
        Value = DefaultValue;
    }
}
