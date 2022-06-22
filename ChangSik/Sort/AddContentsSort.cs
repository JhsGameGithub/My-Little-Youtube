using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddContentsSort : InfoSort, IPriceSort
{
    public override int Sort(Info A, Info B)
    {
        switch (sort_type)
        {
            case E_SORT.PRICE:
                return PriceSort(A, B);

            default:
                break;
        }

        return 0;
    }

    public int PriceSort(Info A, Info B)
    {
        AddContentsInfo a = A as AddContentsInfo;
        AddContentsInfo b = B as AddContentsInfo;

        if (a.price < b.price)
            return 1;
        else if (a.price > b.price)
            return -1;

        return 0;
    }
}
