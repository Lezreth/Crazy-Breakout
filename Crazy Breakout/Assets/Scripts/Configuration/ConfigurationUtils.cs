using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigurationUtils : MonoBehaviour
{
    #region Scriptable Objects

    /// <summary>
    /// List of the brick types that can be spawned.
    /// </summary>
    [SerializeField] private Dictionary<string, IntVariable> BrickPointValues;

    #endregion
    #region Properties

    #endregion
    //---
    #region Settings

    #endregion
    //---G

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        try
        {

        }
        catch (System.Exception)
        {
        }
    }


    /// <summary>
    /// Start is called before the first frame update.
    /// </summary>
    private void Start()
    {
    }
}
