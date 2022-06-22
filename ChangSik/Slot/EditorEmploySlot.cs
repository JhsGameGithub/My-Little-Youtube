using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditorEmploySlot : Slot
{
    [Header("TextField")]
    public Text name_text;
    public Text ability_text;
    public Text time_text;
    public Text price_text;
    public Button buy_btn;

    public Color active_color;
    public Color disabled_color;

    private VideoEditorInfo info;
    private ChallengeSubject challenge_subject = new ChallengeSubject();

    void Start()
    {
        SetUI();

        ChallengeBroker.Instance.Init_Subject(challenge_subject);

        buy_btn.onClick.AddListener(OnBuyBtn);
    }

    public override void SetInfo(Info copy)
    {
        if (info == null)
            info = new VideoEditorInfo();

        info.SetInfo(copy);
    }

    public override void SetUI()
    {
        name_text.text = "이름 : " + info.name;
        ability_text.text = "편집 능력 : " + info.ability;
        time_text.text = "영상 완성까지 : " + info.time + "일";
        price_text.text = "주급 : " + info.price;

        SetBtn(info.is_employ);
    }

    public void SetBtn(bool active)
    {
        buy_btn.image.color = !active ? active_color : disabled_color;

        buy_btn.GetComponentInChildren<Text>().text = !active ? "고용" : "해고";
    }

    public void OnBuyBtn()
    {
        if (info.is_employ)
        {
 
            foreach (VideoEditorInfo removed in DatabaseManager.Instance.my_editor_list)
            {
                if (removed.name == info.name)
                {

                    //생성자의 매개변수로 팝업의 하이라키 위치 설정 -> 최상단 canvase
                    PopupBuilder popupBuilder = new PopupBuilder(GameObject.Find("PopupCanvas").transform);
                    popupBuilder.SetTitle("정말 해고 할거야???");
                    popupBuilder.SetDescription("해당 편집자가 작업중인 영상은 초기화됨!!");

                    popupBuilder.SetButton("해고", () =>
                    {
                        if (DatabaseManager.Instance.my_editor_list.Remove(removed))
                        {
                            info.is_employ = false;
                            SetUI();
                            NotifyObservers("not_slot_init");
                        }
                    });

                    challenge_subject.challenge_handler(ChallengeBroker.Item.Editor, "편집자",-1);
                    popupBuilder.SetButton("취소");
                    popupBuilder.Build("BasePopupUI");     

                    break;
                }
            }
        }
        else if (DatabaseManager.Player.wallet.Money - info.price >= 0)
        {
            AudioManager.Sound.Play("SE/Buying_Sound_Effect", E_SOUND.SE);
            DatabaseManager.Player.wallet.Money = -info.price;

            //돈 차감 팝업창
            PricePopup(-info.price);

            info.is_employ = true;
            SetUI();

            DatabaseManager.Instance.my_editor_list.Add(info);
            challenge_subject.challenge_handler(ChallengeBroker.Item.Editor, "편집자", 1);

            NotifyObservers("not_slot_init");
        }
        else
        {
            // 돈이 부족할 때 나오는 팝업창
            NotBuyPopup();
        }


        //드롭다운 목록 업데이트 _ 현성
        VideoManager.Instance.dropdown_manager.NotifyObservers();
    }
}
