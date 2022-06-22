using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditedVideoSort : SlotSort, IDateSort, IGoodSort, IViewSort
{
    //��¥ ����, ���ƿ� ����, ��ȸ�� ���� _ ����
    public override int Sort(GameObject A, GameObject B)
    {
        switch (sort_type)
        {
            case E_SORT.DATE:
                return DateSort(A, B);
            case E_SORT.GOOD:
                return GoodSort(A, B);
            case E_SORT.VIEW:
                return ViewSort(A, B);
            default:
                break;
        }
        return 0;
    }

    //��¥��� �ε�����ȣ�� ����
    public int DateSort(GameObject A, GameObject B)
    {
        EditedVideoInfo a = A.GetComponent<EditedVideoSlot>().Info;
        EditedVideoInfo b = B.GetComponent<EditedVideoSlot>().Info;

        //b�� �� ũ�� �ڸ��ٲ��� �Ͼ
        if (a.hour < b.hour ||
            a.day < b.day ||
            a.month < b.month ||
            a.year < b.year)
        {
            return 1;
        }
        else if (a.hour > b.hour ||
            a.day > b.day ||
            a.month > b.month ||
            a.year > b.year)
        {
            return -1;
        }

        return 0;
    }

    public int GoodSort(GameObject A, GameObject B)
    {
        EditedVideoInfo a = A.GetComponent<EditedVideoSlot>().Info;
        EditedVideoInfo b = B.GetComponent<EditedVideoSlot>().Info;

        if (a.goods < b.goods)
            return 1;
        else if (a.goods > b.goods)
            return -1;
        return 0;
    }

    public int ViewSort(GameObject A, GameObject B)
    {
        EditedVideoInfo a = A.GetComponent<EditedVideoSlot>().Info;
        EditedVideoInfo b = B.GetComponent<EditedVideoSlot>().Info;

        if (a.views < b.views)
            return 1;
        else if (a.views > b.views)
            return -1;
        return 0;
    }
}
