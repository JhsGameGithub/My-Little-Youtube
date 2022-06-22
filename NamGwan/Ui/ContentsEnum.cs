using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentsEnum : MonoBehaviour 
{
    //모든 컨텐츠 프리팹 
    public List<Contents> allContents;

    //모든 컨텐츠 프리팹을 정리한다.
    public void Setting()
    {
        foreach(var con in allContents)
        {
            //리스트 안에 중복된 내용이 있는가 ? 
            if (!gameName.Contains(con.info.name))
            {
                gameName.Add(con.name);
            }
            //이중 반복문 안에 겹치는 태그가 있는가?
            foreach(var tag in con.info.con_tag)
            {
                if(!con_tag.Contains(tag))
                {
                    con_tag.Add(tag);
                }
            }
        }
    }
    [Header("모든 게임의 이름")]
    public List<string> gameName; //모든 게임 이름 
    [Header("모든 컨텐츠들의 태그")]
    public List<string> con_tag; //모든 컨텐츠의 태그 
}
