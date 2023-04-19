using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Create a new StringConstant.
/// </summary>
[CreateAssetMenu(fileName = "New StringConstant", menuName = "Scriptable Variables/Constant String", order = 51)]
public class StringConstant : ScriptableObject
{
    [SerializeField]private string _value;

    public string Value => _value;
}
