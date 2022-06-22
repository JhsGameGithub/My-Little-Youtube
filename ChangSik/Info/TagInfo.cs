using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class TagInfo : Info 
{
    public int id;
    [SerializeField] private float popularity;

    public float Popularity 
    {
        get
        {
            return popularity;
        }

        set
        {
            popularity = value;

        }
    }

    public TagInfo()
    {
        name = "";
        id = 0;
        popularity = 0;
    }

    public TagInfo(TagInfo copy)
    {
        SetInfo(copy);
    }

    public override void SetInfo(Info copy)
    {
        TagInfo temp = copy as TagInfo;

        if (temp != null)
            SetInfo(temp);
        else
            Debug.LogError("다운 캐스팅 실패");
    }

    public void SetInfo(TagInfo copy)
    {
        name = copy.name;
        id = copy.id;
        popularity = copy.popularity;
    }

}
