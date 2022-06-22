using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordedVideoSortFactory : SlotSortFactory
{
    public override SlotSort CreateSort()
    {
        return new RecordedVideoSort();
    }
}