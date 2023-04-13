using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    #region UI Fields

    /// <summary>
    /// Displays the player's current score.
    /// </summary>
    [SerializeField] private TextMeshProUGUI scoreText;

    /// <summary>
    /// Displays the player's highest score reached.
    /// </summary>
    [SerializeField] private TextMeshProUGUI highScoreText;

    /// <summary>
    /// Displays the number of balls in play.
    /// </summary>
    [SerializeField] private TextMeshProUGUI ballsInPlay;

    #endregion
    //---G

    /// <summary>
    /// Updates the UI with the player's score.
    /// </summary>
    /// <param name="score">The new score to display.</param>
    public void UpdateScore(IntVariable score)
    {
        scoreText.text = "Score: " + score.Value.ToString();
    }

    /// <summary>
    /// Updates the UI with the player's high score.
    /// </summary>
    /// <param name="highScore">The highest score the player has obtained.</param>
    public void UpdateHighScore(IntVariable highScore)
    {
        highScoreText.text = "High Score: " + highScore.Value.ToString();
    }

    /// <summary>
    /// Displays the number of balls that are on the field.
    /// </summary>
    /// <param name="BallsInPlay">The number of balls in play.</param>
    public void UpdateBallsInPlay(IntVariable BallsInPlay)
    {
        ballsInPlay.text = "Balls Left: " + BallsInPlay.Value.ToString();
    }
}
