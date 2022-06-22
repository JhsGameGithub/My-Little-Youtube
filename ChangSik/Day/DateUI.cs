using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DateUI : MonoBehaviour, IObserverUI
{
    public Text year_text;
    public Text month_text;
    public Text date_text;
    public Text day_text;
    public Text timeline_text;
    public Text d_day;

    private DateTime current_date;

    // Start is called before the first frame update
    void Start()
    {
        Clock.Instance.AddObserver(this);
        SetDateUI();
    }

    private void OnDestroy()
    {
        Clock.Instance.RemoveObserver(this);
    }

    public void ObserverUpdate(string message)
    {
        SetDateUI();
    }

    public void SetDateUI()
    {
        current_date = DatabaseManager.Current_date;
        d_day.text = "D + " + DatabaseManager.Instance.D_day;

        string color = GetColor(current_date.DayOfWeek);

        year_text.text = current_date.Year.ToString();
        month_text.text = current_date.Month + " /";
        date_text.text = ColorText(current_date.Day.ToString(), color);
        day_text.text = ColorText(GetDayOfWeekText(current_date.DayOfWeek), color);
        timeline_text.text = GetTimeLineText(current_date.Hour);
    }

    public string GetTimeLineText(int hour)
    {
        if ((int)E_TimeLine.Dawn > hour)
        {
            return "새벽";
        }
        else if ((int)E_TimeLine.Morningt > hour)
        {
            return "아침";
        }
        else if ((int)E_TimeLine.Afternoon > hour)
        {
            return "낮";
        }
        else if ((int)E_TimeLine.Evening > hour)
        {
            return "저녁";
        }
        else if ((int)E_TimeLine.Night > hour)
        {
            return "밤";
        }

        return "";
    }

    public string GetDayOfWeekText(DayOfWeek _type)
    {
        string day_text = "";

        switch (_type)
        {
            case DayOfWeek.Monday:
                day_text = "월요일";
                break;
            case DayOfWeek.Tuesday:
                day_text = "화요일";
                break;
            case DayOfWeek.Wednesday:
                day_text = "수요일";
                break;
            case DayOfWeek.Thursday:
                day_text = "목요일";
                break;
            case DayOfWeek.Friday:
                day_text = "금요일";
                break;
            case DayOfWeek.Saturday:
                day_text = "토요일";
                break;
            case DayOfWeek.Sunday:
                day_text = "일요일";
                break;
        }

        return day_text;
    }

    private string GetColor(DayOfWeek _type)
    {
        if (_type == DayOfWeek.Saturday)
        {
            return "#3399FF";
        }
        else if (_type == DayOfWeek.Sunday)
        {
            return "#FF0000";
        }

        return "#FFFFFF";
    }

    private string ColorText(string data, string color)
    {
        return "<color=" + color + ">" + data + "</color>";
    }
}
