using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Adds colliders to the edge of the screen.  Add this script to the camera.
/// </summary>
public class ScreenColliders : MonoBehaviour
{
    #region Fields

    /// <summary>
    /// The col thickness
    /// </summary>
    [SerializeField] private float colThickness = 4f;

    /// <summary>
    /// The z position
    /// </summary>
    [SerializeField] private float zPosition = 0f;

    #endregion
    //---

    /// <summary>
    /// Start is called before the first frame update.
    /// </summary>
    private void Start()
    {
        //  Create a Dictionary to contain all our Objects/Transforms
        //  Create our GameObjects and add their Transform components to the Dictionary we created above
        Dictionary<string, Transform> colliders = new()
        {
            { "Top", new GameObject().transform },
            //{ "Bottom", new GameObject().transform },
            { "Right", new GameObject().transform },
            { "Left", new GameObject().transform }
        };

        //  Grab the world-space position values of the start and end positions of the screen, then calculate the distance between them and store it as half,
        //  since we only need half that value for distance away from the camera to the edge
        Vector2 screenSize = new()
        {
            x = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0))) * 0.5f,
            y = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height))) * 0.5f
        };

        //  For each Transform/Object in our Dictionary
        foreach (KeyValuePair<string, Transform> valPair in colliders)
        {
            //  Add the colliders.
            _ = valPair.Value.gameObject.AddComponent<BoxCollider2D>();

            valPair.Value.name = valPair.Key + "Collider";  //  Set the object's name to it's "Key" name, and take on "Collider".  i.e: TopCollider
            valPair.Value.tag = valPair.Value.name;  //  Set the object's tag to match it's name.
            valPair.Value.parent = transform;  //  Make the object a child of whatever object this script is on (preferably the camera).

            valPair.Value.localScale = valPair.Key is "Left" or "Right"
                ? new Vector3(colThickness, screenSize.y * 2, colThickness)
                : new Vector3(screenSize.x * 2, colThickness, colThickness);
        }

        //  Generate world space point information for position and scale calculations
        Vector3 cameraPos = Camera.main.transform.position;

        //  Change positions to align perfectly with outter-edge of screen, adding the world-space values of the screen we generated earlier,
        //  and adding/subtracting them with the current camera position, as well as add/subtracting half out objects size so it's not just half way off-screen
        colliders["Right"].position = new Vector3(cameraPos.x + screenSize.x + (colliders["Right"].localScale.x * 0.5f), cameraPos.y, zPosition);
        colliders["Left"].position = new Vector3(cameraPos.x - screenSize.x - (colliders["Left"].localScale.x * 0.5f), cameraPos.y, zPosition);
        colliders["Top"].position = new Vector3(cameraPos.x, cameraPos.y + screenSize.y + (colliders["Top"].localScale.y * 0.5f), zPosition);
        //colliders["Bottom"].position = new Vector3(cameraPos.x, cameraPos.y - screenSize.y - (colliders["Bottom"].localScale.y * 0.5f), zPosition);
    }
}
