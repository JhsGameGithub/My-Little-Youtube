using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RecordedVideoInfo : Info
{
    public string title;
    public int year, month, day, hour;
    public ContentsInfo contents;
    public VideoEditorInfo editor;
    public int funny, wait, max_wait;
    public bool is_player = false;

    public string Title
    {
        get { return title; }
        set { title = value; }
    }

    public VideoEditorInfo Editor
    {
        get { return editor; }
        set { editor = value; }
    }

    public RecordedVideoInfo()
    {
        title = "";
        year = 0;
        month = 0;
        day = 0;
        hour = 0;
        funny = 0;
        wait = 0;
        max_wait = 0;
    }
    public RecordedVideoInfo(RecordedVideoInfo copy)
    {
        SetInfo(copy);
    }

    //편집전 녹화영상 추가 - 현성
    public RecordedVideoInfo(string date, ContentsInfo con, int fun)
    {
        SetInfo(date, con, fun);
    }

    //편집중 녹화영상 추가 - 현성
    public RecordedVideoInfo(string title, string date, ContentsInfo con, int fun, int wait, VideoEditorInfo editor)
    {
        SetInfo(title, date, con, fun, wait, editor);
    }

    public override void SetInfo(Info copy)
    {
        RecordedVideoInfo temp = copy as RecordedVideoInfo;
        if (temp != null)
            SetInfo(temp);
        else
            Debug.LogError("�ٿ� ĳ���� ����");
    }

    public void SetInfo(RecordedVideoInfo copy)
    {
        title = copy.title;

        year = copy.year;
        month = copy.month;
        day = copy.day;
        hour = copy.hour;

        contents = copy.contents;

        funny = copy.funny;

        editor = copy.editor;

        wait = copy.wait;
        max_wait = copy.max_wait;

        is_player = copy.is_player;
    }

    public void SetInfo(string date, ContentsInfo contents, int funny)
    {
        title = date + "_" + contents.name;

        string[] temp = date.Split('-');
        year = int.Parse(temp[0]);
        month = int.Parse(temp[1]);
        day = int.Parse(temp[2]);
        hour = int.Parse(temp[3]);

        this.contents = contents;

        this.funny = funny;

        wait = 0;

        editor = null;

        max_wait = 0;
    }

    public void SetInfo(string title, string date, ContentsInfo contents, int funny, int wait, VideoEditorInfo editor)
    {
        this.title = title;
        string[] temp = date.Split('-');
        year = int.Parse(temp[0]);
        month = int.Parse(temp[1]);
        day = int.Parse(temp[2]);
        hour = int.Parse(temp[3]);
        this.contents = contents;
        this.funny = funny;
        this.wait = wait;
        this.editor = editor;
        max_wait = editor.time * 24 * 60;
    }

    //편집하는 시간
    public bool EditingVideo(bool is_player_edit = false)
    {
        wait = is_player ? (!is_player_edit ? wait : wait + 6) : wait + 1;


        return wait >= max_wait ? true : false;
    }
}
