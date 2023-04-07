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
    /// Brick prefab.
    /// </summary>
    [SerializeField] private GameObject prefabBrick;

    /// <summary>
    /// Multiplier for regulating the speed of the balls.
    /// </summary>
    [SerializeField] private float BallSpeedFactor = DefaultBallSpeedFactor;

    /// <summary>
    /// Game score display.
    /// </summary>
    [SerializeField] private TextMeshProUGUI ScoreUI;

    #endregion
    //---
    #region Properties

    /// <summary>
    /// Divides the screen into eight sections for determining the spawn locations for the paddle and balls.
    /// </summary>
    private float ScreenSectionHeight { get { return (Mathf.Abs(ScreenUtils.ScreenTop) + Mathf.Abs(ScreenUtils.ScreenBottom)) / 8; } }

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

    #endregion
    //---
    #region Constants

    /// <summary>
    /// The default ball speed factor.
    /// </summary>
    private const float DefaultBallSpeedFactor = -3.0f;

    /// <summary>
    /// Padding between bricks so they are not jammed up against each other.
    /// </summary>
    private const float BrickBufferSpace = 0.1f;

    /// <summary>
    /// Tag identifier for the balls.
    /// </summary>
    private const string BallTag = "Ball";

    /// <summary>
    /// Tag identifier for the bricks.
    /// </summary>
    private const string BrickTag = "Brick";

    /// <summary>
    /// Tag identifier for the paddle.
    /// </summary>
    private const string PaddleTag = "Paddle";

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
        SpawnBricks();
        SpawnPaddle();
    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    private void Update()
    {
        //  TODO  Bricks have various effects that happen when they are destroyed
        //  TODO  Positions of bricks with effects and bricks without effects are randomized

        if (gameRunning)
        { _ = StartCoroutine(SpawnNewBall()); }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            gameRunning = true;
        }
    }

    /// <summary>
    /// Spawn the bricks on the brick field.  The brick field is the upper half of the screen.
    /// </summary>
    private void SpawnBricks()
    {
        //  Calculate the available space for the bricks.
        float horizontalSpace = Mathf.Abs(ScreenUtils.ScreenLeft) + Mathf.Abs(ScreenUtils.ScreenRight) - BrickBufferSpace;
        float verticalSpace = (Mathf.Abs(ScreenUtils.ScreenTop) + Mathf.Abs(ScreenUtils.ScreenBottom)) / 2;

        //  Create a sample brick and extract it's dimensions.
        GameObject testBrick = Instantiate(prefabBrick, new Vector3(ScreenUtils.ScreenRight + 5, ScreenUtils.ScreenBottom + 5, 0), Quaternion.identity);
        Vector3 brickSize = testBrick.GetComponent<BoxCollider2D>().bounds.size;
        Destroy(testBrick);

        //  Add the buffer to the brick size so they are not jammed up against each other.
        brickSize.x += BrickBufferSpace;
        brickSize.y += BrickBufferSpace;

        //  Calculate the maximum number of bricks that will fit in the brick field.
        int maxHorizontalBricks = Mathf.FloorToInt(horizontalSpace / brickSize.x);
        int maxVerticalBricks = Mathf.FloorToInt(verticalSpace / brickSize.y);

        //  Spawn and position the bricks on the brick field.
        for (int i = 0; i < maxHorizontalBricks - 1; i++)
        {
            for (int j = 0; j < maxVerticalBricks; j++)
            {
                if (Random.value < 0.2f)
                {
                    _ = Instantiate(prefabBrick, new(ScreenUtils.ScreenLeft + ((i + 1) * brickSize.x), ScreenUtils.ScreenTop - ((j + 1) * brickSize.y), 0), Quaternion.identity);
                }
            }
        }
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
        GameObject newBall = Instantiate(prefabBall, new Vector3(0, ScreenUtils.ScreenBottom + (ScreenSectionHeight * 3), 0), Quaternion.identity);
        newBall.GetComponent<BallControl>().OnBallDestroy += BallDestroyed;
        ballCount++;
        ScoreUI.text = "Balls in play: " + ballCount;

        //  Delay one second, then move the ball.
        yield return new WaitForSeconds(1);
        newBall.GetComponent<Rigidbody2D>().AddForce(new(BallSpeedFactor, -BallSpeedFactor), ForceMode2D.Impulse);
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
    /// Spawn the paddle.
    /// </summary>
    private void SpawnPaddle()
    {
        _ = Instantiate(prefabPaddle, new Vector3(0, ScreenUtils.ScreenBottom + ScreenSectionHeight, 0), Quaternion.identity);
    }

    /// <summary>
    /// Ends the current game and returns to the menu screen.
    /// </summary>
    private void EndGame()
    {
        gameRunning = false;

        //  Find and destroy any bricks that remain on the playing field.
        GameObject[] bricks = GameObject.FindGameObjectsWithTag(BrickTag);
        if (bricks.Length > 0)
        {
            for (int i = bricks.Length - 1; i >= 0; i--)
            {
                Destroy(bricks[i]);
            }
        }

        //  Find and destroy any balls that remain on the playing field.
        GameObject[] balls = GameObject.FindGameObjectsWithTag(BallTag);
        if (balls.Length > 0)
        {
            for (int i = balls.Length - 1; i >= 0; i--)
            {
                Destroy(balls[i]);
            }
        }

        //  Find and destroy the paddle.
        GameObject paddle = GameObject.FindGameObjectWithTag(PaddleTag);
        if (paddle != null)
        {
            Destroy(paddle);
        }
    }
}
