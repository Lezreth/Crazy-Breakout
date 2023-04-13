using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Creates edge colliders around the border of the viewing area of the camera object this script is attached to.  Used in making bounds for 2D games.
/// Edge collider is used because a box collider has an inside.  Any object on the inside of a box collider is always colliding with it.
/// </summary>
public class Boundary : MonoBehaviour
{
    #region Fields

    /// <summary>
    /// The boundary edge collider.
    /// </summary>
    private EdgeCollider2D BoundaryEdge;

    /// <summary>
    /// The camera to wrap the edges around.
    /// </summary>
    private Camera mainCamera;

    /// <summary>
    /// The camera's viewport width.
    /// </summary>
    private float width;

    /// <summary>
    /// The camera's viewport height.
    /// </summary>
    private float height;

    #endregion
    //***G
    #region Unity Methods

    /// <summary>
    /// Awake is called before start.
    /// </summary>
    private void Awake()
    {
        mainCamera = Camera.main;
        BoundaryEdge = GetComponent<EdgeCollider2D>();

    }

    /// <summary>
    /// Update is called once per frame.  Finding and setting the boundaries every frame allows the screen to be resized and the collider will automatically adjust.
    /// </summary>
    private void Update()
    {
        FindBoundaries();
        SetBounds();
    }

    #endregion
    //---G
    #region Supplemental Methods

    /// <summary>
    /// Finds the borders of the world that the camera is looking at.
    /// </summary>
    private void FindBoundaries()
    {
        width = 1 / (mainCamera.WorldToViewportPoint(new Vector3(1, 1, 0)).x - 0.5f);
        height = 1 / (mainCamera.WorldToViewportPoint(new Vector3(1, 1, 0)).y - 0.5f);
    }

    /// <summary>
    /// Creates the edge collider points and aligns them at the borders the boundary finder located.
    /// </summary>
    private void SetBounds()
    {
        Vector2 pointA = new(width / 2, height / 2);
        Vector2 pointB = new(width / 2, -height / 2);
        Vector2 pointC = new(-width / 2, -height / 2);
        Vector2 pointD = new(-width / 2, height / 2);

        //  Assign the points to the edge collider.  the edges go from A to B, B to C, C to D, and D to A.
        Vector2[] pointsArray = new Vector2[] { pointC, pointD, pointA, pointB };
        BoundaryEdge.points = pointsArray;
    }

    #endregion
}
