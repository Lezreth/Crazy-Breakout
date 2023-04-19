using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Create a new FloatConstant.
/// </summary>
[CreateAssetMenu(fileName = "New FloatConstant", menuName = "Scriptable Variables/Constant Float", order = 51)]
public class FloatConstant : ScriptableObject
{
    [SerializeField]private float _value;

    public float Value => _value;
}
