using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Primary game controller.
/// </summary>
public class GameLoop : MonoBehaviour
{
    #region Serialized Fields

    /// <summary>
    /// Paddle prefab.
    /// </summary>
    [SerializeField] private GameObject prefabPaddle;

    /// <summary>
    /// Ball prefab.
    /// </summary>
    [SerializeField] private GameObject prefabBall;

    /// <summary>
    /// Multiplier for regulating the speed of the balls.
    /// </summary>
    [SerializeField] private float BallSpeedFactor = DefaultBallSpeedFactor;

    #endregion
    //---
    #region Fields

    /// <summary>
    /// Paddle that the player controls.
    /// </summary>
    private GameObject paddle = null;

    #endregion
    //---
    #region Constants

    /// <summary>
    /// The default ball speed factor.
    /// </summary>
    private const float DefaultBallSpeedFactor = 3.0f;

    #endregion
    //---

    /// <summary>
    /// Awake is called before Start.  Used to initialize the screen utilities.
    /// </summary>
    private void Awake()
    {
        ScreenUtils.Initialize();
    }

    /// <summary>
    /// Start is called before the first frame update.
    /// </summary>
    private void Start()
    {

        //--------------------------------------------------------------------------------------------------------------------------------------
        paddle = Instantiate(prefabPaddle, new Vector3(0, ScreenUtils.ScreenBottom + 2, 0), Quaternion.identity);
        SpawnNewBall();
    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnNewBall();
        }
    }


    private void SpawnNewBall()
    {
        GameObject newBall = Instantiate(prefabBall, new Vector3(0, 1, 0), Quaternion.identity);
        newBall.GetComponent<Rigidbody2D>().AddForce(new(BallSpeedFactor, BallSpeedFactor), ForceMode2D.Impulse);
    }
}
