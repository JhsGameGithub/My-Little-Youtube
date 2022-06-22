using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SlotSort
{
    protected E_SORT sort_type;

    public abstract int Sort(GameObject A, GameObject B);

    public E_SORT SortType
    {
        get { return sort_type; }
        set { sort_type = value; }
    }
}
