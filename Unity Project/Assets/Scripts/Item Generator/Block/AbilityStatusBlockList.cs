using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityStatusBlockList : BlockList
{
    public void Critical(Status status, float value)
    {
        status.criticalProbability += value;
    }
}
