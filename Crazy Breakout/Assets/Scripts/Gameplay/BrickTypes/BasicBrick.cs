using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBrick : Brick
{
    protected override void BrickDestroyed()
    {
        //score.Value += ConfigurationUtils.BasicBrickPoints;
    }
}
