using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorEmploySort : InfoSort, IAbilitySort, IPriceSort
{
    public override int Sort(Info A, Info B)
    {
        switch (sort_type)
        {
            case E_SORT.ABILITY:
                return AbilitySort(A, B);
            case E_SORT.PRICE:
                return PriceSort(A, B);
            default:
                break;
        }

        return 0;
    }

    public int AbilitySort(Info A, Info B)
    {
        VideoEditorInfo a = A as VideoEditorInfo;
        VideoEditorInfo b = B as VideoEditorInfo;

        if (a.ability < b.ability)
            return 1;
        else if (a.ability > b.ability)
            return -1;

        return 0;
    }

    public int PriceSort(Info A, Info B)
    {
        VideoEditorInfo a = A as VideoEditorInfo;
        VideoEditorInfo b = B as VideoEditorInfo;

        if (a.price < b.price)
            return 1;
        else if (a.price > b.price)
            return -1;

        return 0;
    }
}
