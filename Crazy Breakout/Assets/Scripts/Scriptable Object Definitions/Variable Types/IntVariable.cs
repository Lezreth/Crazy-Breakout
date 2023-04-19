using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Create a new IntVariable.
/// </summary>
[CreateAssetMenu(fileName = "New IntVariable", menuName = "Scriptable Variables/Int Variable", order = 51)]
public class IntVariable : ScriptableObject
{
    public int Value;
}
