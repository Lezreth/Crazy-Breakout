using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New IntVariable", menuName = "Int Variable", order = 51)]
public class IntVariable : ScriptableObject
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
