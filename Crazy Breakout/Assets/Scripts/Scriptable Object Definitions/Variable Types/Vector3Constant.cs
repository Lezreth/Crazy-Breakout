using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Create a new Vector3Constant.
/// </summary>
[CreateAssetMenu(fileName = "New Vector3Constant", menuName = "Scriptable Variables/Constant Vector3", order = 51)]
public class Vector3Constant : ScriptableObject
{
    [SerializeField] private Vector3 _value;

    public Vector3 Value => _value;
}
