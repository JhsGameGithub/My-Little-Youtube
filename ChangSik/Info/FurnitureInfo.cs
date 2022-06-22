using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FurnitureInfo : Info
{
    public Sprite icon;
    //public string name;
    public int id;
    public int level;
    public int price;
    [Multiline(3)]
    public string info_text;

    [Header("Skills")]
    public Skill[] skill;

    public FurnitureInfo()
    {
        icon = null;
        name = "";
        id = 0;
        level = 0;
        price = 0;
        info_text = "";

    }

    public FurnitureInfo(FurnitureInfo copy)
    {
        SetInfo(copy);
    }

    public override void SetInfo(Info copy)
    {
        FurnitureInfo temp = copy as FurnitureInfo;

        if (temp != null)
            SetInfo(temp);
        else
            Debug.LogError("다운 캐스팅 실패");
    }

    public void SetInfo(FurnitureInfo copy)
    {
        icon = copy.icon;
        name = copy.name;
        id = copy.id;
        level = copy.level;
        price = copy.price;
        info_text = copy.info_text;

        if (copy.skill != null)
            skill = (Skill[])copy.skill.Clone();
    }

}
