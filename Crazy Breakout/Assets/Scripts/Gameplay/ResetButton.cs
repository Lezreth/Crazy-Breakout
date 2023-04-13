using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetButton : MonoBehaviour
{
    #region Scriptable Variables

    /// <summary>
    /// The number of balls in play.
    /// </summary>
    [SerializeField] private IntVariable ballsInPlay;

    /// <summary>
    /// The player's current score.
    /// </summary>
    [SerializeField] private IntVariable score;

    /// <summary>
    /// Flag for indicating if the game is running.
    /// </summary>
    [SerializeField] private BoolVariable gameRunning;

    #endregion
    //***G

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        ResetGame();
    }

    /// <summary>
    /// Resets the game.
    /// </summary>
    public void ResetGame()
    {
        ballsInPlay.ResetValue();
        score.ResetValue();
        gameRunning.ResetValue();
    }
}
