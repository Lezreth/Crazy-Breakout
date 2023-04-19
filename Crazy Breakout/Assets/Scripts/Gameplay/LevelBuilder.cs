using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    #region Serialized Fields

    /// <summary>
    /// Brick prefabs.
    /// </summary>
    [SerializeField] private List<GameObject> prefabBricks;

    #endregion
    //---
    #region Scriptable Variables

    /// <summary>
    /// Space between the bricks and the walls, and other bricks.
    /// </summary>
    [SerializeField] private FloatConstant brickBufferSpace;

    /// <summary>
    /// Tag for identifying objects that are bricks.
    /// </summary>
    [SerializeField] private StringConstant brickTag;

    #endregion
    #region Helper Functions


    private RandomWeightedPicker<Brick> picker;


    private List<Brick> bricks = new();

    #endregion
    //---G

    /// <summary>
    /// Start is called before the first frame update.
    /// </summary>
    private void Start()
    {
        foreach (GameObject prefab in prefabBricks)
        {
            bricks.Add(prefab.GetComponent<Brick>());
        }

        picker = new RandomWeightedPicker<Brick>(bricks);
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
        float horizontalSpace = Mathf.Abs(ScreenUtils.ScreenLeft) + Mathf.Abs(ScreenUtils.ScreenRight) - brickBufferSpace.Value;
        float verticalSpace = (Mathf.Abs(ScreenUtils.ScreenTop) + Mathf.Abs(ScreenUtils.ScreenBottom)) / 2;

        //  Create a sample brick and extract it's dimensions.
        GameObject testBrick = Instantiate(prefabBricks[0], new Vector3(ScreenUtils.ScreenRight + 5, ScreenUtils.ScreenBottom + 5, 0), Quaternion.identity);
        Vector3 brickSize = testBrick.GetComponent<BoxCollider2D>().bounds.size;
        Destroy(testBrick);

        //  Add the buffer to the brick size so they are not jammed up against each other.
        brickSize.x += brickBufferSpace.Value;
        brickSize.y += brickBufferSpace.Value;

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
                    //  Pick a brick.  The picker is a randomizer with weights.
                    Brick foundBrick = picker.PickAnItem();

                    //  Get the index of the chosen brick.
                    int brickIndex = prefabBricks.IndexOf(prefabBricks.Find(x => x.GetComponent<Brick>() == foundBrick));

                    //  Generate the selected brick and place it on the field.
                    GameObject brick = Instantiate(prefabBricks[brickIndex], new(ScreenUtils.ScreenLeft + ((i + 1) * brickSize.x), ScreenUtils.ScreenTop - ((j + 1) * brickSize.y), 0), Quaternion.identity);

                    //  Randomize the new brick's color.
                    brick.GetComponent<SpriteRenderer>().material.color = new Color(Random.value, Random.value, Random.value);
                }
            }
        }
    }

    /// <summary>
    /// Destroys all of the bricks on the playing field.
    /// </summary>
    public void DestroyAllBricks()
    {
        //  Find and destroy any bricks that remain on the playing field.
        GameObject[] bricks = GameObject.FindGameObjectsWithTag(brickTag.Value);
        if (bricks.Length > 0)
        {
            for (int i = bricks.Length - 1; i >= 0; i--)
            {
                Destroy(bricks[i]);
            }
        }
    }
}
