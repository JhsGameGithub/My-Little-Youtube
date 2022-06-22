using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoEditorSortFactory : AbsSortFactory
{
    public override InfoSort CreateSort()
    {
        return new EditorEmploySort();
    }
}
