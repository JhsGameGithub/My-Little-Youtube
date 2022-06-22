using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardcastOwner : MonoBehaviour
{
    public List<BoardcastObject> obj; 
    
    void Start()
    {
        obj = new List<BoardcastObject>();

        for(int i=0; i<transform.childCount;i++)
        {
            obj.Add(transform.GetChild(i).GetComponent<BoardcastObject>());
        }
    }

    private void LateUpdate()
    {
        foreach(BoardcastObject get in obj)
        {
            get.Action();
        }
    }
}
