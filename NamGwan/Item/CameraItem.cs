using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraItem : Item
{
    public void Start() //액자는 플레이어의 최대체력이 10증가한다.
    {
        transform.parent.GetComponent<CharacterItem>().AddItem(this);
    }
    public override void ChangeSetting()
    {

    }

    public override void UpdateItem()
    {

    }
}
