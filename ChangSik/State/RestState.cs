using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestState : PlayerState
{
    public override void StateEnter()
    {
        base.StateEnter();

        // 시간 재생
        Clock.Instance.TimeNormalizer();
    }

    public override void StateUpdate()
    {
        check_time++;

        if (fatigue_time < check_time)
        {
            check_time = 0.0f;
            if ( DatabaseManager.Player.status.MaxHP >  DatabaseManager.Player.status.HP)
            {
                 DatabaseManager.Player.status.HP = 1;
            }
        }
        if(GetComponent<CharacterItem>())
        {
            GetComponent<CharacterItem>().UpdateItem();
        }
    }

    public override void StateExit()
    {
        base.StateExit();
    }
}
