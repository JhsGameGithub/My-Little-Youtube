using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DateSlot : MonoBehaviour
{
    public Text day;
    public Text date;

    public CanvasGroup fade_img;
    private DateTime current_date;

    public DateTime Current_date { get => current_date; set => current_date = value; }

    public void Init(DateTime _datetime)
    {
        Current_date = _datetime;
    }

    public void SetUI()
    {
        string color = GetColor(Current_date.DayOfWeek);

        day.text = ColorText(GetDayOfWeekText(Current_date.DayOfWeek), color);
        date.text = ColorText(Current_date.Day.ToString(), color);
    }

    public string GetDayOfWeekText(DayOfWeek _type)
    {
        string day_text = "";

        switch (_type)
        {
            case DayOfWeek.Monday:
                day_text = "MON";
                break;
            case DayOfWeek.Tuesday:
                day_text = "TUE";
                break;
            case DayOfWeek.Wednesday:
                day_text = "WED";
                break;
            case DayOfWeek.Thursday:
                day_text = "THU";
                break;
            case DayOfWeek.Friday:
                day_text = "FRI";
                break;
            case DayOfWeek.Saturday:
                day_text = "SAT";
                break;
            case DayOfWeek.Sunday:
                day_text = "SUN";
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

        return "#373737";
    }

    private string ColorText(string data, string color)
    {
        return "<color=" + color + ">" + data + "</color>";
    }
}
