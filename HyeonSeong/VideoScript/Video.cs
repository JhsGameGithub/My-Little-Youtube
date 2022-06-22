using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Video : MonoBehaviour
{
    public string date;
    public string contents;
    public string tags;
    public int funny;

    protected void SetDate(int year,int month,int day)
    {
        date = year.ToString() + "³â " + month.ToString() + "¿ù " + day.ToString() + "ÀÏ";
    }
    protected void SetFunny(int level)
    {
        funny = level;
    }
    protected void SetContents(string content)
    {
        contents = content;        
    }
    protected void SetTags(string tag)
    {
        tags = tag;
    }
}
