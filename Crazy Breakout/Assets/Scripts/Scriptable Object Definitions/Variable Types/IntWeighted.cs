using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Create a new IntWeighted variable that supports resetting its value to a specified default.  The variable's weight is used in skewing random selection.
/// </summary>
[CreateAssetMenu(fileName = "New IntWeighted", menuName = "Scriptable Variables/Int Weighted Variable", order = 51)]
public class IntWeighted : ScriptableObject, IWeighted
{
    [SerializeField] private int _weight;

    /// <summary>
    /// The weight of this variable.
    /// </summary>
    public int Weight => _weight;
}
