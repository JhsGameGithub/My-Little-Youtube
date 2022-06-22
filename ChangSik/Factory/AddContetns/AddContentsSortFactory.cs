using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddContentsSortFactory : AbsSortFactory
{
    public override InfoSort CreateSort()
    {
        return new AddContentsSort();
    }
}
