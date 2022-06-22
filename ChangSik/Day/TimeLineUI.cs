using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public enum E_TimeLine
{
    Dawn = 5,
    Morningt = 9,
    Afternoon = 17,
    Evening = 21,
    Night = 24,
}

public class TimeLineUI : MonoBehaviour, ILoading
{
    public GameObject SlotField;
    public CanvasGroup fade_ui;

    private TimeLineSlot[] slot_arr;
    private int rot_val = 72;

    private Sequence show_sequence;
    private CallbackEvent callback = null;
    private int add_time;

    private void Awake()
    {
        fade_ui.alpha = 0.0f;
        slot_arr = SlotField.GetComponentsInChildren<TimeLineSlot>();
    }

    private void Start()
    {
        StartCoroutine(StartCo());

    }

    IEnumerator StartCo()
    {
        yield return new WaitForSeconds(0.5f);
        ShowAnim();
    }

    public void ShowAnim()
    {
        int before_time = (DatabaseManager.Current_date.Hour * 60) + DatabaseManager.Current_date.Minute;
        int after_time = before_time + add_time;

        if (after_time >= 24 * 60)
        {
            after_time = after_time - (24 * 60);
        }

        // 회전 값 받아오기
        int be_rot = GetTimeLineValue(before_time);
        int aft_rot = GetTimeLineValue(after_time);

        // 각 버튼 배열 위치
        int start_index = be_rot / rot_val;
        int end_index = aft_rot / rot_val;

        // 활성시 보여주는 트윈 애니메이션
        show_sequence = DOTween.Sequence().OnStart(() =>
        {
            // 초기화
            fade_ui.alpha = 0.0f;
            SlotField.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, -be_rot);
        })
        .Append(slot_arr[start_index].ShowAnim())
        .AppendInterval(0.5f);

        AddAnim(start_index, end_index);
    }

    public void AddAnim(int start_index, int end_index)
    {
        for (int i = start_index; i < end_index;)
        {
            show_sequence
            .Append(slot_arr[i++].HideAnim())
            .Join(SlotField.transform.DORotate(new Vector3(0.0f, 0.0f, -rot_val * i), 0.5f))
            .Join(slot_arr[i].ShowAnim());
        }

        show_sequence.Append(fade_ui.DOFade(1.0f, 1.0f))
        .OnComplete(() =>
        {
            if (callback != null)
                callback();

            Destroy(gameObject);
        });
    }

    public int GetTimeLineValue(int value)
    {
        if ((int)E_TimeLine.Dawn * 60 > value)
        {
            return rot_val * 0;
        }
        else if ((int)E_TimeLine.Morningt * 60 > value)
        {
            return rot_val * 1;
        }
        else if ((int)E_TimeLine.Afternoon * 60 > value)
        {
            return rot_val * 2;
        }
        else if ((int)E_TimeLine.Evening * 60 > value)
        {
            return rot_val * 3;
        }
        else if ((int)E_TimeLine.Night * 60 > value)
        {
            return rot_val * 4;
        }

        return 0;
    }

    public void SetCallback(CallbackEvent _callback)
    {
        callback = _callback == null ? () => { } : _callback;
    }

    public void SetAddTime(float add_time)
    {
        this.add_time = (int)add_time;
    }


}
