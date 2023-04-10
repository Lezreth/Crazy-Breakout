using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    #region Serialized Fields

    /// <summary>
    /// Brick prefab.
    /// </summary>
    [SerializeField] private GameObject prefabBrick;

    #endregion
    //---
    #region Fields

    #endregion
    //---G

    /// <summary>
    /// Start is called before the first frame update.
    /// </summary>
    private void Start()
    {
    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    private void Update()
    {
    }

    /// <summary>
    /// Spawn the bricks on the brick field.  The brick field is the upper half of the screen.
    /// </summary>
    public void SpawnBricks()
    {
        //  Calculate the available space for the bricks.
        float horizontalSpace = Mathf.Abs(ScreenUtils.ScreenLeft) + Mathf.Abs(ScreenUtils.ScreenRight) - ConfigurationUtils.BrickBufferSpace;
        float verticalSpace = (Mathf.Abs(ScreenUtils.ScreenTop) + Mathf.Abs(ScreenUtils.ScreenBottom)) / 2;

        //  Create a sample brick and extract it's dimensions.
        GameObject testBrick = Instantiate(prefabBrick, new Vector3(ScreenUtils.ScreenRight + 5, ScreenUtils.ScreenBottom + 5, 0), Quaternion.identity);
        Vector3 brickSize = testBrick.GetComponent<BoxCollider2D>().bounds.size;
        Destroy(testBrick);

        //  Add the buffer to the brick size so they are not jammed up against each other.
        brickSize.x += ConfigurationUtils.BrickBufferSpace;
        brickSize.y += ConfigurationUtils.BrickBufferSpace;

        //  Calculate the maximum number of bricks that will fit in the brick field.
        int maxHorizontalBricks = Mathf.FloorToInt(horizontalSpace / brickSize.x);
        int maxVerticalBricks = Mathf.FloorToInt(verticalSpace / brickSize.y);

        //  Spawn and position the bricks on the brick field.
        for (int i = 0; i < maxHorizontalBricks - 1; i++)
        {
            for (int j = 0; j < maxVerticalBricks; j++)
            {
                if (Random.value < 0.2f)
                {
                    _ = Instantiate(prefabBrick, new(ScreenUtils.ScreenLeft + ((i + 1) * brickSize.x), ScreenUtils.ScreenTop - ((j + 1) * brickSize.y), 0), Quaternion.identity);
                }
            }
        }
    }
}
