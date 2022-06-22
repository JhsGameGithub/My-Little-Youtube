using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public enum SnailEventVariety
{
    GUN,
    SIZE,
}

public class SnailGameEvent : MonoBehaviour
{
    public SnailEvent handle = null;
     
    public void ActionEvent()
    {
        if(handle ==null)
        {
            SetEvent();
        }
        handle.ActionEvent();
        
    }
    public void NewGame()
    {
        handle.Initalize();
    }
    public void SetEvent()
    {
        handle = gameObject.AddComponent<GunEvent>();
    }
    public void Start()
    {
        SetEvent();
    }
}
