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
    private float BoundsX;

    #endregion
    #region Constants

    /// <summary>
    /// The default paddle speed factor.
    /// </summary>
    private const float DefaultPaddleSpeedFactor = 7.0f;

    #endregion

    /// <summary>
    /// Start is called before the first frame update.
    /// </summary>
    private void Start()
    {
        BoundsX = GetComponent<SpriteRenderer>().bounds.size.x;
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

            if (newPosition.x > ScreenUtils.ScreenLeft + (BoundsX / 2) && newPosition.x < ScreenUtils.ScreenRight - (BoundsX / 2))
            {
                transform.position = newPosition;
            }
        }
    }

    /// <summary>
    /// Called when a ball hits the paddle.
    /// </summary>
    /// <param name="collision">Collider of the ball that hit the paddle.</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        float paddleXPosition = gameObject.transform.position.x;
        float ballXPosition = collision.transform.position.x;
    }
}
