using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Create a new FloatChoice.  A constant who's value can be toggled between two options.
/// </summary>
[CreateAssetMenu(fileName = "New FloatChoice", menuName = "Scriptable Variables/Bool Controlled Float", order = 51)]
public class FloatChoice : ScriptableObject
{
    /// <summary>
    /// Value if the bool is true.
    /// </summary>
    [SerializeField] private float trueValue = 0;

    /// <summary>
    /// Value if the bool is false.
    /// </summary>
    [SerializeField] private float falseValue = 0;

    /// <summary>
    /// Chooser flag.  True = Value is TrueValue.  False = Value is FalseValue.
    /// </summary>
    public bool IsTrue = false;

    /// <summary>
    /// True = Value is TrueValue.  False = Value is FalseValue.
    /// </summary>
    public float Value => IsTrue ? trueValue : falseValue;
}
