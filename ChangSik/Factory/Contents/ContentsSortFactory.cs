using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentsSortFactory : AbsSortFactory
{
    public override InfoSort CreateSort()
    {
        return new ContentsSort();
    }
}
