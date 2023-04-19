using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusBrick : Brick
{
    protected override void BrickDestroyed()
    {
        //score.Value += ConfigurationUtils.BonusBrickPoints;
    }
}
