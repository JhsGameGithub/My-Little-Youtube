using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddContentsSlot : Slot
{
    [Header("IconField")]
    public Image icon;

    [Header("TextField")]
    public Text name_text;
    public Text price_text;
    public Text description_text;

    [Header("BtnField")]
    public Button buy_btn;

    private AddContentsInfo info;

    void Start()
    {
        SetUI();

        buy_btn.onClick.AddListener(OnBuyBtn);
    }

    public override void SetInfo(Info copy)
    {
        if (info == null)
            info = new AddContentsInfo();

        info.SetInfo(copy);
    }

    public override void SetUI()
    {
        icon.sprite = info.icon;
        name_text.text = info.name;
        price_text.text = GetThousandCommaText(info.price);
        description_text.text = info.description + "\n";
        description_text.text += "\n" + info.e_description;
    }

    public void OnBuyBtn()
    {
        if (DatabaseManager.Player.wallet.Money - info.price >= 0)
        {
            AudioManager.Sound.Play("SE/Buying_Sound_Effect", E_SOUND.SE);

            DatabaseManager.Player.wallet.Money = -info.price;
            DatabaseManager.Instance.my_add_contents_list.Add(new AddContentsInfo(info));

            //돈 차감 팝업창
            PricePopup(-info.price);

            // UI 업데이트 하는 옵저버
            NotifyObservers();

            // 데이터 변동에 대한 정보를 옵저버에게 알림
            DatabaseManager.Instance.NotifyObservers("Update");
        }
        else
        {
            // 돈이 부족할 때 나오는 팝업창
            NotBuyPopup();
        }
    }
}
