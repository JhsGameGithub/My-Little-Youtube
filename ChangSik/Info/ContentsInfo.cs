using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_CONTENTS_CATEGORY
{
    DEFAULT = -1,
    JUST_CHATTING,
    GAME,
    SIZE,
}

public enum E_CONTENTS_POPULARITY
{
    VERY_LOW = -3,
    LOW = 0,
    HIGH = 3,
    VERY_HIGH = 6,
}

[System.Serializable]
public class ContentsInfo : Info
{
    [SerializeField]
    public Sprite icon;
    //public string name;
    public int id;
    public int pc_level;
    public int price;
    public E_CONTENTS_CATEGORY[] con_category;
    public string[] con_tag;

    [SerializeField]
    private float popularity;

    public float Popularity 
    {
        get
        {
            return CalculatePopularity();
        }
    }

    public ContentsInfo()
    {
        icon = null;
        name = "";
        id = 0;
        pc_level = 0;
        price = 0;
        popularity = CalculatePopularity();
    }

    public ContentsInfo(ContentsInfo copy)
    {
        SetInfo(copy);
    }

    public override void SetInfo(Info copy)
    {
        ContentsInfo temp = copy as ContentsInfo;

        if (temp != null)
            SetInfo(temp);
        else
            Debug.LogError("다운 캐스팅 실패");
    }

    public void SetInfo(ContentsInfo copy)
    {
        icon = copy.icon;
        name = copy.name;
        id = copy.id;
        pc_level = copy.pc_level;
        price = copy.price;
        popularity = copy.Popularity;

        con_category = (E_CONTENTS_CATEGORY[])copy.con_category.Clone();
        con_tag = (string[])copy.con_tag.Clone();
    }

    public string GetCategories()
    {
        string category_text = "";


        for (int i = 0; i < con_category.Length; i++)
        {
            switch (con_category[i])
            {
                case E_CONTENTS_CATEGORY.JUST_CHATTING:
                    category_text += "JustChatting";
                    break;

                case E_CONTENTS_CATEGORY.GAME:
                    category_text += "Game";
                    break;
                default:
                    category_text += "default";
                    break;
            }

            category_text += (i == con_category.Length - 1) ? "" : ", ";
      
        }

        return category_text;
    }

    public float CalculatePopularity()
    {
        float sum = 0;

        if (con_tag != null)
        {
            foreach (string tag in con_tag)
            {
                sum += DatabaseManager.SearchData(tag, DatabaseManager.Instance.tag_list).Popularity;
            }
        }

        popularity = sum;

        return popularity;
    }

    public string GetPopularity()
    {
        string popularity_text = "";

        CalculatePopularity();

        if (Popularity >= (int)E_CONTENTS_POPULARITY.VERY_HIGH)
        {
            popularity_text = "매우 높음";
        }
        else if (Popularity >= (int)E_CONTENTS_POPULARITY.HIGH)
        {
            popularity_text = "높음";
        }
        else if (Popularity >= (int)E_CONTENTS_POPULARITY.LOW)
        {
            popularity_text = "낮음";
        }
        else if (Popularity >= (int)E_CONTENTS_POPULARITY.VERY_LOW)
        {
            popularity_text = "매우 낮음";
        }
        else
        {
            popularity_text = "잉 에러 에러";
        }

        return popularity_text;
    }
}
