using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedupBrick : Brick
{
    protected override void BrickDestroyed()
    {
        //score.Value += ConfigurationUtils.SpecialBrickPoints;
    }
}
