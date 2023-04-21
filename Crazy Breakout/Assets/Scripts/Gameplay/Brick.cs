using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour, IWeighted
{
    #region Serialized Fields

    /// <summary>
    /// The player's score.
    /// </summary>
    [SerializeField] protected ResettableIntVariable score;

    /// <summary>
    /// The weight of this variable.
    /// </summary>
    [SerializeField] private IntWeighted weight;

    /// <summary>
    /// How much this brick is worth.
    /// </summary>
    [SerializeField] private IntConstant pointValue;

    #endregion
    //---
    #region Internal Fields

    /// <summary>
    /// The weight of this variable.
    /// </summary>
    public int Weight => weight.Weight;

    #endregion
    //---
    #region Events

    /// <summary>
    /// Event raised when a brick is destroyed.
    /// </summary>
    [SerializeField] private GameEvent[] OnBrickDestroy;

    #endregion
    //***G
    #region Methods

    /// <summary>
    /// Event that happens when something collides with this object.
    /// </summary>
    /// <param name="collision">The collider of the object that collided with this one.</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            //  Perform brick type-specific behavior.
            BrickDestroyed();
        }
    }

    /// <summary>
    /// Brick type-specific behavior to perform right before the brick is destroyed by being hit by a ball.
    /// </summary>
    protected void BrickDestroyed()
    {
        //  TODO  Implement brick destruction - Make effects.

        //  Add the value of this brick to the player's score.
        score.Value += pointValue.Value;

        //  Let any listeners know that this brick has been destroyed by a ball.
        foreach (GameEvent e in OnBrickDestroy)
        {
            e.Raise();
        }

        //  Destroy this brick.
        DestroyMe();
    }

    /// <summary>
    /// Destroy this brick.  Destruction methods are 1) Being hit by a ball; 2) Game ends.
    /// </summary>
    public void DestroyMe()
    {
        Destroy(gameObject);
    }

    #endregion
}
