using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class BallControl : MonoBehaviour
{
    #region Fields

    /// <summary>
    /// The time this ball was created.
    /// </summary>
    private float startTime = 0.0f;

    #endregion
    //---
    #region Delegates

    /// <summary>
    /// Delegate for notifying the control loop that this ball has been destroyed.
    /// </summary>
    public delegate void OnBallDestroyDelegate();

    /// <summary>
    /// Called when a ball is about to be destroyed because it expired or it left the playing field.
    /// </summary>
    public OnBallDestroyDelegate OnBallDestroy;
    #endregion
    //***G
    #region Unity Methods

    /// <summary>
    /// Start is called before the first frame update.
    /// </summary>
    void Start()
    {
        startTime = Time.realtimeSinceStartup;
    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    void Update()
    {
        if (Time.realtimeSinceStartup - startTime > 20 || transform.position.y < ScreenUtils.ScreenBottom)
        {
            OnBallDestroy();
            Destroy(gameObject);
        }
    }

    #endregion
    //---G
    #region Methods

    /// <summary>
    /// Sets the direction the ball should travel while maintaining its current speed.
    /// </summary>
    /// <param name="Direction">The new direction the ball should travel in.</param>
    public void SetDirection(Vector2 Direction)
    {
        Rigidbody2D rigidBody2D = GetComponent<Rigidbody2D>();
        float speed = rigidBody2D.velocity.magnitude;
        rigidBody2D.velocity = Direction * speed;
    }

    #endregion
}
