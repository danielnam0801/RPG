using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGroundDecision : AIDecision
{
    public override bool MakeDecision()
    {
        return _aIActionData.IsGround;
    }
}
