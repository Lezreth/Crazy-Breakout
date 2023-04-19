using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Create a new IntConstant.
/// </summary>
[CreateAssetMenu(fileName = "New IntConstant", menuName = "Scriptable Variables/Constant Integer", order = 51)]
public class IntConstant : ScriptableObject
{
    [SerializeField] private int _value;

    public int Value => _value;
}
