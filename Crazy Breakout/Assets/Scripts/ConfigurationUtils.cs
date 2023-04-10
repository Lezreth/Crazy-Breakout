using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ConfigurationUtils
{
    #region Properties

    #endregion
    //---
    #region Constants

    /// <summary>
    /// Padding between bricks so they are not jammed up against each other.
    /// </summary>
    public const float BrickBufferSpace = 0.1f;

    /// <summary>
    /// Tag identifier for the balls.
    /// </summary>
    public const string BallTag = "Ball";

    /// <summary>
    /// Tag identifier for the bricks.
    /// </summary>
    public const string BrickTag = "Brick";

    /// <summary>
    /// Tag identifier for the paddle.
    /// </summary>
    public const string PaddleTag = "Paddle";

    /// <summary>
    /// The default ball speed factor.
    /// </summary>
    public const float BallSpeedFactor = -3.0f;

    #endregion
    //---G

    /// <summary>
    /// Start is called before the first frame update.
    /// </summary>
    private static void Start()
    {
    }
}
