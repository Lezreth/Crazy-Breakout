using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a Weighted item.
/// </summary>
public interface IWeighted
{
    /// <summary>
    /// A positive weight.  It is up to the implementer to ensure this requirement.
    /// </summary>
    public int Weight { get; }
}
