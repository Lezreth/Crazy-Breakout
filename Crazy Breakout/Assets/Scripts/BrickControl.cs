using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickControl : MonoBehaviour
{
    #region Fields

    #endregion

    /// <summary>
    /// Awake is called before Start.
    /// </summary>
    private void Awake()
    {
    }

    /// <summary>
    /// Start is called before the first frame update.
    /// </summary>
    private void Start()
    {
    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    private void Update()
    {
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Destroy(gameObject);
        }
    }
}
