using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentsSort : InfoSort, ILevelSort, IPriceSort, IPopularitySort
{
    public override int Sort(Info A, Info B)
    {
        switch (sort_type)
        {
            case E_SORT.LEVEL:
                return LevelSort(A, B);
            case E_SORT.PRICE:
                return PriceSort(A, B);
            case E_SORT.POPULARITY:
                return PopularitySort(A, B);
            default:
                break;
        }

        return 0;
    }

    public int LevelSort(Info A, Info B)
    {
        ContentsInfo a = A as ContentsInfo;
        ContentsInfo b = B as ContentsInfo;

        if (a.pc_level < b.pc_level)
            return 1;
        else if (a.pc_level > b.pc_level)
            return -1;

        return 0;
    }

    public int PriceSort(Info A, Info B)
    {
        ContentsInfo a = A as ContentsInfo;
        ContentsInfo b = B as ContentsInfo;

        if (a.price < b.price)
            return 1;
        else if (a.price > b.price)
            return -1;

        return 0;
    }

    public int PopularitySort(Info A, Info B)
    {
        ContentsInfo a = A as ContentsInfo;
        ContentsInfo b = B as ContentsInfo;

        if (a.Popularity < b.Popularity)
            return 1;
        else if (a.Popularity > b.Popularity)
            return -1;

        return 0;
    }
}
