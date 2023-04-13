using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallGenerator : MonoBehaviour
{
    #region Game Objects

    /// <summary>
    /// Ball prefab.
    /// </summary>
    [SerializeField] private GameObject prefabBall;

    #endregion
    //---
    #region Events

    /// <summary>
    /// Event raised when spawning a new ball.  Notifies listeners that a new ball has been created.
    /// </summary>
    [SerializeField] private GameEvent OnSpawnNewBall;

    #endregion
    //---
    #region Scriptable Variables

    /// <summary>
    /// Keeps track of the number of balls on the field.
    /// </summary>
    [SerializeField] private IntVariable ballCount;

    #endregion
    //---
    #region Fields

    /// <summary>
    /// Flag for indicating if the ball spawner is active.
    /// </summary>
    private bool ballSpawnerRunning = false;

    /// <summary>
    /// The last time a ball was spawned.
    /// </summary>
    private float lastBallSpawnTime = 0.0f;

    /// <summary>
    /// The amount of time in seconds to wait before spawning another ball.
    /// </summary>
    private float nextBallSpawnTime = 0.0f;

    #endregion
    //***G

    /// <summary>
    /// Start is called before the first frame update.
    /// </summary>
    private void Start()
    {
        ballCount.Value = 0;
    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    private void Update()
    {
    }

    /// <summary>
    /// Fixed update is called at a fixed framerate time.
    /// </summary>
    private void FixedUpdate()
    {
        //  Wait for game start.
        if (!ballSpawnerRunning) { return; }

        //  Wait until the designated time to spawn a new ball.
        if (Time.time - lastBallSpawnTime < nextBallSpawnTime) { return; }

        SpawnNewBall();
    }

    //---G

    /// <summary>
    /// Called when the game starts.
    /// </summary>
    public void OnStartGame()
    {
        ballSpawnerRunning = true;
    }

    /// <summary>
    /// Called when the game is set to end.  Destroys all remaining balls on the field.
    /// </summary>
    public void OnEndGame()
    {
        ballSpawnerRunning = false;

        //  Find and destroy any balls that remain on the playing field.
        GameObject[] balls = GameObject.FindGameObjectsWithTag(ConfigurationUtils.BallTag);
        if (balls.Length > 0)
        {
            for (int i = balls.Length - 1; i >= 0; i--)
            {
                Destroy(balls[i]);
            }
        }
    }

    /// <summary>
    /// Spawns a new ball.
    /// </summary>
    private void SpawnNewBall()
    {
        //  Capture the spawn time and set the amount of time to wait before spawning the next ball.
        lastBallSpawnTime = Time.time;
        nextBallSpawnTime = Random.Range(5, 10);

        //  Spawn the ball below the brick field, but above the paddle location.
        _ = Instantiate(prefabBall, new Vector3(0, ScreenUtils.ScreenBottom + (ScreenUtils.ScreenSectionHeight * 3), 0), Quaternion.identity);

        //  Increase the ball counter.
        ballCount.Value++;

        //  Inform listeners that a new ball has been spawned.
        OnSpawnNewBall.Raise();
    }
}
