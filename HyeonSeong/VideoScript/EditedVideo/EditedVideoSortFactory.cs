using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditedVideoSortFactory : SlotSortFactory
{
    public override SlotSort CreateSort()
    {
        return new EditedVideoSort();
    }
}
