using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Primary game controller.
/// </summary>
public class GameLoop : MonoBehaviour
{
    #region Game Objects

    /// <summary>
    /// Paddle prefab.
    /// </summary>
    [SerializeField] private GameObject prefabPaddle;

    #endregion
    //---
    #region Events

    /// <summary>
    /// Event to raise when starting a new game/level.
    /// </summary>
    [SerializeField] private GameEvent OnNewGame;

    /// <summary>
    /// Event to raise when the game is over.
    /// </summary>
    [SerializeField] private GameEvent OnEndGame;

    #endregion
    //---
    #region Scriptable Variables

    /// <summary>
    /// Number of balls in play.
    /// </summary>
    [SerializeField] private ResettableIntVariable ballCount;

    /// <summary>
    /// Flag for indicating if the game is running.
    /// </summary>
    [SerializeField] private BoolVariable gameRunning;

    /// <summary>
    /// Tag for identifying the paddle.
    /// </summary>
    [SerializeField] private StringConstant paddleTag;

    /// <summary>
    /// Multiplier for altering the ball speed when the speedup brick is destroyed.
    /// </summary>
    [SerializeField] private FloatChoice ballSpeedMultiplier;

    /// <summary>
    /// How long the effects of special bricks should last.
    /// </summary>
    [SerializeField] private FloatConstant specialBrickEffectDuration;

    #endregion
    //---
    #region Fields

    /// <summary>
    /// Time since the game start when the balls were last sped up.
    /// </summary>
    private float timeSinceBallSpeedup = 0.0f;

    #endregion
    //***G

    /// <summary>
    /// Awake is called before Start.  Used to initialize the screen utilities.
    /// </summary>
    private void Awake()
    {
        ScreenUtils.Initialize();
        gameRunning.Value = false;
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
        //  TODO  Bricks have various effects that happen when they are destroyed
        //  TODO  Positions of bricks with effects and bricks without effects are randomized
        //  TODO  Implement a configuration file for saving hard-coded values
        //  TODO  Add a brick counter to keep track of the number of bricks on the screen and end the game when they are all gone
        //  TODO  Create a level generation system

        if (Input.GetKeyDown(KeyCode.Space) && !gameRunning.Value)
        {
            gameRunning.Value = true;
            OnNewGame.Raise();
            SpawnPaddle();
        }

        //  The rest of the update needs to run only if a game is in session.
        if (!gameRunning.Value) { return; }

        //  Ball speedup effect lasts only a short while.
        if (ballSpeedMultiplier.IsTrue && Time.realtimeSinceStartup - timeSinceBallSpeedup > specialBrickEffectDuration.Value)
        {
            ballSpeedMultiplier.IsTrue = false;
        }
    }

    //---G

    /// <summary>
    /// Spawn the paddle.
    /// </summary>
    private void SpawnPaddle()
    {
        _ = Instantiate(prefabPaddle, new Vector3(0, ScreenUtils.ScreenBottom + ScreenUtils.ScreenSectionHeight, 0), Quaternion.identity);
    }

    /// <summary>
    /// Called when a ball is destroyed.  Ends the game if there are no more balls in the field.
    /// </summary>
    public void OnBallDestroyed()
    {
        if (ballCount.Value == 0)
        {
            EndGame();
            OnEndGame.Raise();
        }
    }

    /// <summary>
    /// Ends the current game and returns to the menu screen.
    /// </summary>
    private void EndGame()
    {
        gameRunning.Value = false;



        //  Find and destroy the paddle.
        GameObject paddle = GameObject.FindGameObjectWithTag(paddleTag.Value);
        if (paddle != null)
        {
            Destroy(paddle);
        }
    }

    /// <summary>
    /// Changes the value of the ball when the speedup brick is destroyed.
    /// </summary>
    public void OnSpeedupBrickDestroyed()
    {
        timeSinceBallSpeedup = Time.realtimeSinceStartup;
        ballSpeedMultiplier.IsTrue = true;
    }
}
