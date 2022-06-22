using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureSortFactory : AbsSortFactory
{
    public override InfoSort CreateSort()
    {
        return new FurnitureSort();
    }
}
