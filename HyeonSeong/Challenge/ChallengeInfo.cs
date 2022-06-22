using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ChallengeInfo : Info
{
    public int achieve;
    public string title;
    public int index;

    public ChallengeInfo()
    {
        title = "";
        achieve = 0;
        index = 0;
    }

    public ChallengeInfo(string title, int achieve,int index)
    {
        SetInfo(title, achieve,index);
    }

    public override void SetInfo(Info copy)
    {
        ChallengeInfo temp = copy as ChallengeInfo;
        if (temp == null)
            Debug.LogError("다운 캐스팅 실패");
    }

    public void SetInfo(string title, int achieve,int index)
    {
        this.title = title;
        this.achieve = achieve;
        this.index = index;
    }
}
