using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Processes user input for controlling the paddle.
/// </summary>
public class PaddleControl : MonoBehaviour
{
    #region Fields

    /// <summary>
    /// Multiplier for regulating the speed of the paddle.
    /// </summary>
    [SerializeField] private FloatConstant paddleSpeedFactor;

    /// <summary>
    /// The width of the paddle.
    /// </summary>
    private float paddleWidth;

    /// <summary>
    /// Half the width of the paddle.
    /// </summary>
    private float paddleHalfWidth;

    /// <summary>
    /// Flag for indicating if the paddle is frozen.
    /// </summary>
    private bool paddleFrozen = false;

    /// <summary>
    /// Time the paddle was frozen.
    /// </summary>
    private float frozenTime = 0.0f;

    /// <summary>
    /// The material component for changing the paddle's color.
    /// </summary>
    private Material paddleMaterial = null;

    #endregion
    #region Constants

    /// <summary>
    /// Half of the bounce angle range.
    /// </summary>
    private const float BounceAngleHalfRange = 60 * Mathf.Deg2Rad;

    #endregion

    /// <summary>
    /// Start is called before the first frame update.
    /// </summary>
    private void Start()
    {
        paddleMaterial = GetComponent<SpriteRenderer>().material;
        paddleWidth = GetComponent<PolygonCollider2D>().bounds.size.x;
        paddleHalfWidth = paddleWidth / 2;
    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    private void Update()
    {
        //  Move the paddle left/right according to the player's input.
        float input = Input.GetAxis("Horizontal");
        if (input != 0 && !paddleFrozen)
        {
            Vector3 newPosition = new(transform.position.x + (input * Time.deltaTime * paddleSpeedFactor.Value), transform.position.y, transform.position.z);

            if (newPosition.x > ScreenUtils.ScreenLeft + (paddleWidth / 2) && newPosition.x < ScreenUtils.ScreenRight - (paddleWidth / 2))
            {
                transform.position = newPosition;
            }
        }

        //  Thaw the paddle after its duration has elapsed.
        if (paddleFrozen && Time.realtimeSinceStartup - frozenTime > 2)
        {
            paddleFrozen = false;
            paddleMaterial.color = new(1, 1, 1);
        }
    }

    /// <summary>
    /// Called when a ball hits the paddle.
    /// </summary>
    /// <param name="collision">Collider of the ball that hit the paddle.</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            //  The horizontal position of the paddle and ball.
            float paddleXPosition = gameObject.transform.position.x;
            float ballXPosition = collision.transform.position.x;

            //  Subtract the X positions and divide result by half the width of the paddle to normalize the distance the ball is from the center of the paddle.
            float distanceFromCenter = paddleXPosition - ballXPosition;
            float normalizedBallOffset = distanceFromCenter / paddleHalfWidth;

            //  Calculate the new bounce angle, scaled to the distance the ball is from the center of the paddle.
            float angleOffset = normalizedBallOffset * BounceAngleHalfRange;
            float bounceAngle = (Mathf.PI / 2) + angleOffset;
            Vector2 bounceDirection = new(Mathf.Cos(bounceAngle), Mathf.Sin(bounceAngle));

            //  Tell the ball to change direction.
            collision.gameObject.GetComponent<BallControl>().SetDirection(bounceDirection);
        }
    }

    /// <summary>
    /// Freeze the paddle.
    /// </summary>
    public void FreezePaddle()
    {
        frozenTime = Time.realtimeSinceStartup;
        paddleFrozen = true;
        paddleMaterial.color = new(0, 0, 0);
    }
}
