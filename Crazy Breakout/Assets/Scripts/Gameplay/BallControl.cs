using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class BallControl : MonoBehaviour
{
    #region Scripted Variables

    /// <summary>
    /// The number of balls on the playing field.
    /// </summary>
    [SerializeField] private ResettableIntVariable ballCount;

    /// <summary>
    /// Multiplier for applying force to the ball.
    /// </summary>
    [SerializeField] private FloatConstant ballSpeedFactor;

    /// <summary>
    /// Direction to start moving in.
    /// </summary>
    [SerializeField] private Vector3Constant ballStartingVector;

    #endregion
    //---
    #region Fields

    /// <summary>
    /// The time this ball was created.
    /// </summary>
    private float startTime = 0.0f;

    /// <summary>
    /// The rigid body component of this ball.  Used to control the ball's speed.
    /// </summary>
    private Rigidbody2D ballRigidBody = null;

    /// <summary>
    /// The material component of this ball.  Used to manipulate the color over this ball's lifetime.
    /// </summary>
    private Material ballMaterial = null;

    #endregion
    //---
    #region Events

    /// <summary>
    /// Called when a ball is about to be destroyed because it expired or it left the playing field.
    /// </summary>
    [SerializeField] private GameEvent OnBallDestroy;


    #endregion
    //***G
    #region Unity Methods

    /// <summary>
    /// Start is called before the first frame update.
    /// </summary>
    void Start()
    {
        //  Get the sprite renderer's material so we can manipulate the color.
        ballMaterial = GetComponent<SpriteRenderer>().material;

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
        float colorFactor = (20.0f - (Time.realtimeSinceStartup - startTime)) / 20;
        ballMaterial.color = new(colorFactor, colorFactor, colorFactor);

        //  Destroy this ball if it is too old or has left the screen boundary.
        if (Time.realtimeSinceStartup - startTime > 20 || transform.position.y < ScreenUtils.ScreenBottom)
        {
            //  But decrement the ball count and let any listeners know first!
            ballCount.Value--;
            OnBallDestroy.Raise();
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
        ballRigidBody.AddForce(ballStartingVector.Value * ballSpeedFactor.Value, ForceMode2D.Impulse);
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
