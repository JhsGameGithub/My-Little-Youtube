using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordedVideoSort : SlotSort, IFunnySort
{
    //유튭각 정렬_현성
    public override int Sort(GameObject A, GameObject B)
    {
        switch(sort_type)
        {
            case E_SORT.FUNNY:
                return FunnySort(A, B);
            default:
                break;
        }
        return 0;
    }

    public int FunnySort(GameObject A, GameObject B)
    {
        RecordedVideoInfo a = A.GetComponent<RecordedVideoSlot>().Info;
        RecordedVideoInfo b = B.GetComponent<RecordedVideoSlot>().Info;

        if (a.funny < b.funny)
            return 1;
        else if (a.funny > b.funny)
            return -1;

        return 0;
    }
}
