using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AddContentsInfo : Info
{
    public Sprite icon;
    public int price;

    [Multiline(3)]
    public string description;

    [Multiline(3)]
    public string e_description;

    public FlexItemList itemType;
    public AddContentsInfo()
    {
        icon = null;
        price = 0;
        name = "";
        description = "";
        e_description = "";

    }

    public AddContentsInfo(AddContentsInfo copy)
    {
        SetInfo(copy);
    }

    public override void SetInfo(Info copy)
    {
        AddContentsInfo temp = copy as AddContentsInfo;

        if (temp != null)
            SetInfo(temp);
        else
            Debug.LogError("다운 캐스팅 실패");
    }

    public void SetInfo(AddContentsInfo copy)
    {
        icon = copy.icon;
        price = copy.price;
        name = copy.name;
        description = copy.description;
        e_description = copy.e_description;
        itemType = copy.itemType;
    }
}
