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

        //  TODO  Delay before spawning the ball.
        StartCoroutine(SpawnNewBall());
    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    private void Update()
    {
        //  TODO  Spawn balls periodically
        //  TODO  Remove bottom screen barrier
        //  TODO  Subtract one life when no more balls remain due to them leaving the bottom of the screen, game over on zero lives.
        //  TODO  Make balls last a certain amount of time, then despawn.  Lives are not subtracted from balls expiring.
        //  TODO  New balls are spawned randomly between 5 - 10 seconds
        //  TODO  Bricks have various effects that happen when they are destroyed
        //  TODO  Positions of bricks with effects and bricks without effects are randomized

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(SpawnNewBall());
        }
    }

    /// <summary>
    /// Spawn the bricks on the brick field.
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
                if (Random.value > 0.2f)
                {
                    _ = Instantiate(prefabBrick, new(ScreenUtils.ScreenLeft + ((i + 1) * brickSize.x), ScreenUtils.ScreenTop - ((j + 1) * brickSize.y), 0), Quaternion.identity);
                }
            }
        }
    }

    /// <summary>
    /// Create a new ball.
    /// </summary>
    private IEnumerator SpawnNewBall()
    {
        //  Spawn the ball below the brick field, but above the paddle location.
        GameObject newBall = Instantiate(prefabBall, new Vector3(0, ScreenUtils.ScreenBottom + (ScreenSectionHeight * 3), 0), Quaternion.identity);

        //  Delay one second, then move the ball.
        yield return new WaitForSeconds(1);
        newBall.GetComponent<Rigidbody2D>().AddForce(new(BallSpeedFactor, -BallSpeedFactor), ForceMode2D.Impulse);
    }

    /// <summary>
    /// Spawn the paddle.
    /// </summary>
    private void SpawnPaddle()
    {
        _ = Instantiate(prefabPaddle, new Vector3(0, ScreenUtils.ScreenBottom + ScreenSectionHeight, 0), Quaternion.identity);
    }
}
