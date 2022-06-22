using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SleepState : PlayerState
{
    public RecordedVideoUI recorded_video_ui;
    public EditedVideoUI edited_video_ui;
    public override void StateEnter()
    {
        // 시간 일시정지
        Clock.Instance.TimeStopper();
        AudioManager.Instance.FadeOutSound(1.0f);

        if (DatabaseManager.Player.status.HP <= 0)
        {
            //12시간 수면
            SetFatigueTime(12);

            // 건강 1 하락
            DatabaseManager.Player.status.Health -= 1;

            for (int i = 0; i < 720; i++)
            {
                recorded_video_ui.EditingVideo();
                edited_video_ui.ProfitingVideo();
            }
        }
        else
        {
            //AudioManager.Sound.Play("SE/musmus_st019", E_SOUND.SE);

            //8시간 수면
            SetFatigueTime(8);
            for (int i = 0; i < 480; i++)
            {
                recorded_video_ui.EditingVideo();
                edited_video_ui.ProfitingVideo();
            }
        }

        // 최대치
        DatabaseManager.Player.status.HP += 1000;

        // 업데이트 직접 호출
        StateUpdate();
    }

    // 직접 호출하며, 한 번만 호출
    public override void StateUpdate()
    {
        DateTime before_date = DatabaseManager.Current_date;

        string prefab_name = "TimeLineUI";

        if (before_date.Hour * 60 + fatigue_time >= 24 * 60)
        {
            prefab_name = "NextDayUI";
            DatabaseManager.Instance.D_day += 1;

            if (DatabaseManager.Instance.D_day >= 365)
            {
                GameObject.Find("GameEnding").GetComponent<Ending>().EndingButton();
                return;
            }
        }

        // 날짜 로딩 UI
        LoadUI(prefab_name, () =>
        {
            // 직접 시간 데이터 셋팅
            //Clock.Instance.SetDate(after_year, after_month, after_day, after_hour);
            DatabaseManager.Current_date = DatabaseManager.Current_date.AddMinutes(fatigue_time);

            // UI 업데이트
            Clock.Instance.NotifyObservers();

            // 데이터 저장
            //VideoManager.Instance.DataSave();
            DatabaseManager.Instance.DataSave();

            Clock.Instance.state_machine.ChangeState("RestPlayerPage");

        });
    }

    public override void StateExit()
    {
        // 시간 재생
        Clock.Instance.TimeNormalizer();

        //AudioManager.Instance.FadeOutSound(0.1f, false, () =>
        // {
        //     //AudioManager.Sound.Play("BGM/musmus_st019", E_SOUND.BGM);
        //     AudioManager.Sound.Play("BGM/Sunny_Animal_Crossing", E_SOUND.BGM);
        //     //AudioManager.Instance.FadeInSound(0.5f);
        // });

        AudioManager.Sound.Play("BGM/Sunny_Animal_Crossing", E_SOUND.BGM);
    }

    public void SetFatigueTime(int _time)
    {
        fatigue_time = (_time * 60) - DatabaseManager.SearchData("침대", DatabaseManager.Instance.my_furniture_list).skill[0].ability_value;
    }

    private void LoadUI(string name, CallbackEvent callback = null)
    {
        // 다음 날짜 UI 연출 (임시)
        GameObject parent = GameObject.Find("LoadingCanvas");
        GameObject obj = Instantiate(Resources.Load("Prefabs/UI/LoadingUI/" + name), new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        obj.transform.SetParent(parent.transform, false);
        obj.GetComponent<ILoading>().SetCallback(callback);
        obj.GetComponent<ILoading>().SetAddTime(fatigue_time);
    }

}
