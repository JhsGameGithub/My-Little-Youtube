using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingItem : Item //액자 
{
    public void Start() //액자는 플레이어의 최대체력이 10증가한다.
    {
        transform.parent.GetComponent<CharacterItem>().AddItem(this);
       DatabaseManager.Player.status.havePaint = true;

        DatabaseManager.Player.status.HP = 10;

    }
    public override void ChangeSetting()
    {

    }

    public override void UpdateItem()
    {

    }

}
