using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Creates a new StringVariable.
/// </summary>
[CreateAssetMenu(fileName = "New StringVariable", menuName = "Scriptable Variables/String Variable", order = 51)]
public class StringVariable : ScriptableObject
{
    public string Value;
}
