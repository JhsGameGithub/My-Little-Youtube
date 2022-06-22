using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SnailEvent : MonoBehaviour
{
    public SnailEventVariety gameType;

    public GameObject[] line = new GameObject[3];

    public virtual void NewGame()
    {
        SetLine();
    }
    public void SetLine()
    {
        GameObject lineObj = GameObject.Find("MainCanvas").transform.Find("SnailGame").transform.Find("Game").gameObject.transform.Find("Line").gameObject;
        for (int i = 0; i < lineObj.transform.childCount; i++)
        {
            line[i] = lineObj.transform.GetChild(i).gameObject;
        }
    }
    public virtual void Initalize()
    {

    }
    public virtual void ActionEvent()
    {

    }
    public virtual bool Condition()
    {
        return true;
    }
}
