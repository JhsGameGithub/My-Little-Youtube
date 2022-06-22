using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FurnitureSlot : Slot
{
    [Header("IconField")]
    public Image icon;

    [Header("TextField")]
    public Text name_text;
    public Text level_text;
    public Text price_text;
    public Text info_text;
    public Button buy_btn;

    private FurnitureInfo info;
    private string message = "";

    private ChallengeSubject challenge_subject = new ChallengeSubject();

    private bool is_btn;
    public Color active_color;
    public Color disabled_color;

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
            info = new FurnitureInfo();

        info.SetInfo(copy);
    }

    public override void SetUI()
    {
        icon.sprite = info.icon;
        name_text.text = info.name;// ColorText(info.name, "#4682B4");
        info_text.text = info.info_text;// ColorText(info.info_text, "#990000");


        level_text.text = "현재 레벨 : " + ((info.name != "포션" && info.name != "페이스 크림") ? info.level + "레벨" : "Max");// ColorText((info.name != "포션") && (info.name != "페이스 크림")  ? info.level + "레벨" : "Max", "#990000");


        price_text.text = ((info.name != "포션") && (info.name != "페이스 크림") ? "강화 비용: " : "구매 비용: ") + GetThousandCommaText(info.price); //ColorText(GetThousandCommaText(info.price), "#990000");

        if (info.name == "포션" || info.name == "페이스 크림")
        {
            SetBtn(info.level == 0);
        }
    }

    public void SetBtn(bool active)
    {
        buy_btn.interactable = active;

        buy_btn.image.color = active ? active_color : disabled_color;

        buy_btn.GetComponentInChildren<Text>().text = active ? "구매" : "구매 불가";
    }

    public void OnBuyBtn()
    {
        if (DatabaseManager.Player.wallet.Money - info.price >= 0)
        {
            AudioManager.Sound.Play("SE/Black", E_SOUND.SE);

            DatabaseManager.Player.wallet.Money = -info.price;

            //돈 차감 팝업창
            PricePopup(-info.price);

            if ((info.name == "포션") || (info.name == "페이스 크림"))
            {
                OnPotion();
            }
            else
            {
                OnUpgrade();
            }
            challenge_subject.challenge_handler(ChallengeBroker.Item.Furniture, info.name == "포션" || info.name == "침대" ? info.name : "장비", 1);

            NotifyObservers(message);
        }
        else
        {
            // 돈이 부족할 때 나오는 팝업창
            NotBuyPopup();
        }
    }

    public void OnUpgrade()
    {
        FurnitureInfo temp = DatabaseManager.SearchData(info.name, DatabaseManager.Instance.my_furniture_list);
        FurnitureInfo temp_upgrade = DatabaseManager.Instance.SearchFurnitureData(info.name, info.level + 1);

        //temp.SetInfo(info);
        message = "";

        if (temp_upgrade != null)
        {
            temp.SetInfo(temp_upgrade);
            SetInfo(temp_upgrade);
            SetUI();

            if (temp_upgrade.level < DatabaseManager.Instance.GetFurnitureMaxLevel(info.name))
            {
                message = "not_slot_init";
            }
        }
    }

    private void OnPotion()
    {
        // 포션 구매에 대한 처리 (Level 비교..)
        info.level++;

        // 나의 포션 데이터 수정
        FurnitureInfo temp = DatabaseManager.SearchData(info.name, DatabaseManager.Instance.my_furniture_list);
        temp.SetInfo(info);

        if (info.name == "포션")
        {
            // 플레이어 체력 회복
            DatabaseManager.Player.status.HP = 20;
        }
        else if (info.name == "페이스 크림")
        {
            DatabaseManager.Player.status.Appearance += 1;
        }

        // UI 업데이트
        SetUI();

        // 구매에 대한 처리는 없음
        message = "not_slot_init";

    }
}
