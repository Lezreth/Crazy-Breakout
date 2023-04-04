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
    [SerializeField] private float PaddleSpeedFactor = DefaultPaddleSpeedFactor;

    /// <summary>
    /// The width of the paddle.
    /// </summary>
    private float paddleWidth;

    /// <summary>
    /// Half the width of the paddle.
    /// </summary>
    private float paddleHalfWidth;

    #endregion
    #region Constants

    /// <summary>
    /// The default paddle speed factor.
    /// </summary>
    private const float DefaultPaddleSpeedFactor = 7.0f;

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
        paddleWidth = GetComponent<PolygonCollider2D>().bounds.size.x;
        paddleHalfWidth = paddleWidth / 2;
    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    private void Update()
    {
        float input = Input.GetAxis("Horizontal");
        if (input != 0)
        {
            Vector3 newPosition = new(transform.position.x + (input * Time.deltaTime * PaddleSpeedFactor), transform.position.y, transform.position.z);

            if (newPosition.x > ScreenUtils.ScreenLeft + (paddleWidth / 2) && newPosition.x < ScreenUtils.ScreenRight - (paddleWidth / 2))
            {
                transform.position = newPosition;
            }
        }
    }

    /// <summary>
    /// Called when a ball hits the paddle.
    /// </summary>
    /// <param name="collision">Collider of the ball that hit the paddle.</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            //collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;

            float paddleXPosition = gameObject.transform.position.x;
            float ballXPosition = collision.transform.position.x;

            float distanceFromCenter = paddleXPosition - ballXPosition;
            float normalizedBallOffset = distanceFromCenter / paddleHalfWidth;

            float angleOffset = normalizedBallOffset * BounceAngleHalfRange;
            float bounceAngle = (Mathf.PI / 2) + angleOffset;
            Vector2 bounceDirection = new(Mathf.Cos(bounceAngle), Mathf.Sin(bounceAngle));

            collision.gameObject.GetComponent<BallControl>().SetDirection(bounceDirection);
        }
    }
}
