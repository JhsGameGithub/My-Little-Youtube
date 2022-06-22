using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class Clock : MonoBehaviour, ISubjectUI
{
    public delegate void VideoHandler(bool active = false);
    public VideoHandler video_handler;

    //기본 시간 속도
    public float time_speed = 1.0f;

    //시간 가중 단위
    public float weight_unit = 1.0f;

    //시간 속도 제한
    public float time_limit = 5.0f;

    //시간 가중 속도
    float time_weight;

    public Text time_text;
    public Text time_info_text;

    public StateMachine state_machine;

    //private DateTime current_date;

    bool time_stop;

    private List<IObserverUI> observer_list = new List<IObserverUI>();

    #region 싱글톤
    private static Clock instance = null;

    public static Clock Instance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }

            return instance;
        }
    }

    //public DateTime Current_date
    //{
    //    get
    //    {
    //        if(current_date == null)
    //        {
    //            Current_date = DatabaseManager.Current_date;
    //        }

    //        return current_date;
    //    }
    //    set => current_date = value;
    //}

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        init();
    }
    #endregion

    #region 옵저버(서브젝트)
    public void AddObserver(IObserverUI observer)
    {
        observer_list.Add(observer);
    }

    public void RemoveObserver(IObserverUI observer)
    {
        observer_list.Remove(observer);
    }

    public void NotifyObservers(string message = "")
    {
        foreach (IObserverUI observer in observer_list)
        {
            observer.ObserverUpdate(message);
        }
    }

    #endregion

    #region 창식
    public void init()
    {
        time_stop = false;
        time_weight = time_speed;

        // 날짜 받아오기
        //Current_date = DatabaseManager.Current_date;

        state_machine = GameObject.Find("InGameCanvas").GetComponent<StateMachine>();

        if (time_text == null)
            time_text = GetComponent<Text>();

        StartCoroutine(GameStartCor());
    }

    private IEnumerator GameStartCor()
    {
        yield return new WaitForSeconds(0.3f);

        AudioManager.Sound.Play("BGM/Sunny_Animal_Crossing", E_SOUND.BGM);

        state_machine.SetSpeed(time_weight);
        state_machine.ChangeState("RestPlayerPage");

        InvokeRepeating("TimeUpdate", 0, time_speed);

        // 정지
        TimeStopper();

        // UI 초기화
        time_text.text = string.Format("{0:D2}:{1:D2}", DatabaseManager.Current_date.Hour, DatabaseManager.Current_date.Minute);

        DatabaseManager.Instance.isGameRun = true;
    }


    #endregion

    #region 시간 업데이트
    //시간 업데이트_조현성
    void TimeUpdate()
    {
        DatabaseManager.Current_date = DatabaseManager.Current_date.AddMinutes(1);

        if (DatabaseManager.Current_date.Minute == 0)
        {
            NotifyObservers();
        }

        state_machine.StateUpdate();

        time_text.text = string.Format("{0:D2}:{1:D2}", DatabaseManager.Current_date.Hour, DatabaseManager.Current_date.Minute);

        video_handler();
    }
    #endregion

    #region 버튼 이벤트 (시간 재생, 정지, 가속)
    public void OnTimeAccelerator()
    {
        AudioManager.Sound.Play("SE/button-3", E_SOUND.SE);
        TimeAccelerator();
    }

    public void OnTimeNormalizer()
    {
        AudioManager.Sound.Play("SE/button-3", E_SOUND.SE);
        TimeNormalizer();
    }

    public void OnTimeStopper()
    {
        AudioManager.Sound.Play("SE/button-3", E_SOUND.SE);
        TimeStopper();
    }

    #endregion

    #region 시간 재생, 정지, 가속

    //시간 가속_조현성
    public void TimeAccelerator()
    {
        if (time_weight < time_limit && !time_stop)
        {
            CancelInvoke("TimeUpdate");
            time_weight += weight_unit;
            time_info_text.text = "X" + time_weight + " 배속";

            state_machine.SetSpeed(time_weight);
            InvokeRepeating("TimeUpdate", 0, time_speed / time_weight);
        }
    }

    //시간 정상화_조현성
    public void TimeNormalizer()
    {
        if (time_stop || time_weight > time_speed)
        {
            time_info_text.text = time_stop ? "재생" : "X1 배속";

            CancelInvoke("TimeUpdate");
            time_weight = time_speed;
            time_stop = false;

            state_machine.SetSpeed(time_weight);
            InvokeRepeating("TimeUpdate", 0, time_speed);
        }
    }

    //시간 정지_조현성
    public void TimeStopper()
    {
        if (!time_stop)
        {
            time_info_text.text = "일시정지";
            time_stop = true;
            state_machine.SetSpeed(0.0f);
            CancelInvoke("TimeUpdate");
        }
    }
    //정지 받아오기 남관 
    public bool GetTimeStop()
    {
        return time_stop;
    }

    #endregion
}
