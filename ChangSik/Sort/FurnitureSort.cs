using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureSort : InfoSort, ILevelSort, IPriceSort
{
    public override int Sort(Info A, Info B)
    {
        switch (sort_type)
        {
            case E_SORT.LEVEL:
                return LevelSort(A, B);
            case E_SORT.PRICE:
                return PriceSort(A, B);
            default:
                break;
        }

        return 0;
    }

    public int LevelSort(Info A, Info B)
    {
        FurnitureInfo a = A as FurnitureInfo;
        FurnitureInfo b = B as FurnitureInfo;

        if (a.level < b.level)
            return 1;
        else if (a.level > b.level)
            return -1;

        return 0;
    }

    public int PriceSort(Info A, Info B)
    {
        FurnitureInfo a = A as FurnitureInfo;
        FurnitureInfo b = B as FurnitureInfo;

        if (a.price < b.price)
            return 1;
        else if (a.price > b.price)
            return -1;

        return 0;
    }
}
