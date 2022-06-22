using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GlobalState : IState
{
    private DateTime before_date;
    private int d_day;

    public GlobalState()
    {
        StateEnter();
    }

    public virtual void StateEnter()
    {
        before_date = DatabaseManager.Current_date;
        d_day = DatabaseManager.Instance.D_day;
    }

    public virtual void StateUpdate()
    {
        // 날짜가 지날 경우
        if( before_date.Day < DatabaseManager.Current_date.Day
            || before_date.Month < DatabaseManager.Current_date.Month 
            || before_date.Year < DatabaseManager.Current_date.Year)
        {
            // 날짜 초기화
            before_date = DatabaseManager.Current_date;

            // 디데이 플러스
            if (d_day == DatabaseManager.Instance.D_day)
            {
                DatabaseManager.Instance.D_day += 1;
                Clock.Instance.NotifyObservers();

                if (DatabaseManager.Instance.D_day >= 365)
                {
                    GameObject.Find("GameEnding").GetComponent<Ending>().EndingButton();
                    return;
                }
            }
            d_day = DatabaseManager.Instance.D_day;

            // 만약 포션이 구입된 상태라면.. (Level로 판단)
            if (DatabaseManager.SearchData("포션", DatabaseManager.Instance.my_furniture_list).level > 0)
            {
                DatabaseManager.SearchData("포션", DatabaseManager.Instance.my_furniture_list).level = 0;
            }

            // 만약 페이스 크림이 구입된 상태라면.. (Level로 판단)
            if (DatabaseManager.SearchData("페이스 크림", DatabaseManager.Instance.my_furniture_list).level > 0)
            {
                DatabaseManager.SearchData("페이스 크림", DatabaseManager.Instance.my_furniture_list).level = 0;
            }

            // 만약 월요일일때, 편집자 주급 나감 , 뉴스 업데이트
            if (before_date.DayOfWeek == DayOfWeek.Monday)
            {
                // 뉴스 업데이트 트리거
                //DatabaseManager.Instance.isNewsUpdate = true;

                if(DatabaseManager.Instance.my_editor_list.Count > 0) 
                {
                    int price = 0;

                    // 고용한 편집자 월급 차감
                    foreach (VideoEditorInfo editor in DatabaseManager.Instance.my_editor_list)
                    {
                        DatabaseManager.Player.wallet.Money = -editor.price;

                        price -= editor.price;
                    }

                    //돈 차감 사운드
                    AudioManager.Sound.Play("SE/Buying_Sound_Effect", E_SOUND.SE);

                    //돈 차감 팝업창
                    PopupBuilder popupBuilder = new PopupBuilder(GameObject.Find("PopupCanvas").transform);
                    popupBuilder.SetDescription("<color=" + "#FF3333" + ">" + (price).ToString() + "</color>");
                    popupBuilder.SetAnim("MoveUp");
                    popupBuilder.SetAutoDestroy(true);
                    popupBuilder.Build("TextPopupUI", 325.0f, -650.0f);

                }

                //만약 일주일이 지났다면 뉴스를 갱신해줌  만약 오류가 나신다면 개인 신에서 작업하시는곳에서 해당 오브젝트가 없어서입니다요 그때는 재량껏 
                GameObject.Find("MainCanvas").transform.Find("NewsPage").GetComponent<NewsEvent>().DayEvent();          



            }
        }

    }

    public virtual void StateExit()
    {
     
    }
}
