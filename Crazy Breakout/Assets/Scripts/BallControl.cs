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

    /// <summary>
    /// The rigid body component of this ball.  Used to control the ball's speed.
    /// </summary>
    private Rigidbody2D ballRigidBody = null;

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
        //  Make note of the time this ball was spawned, because balls have a limited lifespan.
        startTime = Time.realtimeSinceStartup;
        ballRigidBody = GetComponent<Rigidbody2D>();
        _ = StartCoroutine(GetTheBallRolling());
    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    void Update()
    {
        //  Destroy this ball if it is too old or has left the screen boundary.
        if (Time.realtimeSinceStartup - startTime > 20 || transform.position.y < ScreenUtils.ScreenBottom)
        {
            //  But let any listeners know first!
            OnBallDestroy();
            Destroy(gameObject);
        }
    }

    #endregion
    //---G
    #region Methods

    /// <summary>
    /// Gets the ball rolling.
    /// </summary>
    /// <returns>Yield to the caller so we are not needlessly burning cycles.</returns>
    private IEnumerator GetTheBallRolling()
    {
        //  But wait for one second before pushing it.
        yield return new WaitForSeconds(1);
        ballRigidBody.AddForce(new(ConfigurationUtils.BallSpeedFactor, ConfigurationUtils.BallSpeedFactor), ForceMode2D.Impulse);
    }

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
