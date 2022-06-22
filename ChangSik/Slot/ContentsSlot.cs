using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContentsSlot : Slot
{
    [Header("IconField")]
    public Image icon;

    [Header("TextField")]
    public Text name_text;
    public Text category_text;
    public Text level_text;
    public Text price_text;
    public Text popularity_text;
    public Button buy_btn;

    private bool is_btn;
    public Color active_color;
    public Color disabled_color;

    public GameObject TagBoxParent;

    private ChallengeSubject challenge_subject = new ChallengeSubject();

    private ContentsInfo info;

    // Start is called before the first frame update
    void Start()
    {
        SetUI();

        ChallengeBroker.Instance.Init_Subject(challenge_subject);
        buy_btn.onClick.AddListener(OnBuyBtn);
    }

    public override void SetInfo(Info copy)
    {
        if (info == null)
            info = new ContentsInfo();

        info.SetInfo(copy);
    }

    // 스틸브루 = #4682B4
    // 적당한 빨간색 = #990000
    public override void SetUI()
    {
        icon.sprite = info.icon;
        name_text.text = info.name;
        //name_text.text = ColorText(info.name, "#4682B4");

        category_text.text = "카테고리 : " + ColorText(info.GetCategories(), "#00539C");
        level_text.text = "필요 장비 레벨 : " + ColorText(info.pc_level+"레벨", "#00539C");
        price_text.text = "가격 : " + ColorText(GetThousandCommaText(info.price), "#00539C");
        popularity_text.text = "인기도 : " + ColorText(info.GetPopularity(), GetPopularityColor());

        // 현재 컴퓨터의 장비 레벨 비교
        is_btn = DatabaseManager.SearchData("컴퓨터", DatabaseManager.Instance.my_furniture_list).level >= info.pc_level ? true : false;
        SetBtn(is_btn);

        SetTagBoxUI();
    }

    public void SetBtn(bool active)
    {
        buy_btn.interactable = active;

        buy_btn.image.color = active ? active_color : disabled_color;

        buy_btn.GetComponentInChildren<Text>().text = active ? "구매" : "레벨 부족";
    }


    public string GetPopularityColor()
    {
        string color = "";

        if(info.Popularity >= (int)E_CONTENTS_POPULARITY.HIGH)
        {
            color = "#2C5F2D";
        }
        else
        {
            color = "#A4193D";
        }

        return color;
    }

    public void SetTagBoxUI()
    {
        for(int i=0; i < info.con_tag.Length; i++)
        {
            GameObject obj = Instantiate(Resources.Load("Prefabs/UI/TagBoxUI"), new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            obj.transform.SetParent(TagBoxParent.transform, false);
            obj.GetComponentInChildren<Text>().text = info.con_tag[i];
        }
    
    }

    // 컨텐츠 구매
    public void OnBuyBtn()
    {
        if(DatabaseManager.Player.wallet.Money - info.price >= 0)
        {
            AudioManager.Sound.Play("SE/Buying_Sound_Effect", E_SOUND.SE);

            DatabaseManager.Player.wallet.Money = -info.price;
            DatabaseManager.Instance.my_contents_list.Add(new ContentsInfo(info));
            challenge_subject.challenge_handler(ChallengeBroker.Item.Contents, "컨텐츠",1);

            //돈 차감 팝업창
            PricePopup(-info.price);

            // UI 업데이트 하는 옵저버
            NotifyObservers();
        }
        else
        {
            // 돈이 부족할 때 나오는 팝업창
            NotBuyPopup();
        }
    }
}
