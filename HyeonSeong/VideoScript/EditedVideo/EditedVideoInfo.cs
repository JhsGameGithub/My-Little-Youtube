using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EditedVideoInfo : Info
{
    //제목
    public string title;
    //조회수
    public float views;
    //좋아요
    public float goods;
    //업로드 날짜
    public int year, month, day, hour;
    //편집자
    public string editor;
    //유튭각
    public int funny;
    //계산 횟수
    public int repeat;
    //컨텐츠
    public ContentsInfo contents;
    //계산기
    public EditedVideoCalculator calculator;

    public EditedVideoInfo()
    {
        year = 0;
        month = 0;
        day = 0;
        title = "";
        views = 0;
        goods = 0;
        funny = 0;
    }

    public EditedVideoInfo(EditedVideoInfo copy)
    {
        SetInfo(copy);
    }

    //녹화된 영상 -> 편집된 영상
    public EditedVideoInfo(string date, RecordedVideoInfo video)
    {
        SetInfo(date, video);
    }

    //CSV -> 편집된 영상
    public EditedVideoInfo(string title, float views, float goods, string date, string editor, int funny, ContentsInfo contents, int repeat, int subscriber, float popularity, int seed)
    {
        SetInfo(title, views, goods, date, editor, funny, contents, repeat, subscriber, popularity, seed);
    }

    public override void SetInfo(Info copy)
    {
        EditedVideoInfo temp = copy as EditedVideoInfo;
        if (temp != null)
            SetInfo(temp);
        else
            Debug.LogError("다운 캐스팅 실패");
    }

    public void SetInfo(EditedVideoInfo copy)
    {
        year = copy.year;
        month = copy.month;
        day = copy.day;
        hour = copy.hour;
        title = copy.title;
        editor = copy.editor;
        views = copy.views;
        goods = copy.goods;
        contents = copy.contents;
        funny = copy.funny;
        repeat = copy.repeat;
        calculator = copy.calculator;
    }

    //CSV -> 편집된 영상
    public void SetInfo(string title, float views, float goods, string date, string editor, int funny, ContentsInfo contents, int repeat, int subscriber, float popularity, int seed)
    {
        //제목
        this.title = title;
        //조회수
        this.views = views;
        //좋아요
        this.goods = goods;
        //업로드 날짜
        string[] temp = date.Split('-');
        year = int.Parse(temp[0]);
        month = int.Parse(temp[1]);
        day = int.Parse(temp[2]);
        hour = int.Parse(temp[3]);
        //편집자
        this.editor = editor;
        //유튭각
        this.funny = funny;
        //컨텐츠
        this.contents = contents;
        //계산 횟수
        this.repeat = repeat;

        //계산기
        calculator = new EditedVideoCalculator(this,subscriber, this.contents.Popularity, seed);
    }

    //녹화된 영상 -> 편집된 영상
    public void SetInfo(string date, RecordedVideoInfo video)
    {
        //제목
        title = video.title;
        //업로드 날짜
        string[] temp = date.Split('-');
        year = int.Parse(temp[0]);
        month = int.Parse(temp[1]);
        day = int.Parse(temp[2]);
        hour = int.Parse(temp[3]);
        //편집자
        editor = video.editor.name;
        //컨텐츠
        contents = video.contents;
        //유튭각
        funny = video.funny;
        //계산 횟수
        repeat = 0;
        //계산기
        calculator = new EditedVideoCalculator(this, DatabaseManager.Player.status.Subscriber, contents.Popularity, Random.Range(4000, 12001));
    }


    //조회수, 좋아요, 구독자, 자금 상승 _ 현성
    public bool YoutubeProfit( )
    {
        if (++repeat != 43200)
        {
            int temp_views = calculator.GetOnceViews();
            views += temp_views;
            goods += calculator.GetOnceGoods();
            DatabaseManager.Player.status.Views += temp_views;
            DatabaseManager.Player.status.Subscriber += calculator.GetOnceSubscriber();
            DatabaseManager.Player.wallet.Money = calculator.GetOnceMoney();
            return false;
        }
        return true;
    }
}
