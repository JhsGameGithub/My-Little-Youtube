using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InfoSort
{
    public abstract int Sort(Info A, Info B);

    protected E_SORT sort_type;

    public E_SORT SortType
    {
        get
        {
            return sort_type;
        }

        set
        {
            sort_type = value;
        }
    }

}
