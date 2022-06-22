using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreamingState : PlayerState
{
    public GameObject end_btn;
    public int fatigue_value;

    public override void StateEnter()
    {
        if (BoardcastManager.Instance)
        {
            BoardcastManager.Instance.OnBoardcast();

            if (end_btn != null)
                end_btn.SetActive(true);
        }

        base.StateEnter();

        // 시간 재생
        Clock.Instance.TimeNormalizer();
    }

    public override void StateUpdate()
    {
        check_time++;

        if (fatigue_time + DatabaseManager.SearchData("의자", DatabaseManager.Instance.my_furniture_list).skill[0].ability_value < check_time)
        {
            check_time = 0.0f;
            DatabaseManager.Player.status.HP = -fatigue_value;
        }

        if (BoardcastManager.Instance)
            BoardcastManager.Instance.OnAir();
        if (GetComponent<CharacterItem>())
        {
            GetComponent<CharacterItem>().UpdateItem();
        }
    }

    public override void StateExit()
    {
        if (BoardcastManager.Instance)
        {
            BoardcastManager.Instance.OutBoardcast();

            if (end_btn != null)
                end_btn.SetActive(false);
        }

        base.StateExit();
    }
}
