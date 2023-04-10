using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    /// Game score display.
    /// </summary>
    [SerializeField] private TextMeshProUGUI ScoreUI;

    #endregion
    //---
    #region Fields

    /// <summary>
    /// Flag for indicating if the game is running.
    /// </summary>
    private bool gameRunning = false;

    /// <summary>
    /// Keeps track of the number of balls on the field.
    /// </summary>
    private int ballCount = 0;

    /// <summary>
    /// The last time a ball was spawned.
    /// </summary>
    private float lastBallSpawnTime = 0.0f;

    /// <summary>
    /// The amount of time in seconds to wait before spawning another ball.
    /// </summary>
    private float nextBallSpawnTime = 0.0f;

    /// <summary>
    /// The brick spawner.
    /// </summary>
    private LevelBuilder brickSpawner;

    #endregion
    //---
    #region Constants

    #endregion
    //---G

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
        brickSpawner = GetComponent<LevelBuilder>();
        brickSpawner.SpawnBricks();
    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    private void Update()
    {
        //  TODO  Bricks have various effects that happen when they are destroyed
        //  TODO  Positions of bricks with effects and bricks without effects are randomized
        //  TODO  Implement a configuration file for saving hard-coded values
        //  TODO  HUD: Track and display the number of balls the player has left
        //  TODO  HUD: Track and display the player's score

        if (gameRunning)
        { _ = StartCoroutine(SpawnNewBall()); }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            gameRunning = true;
            SpawnPaddle();
        }
    }

    /// <summary>
    /// Late Update is called after all Update routines have finished.
    /// </summary>
    private void LateUpdate()
    {
        ScoreUI.text = "Balls in play: " + ballCount;
    }

    /// <summary>
    /// Create a new ball.  New balls wait one second before moving.
    /// </summary>
    private IEnumerator SpawnNewBall()
    {
        //  Wait until the designated time to spawn a new ball.
        if (Time.time - lastBallSpawnTime < nextBallSpawnTime) { yield break; }

        //  Capture the spawn time and set the amount of time to wait before spawning the next ball.
        lastBallSpawnTime = Time.time;
        nextBallSpawnTime = Random.Range(5, 10);

        //  Spawn the ball below the brick field, but above the paddle location.
        GameObject newBall = Instantiate(prefabBall, new Vector3(0, ScreenUtils.ScreenBottom + (ScreenUtils.ScreenSectionHeight * 3), 0), Quaternion.identity);
        newBall.GetComponent<BallControl>().OnBallDestroy += BallDestroyed;
        ballCount++;
    }

    /// <summary>
    /// Spawn the paddle.
    /// </summary>
    private void SpawnPaddle()
    {
        _ = Instantiate(prefabPaddle, new Vector3(0, ScreenUtils.ScreenBottom + ScreenUtils.ScreenSectionHeight, 0), Quaternion.identity);
    }

    /// <summary>
    /// Subtracts one from the ball counter when a ball is destroyed.
    /// </summary>
    private void BallDestroyed()
    {
        ballCount--;
        ScoreUI.text = "Balls in play: " + ballCount;

        if (ballCount == 0)
        {
            EndGame();
        }
    }

    /// <summary>
    /// Ends the current game and returns to the menu screen.
    /// </summary>
    private void EndGame()
    {
        gameRunning = false;

        //  Find and destroy any bricks that remain on the playing field.
        GameObject[] bricks = GameObject.FindGameObjectsWithTag(ConfigurationUtils.BrickTag);
        if (bricks.Length > 0)
        {
            for (int i = bricks.Length - 1; i >= 0; i--)
            {
                Destroy(bricks[i]);
            }
        }

        //  Find and destroy any balls that remain on the playing field.
        GameObject[] balls = GameObject.FindGameObjectsWithTag(ConfigurationUtils.BallTag);
        if (balls.Length > 0)
        {
            for (int i = balls.Length - 1; i >= 0; i--)
            {
                Destroy(balls[i]);
            }
        }

        //  Find and destroy the paddle.
        GameObject paddle = GameObject.FindGameObjectWithTag(ConfigurationUtils.PaddleTag);
        if (paddle != null)
        {
            Destroy(paddle);
        }
    }
}
