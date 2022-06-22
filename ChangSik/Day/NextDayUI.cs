using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public enum E_MONTH_TEXT
{
    January = 1,
    February = 2,
    March = 3,
    April = 4,
    May = 5,
    June = 6,
    July = 7,
    August = 8,
    September = 9,
    October = 10,
    November = 11,
    December = 12,
}

public class NextDayUI : MonoBehaviour, ILoading
{
    public RectTransform bar_rect;
    public RectTransform light_rect;
    public DateSlot temp_slot;

    public Text current_year;
    public Text current_month;

    public CanvasGroup fade_img;

    //슬롯 부모
    [SerializeField]
    private GameObject slot_parent;

    //생성된 슬롯을 담는 리스트
    private List<DateSlot> slot_list = new List<DateSlot>();

    //10개의 날짜만 보여주기 (즉 10개의 슬롯만 생성)
    private int slot_count = 10;

    private float line_height;
    private float light_height;

    private Sequence show_sequence;
    private Sequence hide_sequence;

    private CallbackEvent callback = null;

    // Start is called before the first frame update
    void Start()
    {
        // 초기화
        line_height = bar_rect.sizeDelta.y;
        bar_rect.sizeDelta = new Vector2(bar_rect.sizeDelta.x, 0.0f);

        light_height = light_rect.sizeDelta.y;
        light_rect.sizeDelta = new Vector2(light_rect.sizeDelta.x, 0.0f);

        // 임시 날짜
        temp_slot.gameObject.SetActive(false);

        // 날짜 슬롯 초기화
        SlotsInit();

        // 슬롯 오브젝트 비활성화
        SetActiveSlot(false);

        // 활성시 보여주는 트윈 애니메이션
        show_sequence = DOTween.Sequence().Pause();
        show_sequence.Append(ShowDayAnim());

        // 비활성시 보여주는 트윈 애니메이션
        hide_sequence = DOTween.Sequence().Pause();
        DaySlotHide();
        hide_sequence.Append(HideDayAnim());

        show_sequence.Append(hide_sequence);
        
        // UI 연출
        StartCoroutine(StartAnim());
    }

    // 각 슬롯 오브젝트 생성 및 초기화
    private void SlotsInit()
    {
        SlotsClear();


        DateTime dateTime = DatabaseManager.Current_date.AddDays(-1);

        for (int i = 0; i < slot_count; i++)
        {
            GameObject obj = Instantiate(Resources.Load("Prefabs/UI/Slot/" + "DaySlot"), new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            obj.transform.SetParent(slot_parent.transform, false);
            obj.GetComponent<DateSlot>().Init(dateTime.AddDays(i));
            obj.GetComponent<DateSlot>().SetUI();
            slot_list.Add(obj.GetComponent<DateSlot>());
        }

        dateTime = dateTime.AddDays(2);
        current_year.text = dateTime.Year.ToString();
        current_month.text = GetMonth(dateTime.Month);
    }

    // 각 슬롯 오브젝트 삭제
    private void SlotsClear()
    {
        for (int i = 0; i < slot_list.Count; i++)
        {
            if (slot_list[i] != null)
                Destroy(slot_list[i].gameObject);
        }

        slot_list.Clear();
    }

    private void SetBarSlot()
    {
        // 날짜 초기화
        temp_slot.Init(slot_list[2].Current_date);

        // UI 초기화
        temp_slot.SetUI();

        // 임시 날짜
        temp_slot.gameObject.SetActive(true);


        //GameObject obj = Instantiate(slot_list[2].gameObject, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        //obj.transform.SetParent(slot_field.transform, false);

        //obj.GetComponent<RectTransform>().anchorMax = new Vector2(1.0f, 1.0f);
        //obj.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
    }

    private void SetActiveSlot(bool _active)
    {
        for (int i = 0; i < slot_list.Count; i++)
        {
            slot_list[i].gameObject.SetActive(_active);
        }
    }

    private string GetMonth(int _month)
    {
        string month_text = "";

        switch ((E_MONTH_TEXT)_month)
        {
            case E_MONTH_TEXT.January:
                month_text = "January";
                break;
            case E_MONTH_TEXT.February:
                month_text = "February";
                break;
            case E_MONTH_TEXT.March:
                month_text = "March";
                break;
            case E_MONTH_TEXT.April:
                month_text = "April";
                break;
            case E_MONTH_TEXT.May:
                month_text = "May";
                break;
            case E_MONTH_TEXT.June:
                month_text = "June";
                break;
            case E_MONTH_TEXT.July:
                month_text = "July";
                break;
            case E_MONTH_TEXT.August:
                month_text = "August";
                break;
            case E_MONTH_TEXT.September:
                month_text = "September";
                break;
            case E_MONTH_TEXT.October:
                month_text = "October";
                break;
            case E_MONTH_TEXT.November:
                month_text = "November";
                break;
            case E_MONTH_TEXT.December:
                month_text = "December";
                break;
            default:
                break;
        }

        return month_text;

    }

    IEnumerator StartAnim()
    {
        yield return new WaitForSeconds(0.5f);
        show_sequence.Restart();

        //yield return new WaitForSeconds(5.0f);
        //hide_sequence.Restart();
    }
    IEnumerator DestroyCo()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

    public Sequence ShowDayAnim()
    {
        return DOTween.Sequence().Append(bar_rect.DOSizeDelta(new Vector2(bar_rect.sizeDelta.x, line_height), 0.5f))
            .Join(light_rect.DOSizeDelta(new Vector2(light_rect.sizeDelta.x, light_height), 0.5f))
            .Join(DOTween.Sequence().OnStart(() =>
            {
                // 페이드 이미지 알파 맥스
                fade_img.alpha = 1.0f;

                // 임시 날짜 (비활성)
                temp_slot.gameObject.SetActive(false);

                // 슬롯 오브젝트 활성화
                SetActiveSlot(true);
            }))
            .AppendInterval(1.0f)
            .Append(slot_parent.transform.DOLocalMoveY(270, 1.0f))
            .OnComplete(() =>
            {
                SetBarSlot();
            })
            .AppendInterval(0.5f);
    }

    public Sequence HideDayAnim()
    {
        return DOTween.Sequence()
        .AppendInterval(0.5f)
        .Append(bar_rect.DOSizeDelta(new Vector2(bar_rect.sizeDelta.x, 0.0f), 0.5f))
        .Join(light_rect.DOSizeDelta(new Vector2(light_rect.sizeDelta.x, 0.0f), 0.5f))
        .OnComplete(() =>
        {
            fade_img.DOFade(0.0f, 0.5f);
            callback();
            StartCoroutine(DestroyCo());
        });
    }

    private void DaySlotHide()
    {
        for(int i=0; i< slot_list.Count; i++)
        {
            hide_sequence
            .Join(slot_list[i].fade_img.DOFade(0.0f, 0.5f));
        }
    }

    public void SetCallback(CallbackEvent _callback)
    {
        callback = _callback == null ? () => { } : _callback;
    }

    public void SetAddTime(float add_time)
    {
        // 없음
    }
}
