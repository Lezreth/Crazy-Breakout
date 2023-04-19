using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Creates a new FloatVariable.
/// </summary>
[CreateAssetMenu(fileName = "New FloatVariable", menuName = "Scriptable Variables/Float Variable", order = 51)]
public class FloatVariable : ScriptableObject
{
    public float Value;
}
