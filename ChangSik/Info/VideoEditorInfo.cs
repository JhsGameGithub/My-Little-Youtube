using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class VideoEditorInfo : Info
{
    //이름
   // public string name;
    //고유 아이디 (그냥 만들어놓은 속성)
    public int id;
    //편집 능력
    public int ability;
    //완성 시간
    public int time;
    //주급 (월요일)
    public int price;
    //고용 여부
    public bool is_employ;
    public bool is_working;

    public VideoEditorInfo()
    {
        name = "";
        id = 0;
        ability = 0;
        time = 1;
        price = 0;
        is_employ = false;
        is_working = false;
    }

    public VideoEditorInfo(VideoEditorInfo copy)
    {
        SetInfo(copy);
    }

    public override void SetInfo(Info copy)
    {
        VideoEditorInfo temp = copy as VideoEditorInfo;

        if (temp != null)
            SetInfo(temp);
        else
            Debug.LogError("다운 캐스팅 실패");
    }

    public void SetInfo(VideoEditorInfo copy)
    {
        name = copy.name;
        id = copy.id;
        ability = copy.ability;
        time = copy.time;
        price = copy.price;
        is_employ = copy.is_employ;
        is_working = copy.is_working;
    }
}
