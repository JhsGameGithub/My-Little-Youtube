using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditedVideoSlot : Slot, IProfitingVideo
{
    [Header("TextField")]
    public Text title_text;
    public Text views_text;
    public Text goods_text;
    public Text date_text;
    public Text editor_text;

    [Header("FunnyField")]
    public int funny_int;
    public Sprite blanked_star_spr;
    public Image[] funny_image = new Image[5];

    public EditedVideoUI video_ui;

    private EditedVideoInfo info;

    public EditedVideoInfo Info
    {
        get { return info; }
    }
    private void Start()
    {
        SetUI();
    }

    public override void SetInfo(Info copy)
    {
        if (info == null)
            info = new EditedVideoInfo();
        info.SetInfo(copy);
    }

    public void SetInfo(EditedVideoInfo copy)
    {
        info = copy;
    }

    public override void SetUI()
    {
        title_text.text = "제목 : " + info.title;
        views_text.text = "조회수 : " + info.views;
        goods_text.text = "좋아요 : " + info.goods;
        date_text.text = "업로드 : " +
            info.year + "년 " +
            info.month + "월 " +
            info.day + "일 " +
            info.hour + "시간";
        editor_text.text = "편집자 : " + info.editor;
        SetFunnyUI();
    }

    public void SetFunnyUI()
    {
        funny_int = info.funny;
        for (int i = funny_int; i < 5; i++)
        {
            funny_image[i].sprite = blanked_star_spr;
        }
    }
    //영상으로 이익 내기
    public bool ProfitingVideo()
    {
        bool temp = info.YoutubeProfit();
        views_text.text = "조회수 : " + info.views;
        goods_text.text = "좋아요 : " + info.goods;
        return temp;
    }

    //이익 내고있던 영상 불러오기
    public void ProfitingLoad()
    {
        views_text.text = "조회수 : " + info.views;
        goods_text.text = "좋아요 : " + info.goods;
        video_ui.profiting_video_slots.Enqueue(this);
    }
}
