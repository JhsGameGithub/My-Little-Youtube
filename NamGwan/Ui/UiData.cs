using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UiData : MonoBehaviour, IObserverUI
{
    public Text PlayerNameText;
    public Text PlayerHealth;
    public Text PlayerAppearance;
    public Text PlayerNarration;
    public Text PlayerRequired;
    public Text PlayerSubscriber;
    public Text PlayerViews;
    public Text PlayerMoney;
    public Text PlayerHP;
    public Image PlayerHPImage;
    #region 오브젝트를 찾기 위해 사용 
    public const string PlayerObjectName = "Name";
    public const string PlayerObjectHealth = "Health";
    public const string PlayerObjectAppearance = "Appearance";
    public const string PlayerObjectRequired = "Required";
    public const string PlayerObjectSubscriber = "Subscriber";
    public const string PlayerObjectViews = "Views";
    public const string PlayerObjectMoney = "PlayerMoneyText";
    public const string PlayerObjectHP = "HPText";
    public const string PlayerObjectHPImage = "HPImage";
    #endregion
    #region 기본 설명 텍스트 
    public const string DefaultTextName = "이름 : ";
    public const string DefaultTextHealth = "건강 : ";
    public const string DefaultTextAppearance = "외모 : ";
    public const string DefaultTextNarration = "화술 : ";
    public const string DefaultTextRequired = "장비 레벨 : ";
    public const string DefaultTextSubscriber = "구독자 수 : ";
    public const string DefaultTextViews = "누적 조회수 : ";

    #endregion

    void Awake() // 오브젝트안에 Text 컴퍼넌트를 가져온다.
    {
        //PlayerNameText = GameObject.Find(PlayerObjectName).GetComponent<Text>();
        //PlayerHealth = GameObject.Find(PlayerObjectHealth).GetComponent<Text>();
        //PlayerAppearance = GameObject.Find(PlayerObjectAppearance).GetComponent<Text>();
        //PlayerRequired = GameObject.Find(PlayerObjectRequired).GetComponent<Text>();
        //PlayerSubscriber = GameObject.Find(PlayerObjectSubscriber).GetComponent<Text>();
        //PlayerViews = GameObject.Find(PlayerObjectViews).GetComponent<Text>();
        //PlayerMoney = GameObject.Find(PlayerObjectMoney).GetComponent<Text>();
        //PlayerHP = GameObject.Find(PlayerObjectHP).GetComponent<Text>();
        //PlayerHPImage = GameObject.Find(PlayerObjectHPImage).GetComponent<Image>();
    }

    private void Start()
    {
        DatabaseManager.Player.status.AddObserver(this);
    }

    private void OnDestroy()
    {
        DatabaseManager.Player.status.RemoveObserver(this);
    }

    public void OnEnable()
    {
        UiStatusUpdate();
        UiRequiredUpdate();
    }

    public void ObserverUpdate(string message = "")
    {
        UiStatusUpdate();

        if (message == "Furniture")
        {
            UiRequiredUpdate();
        }
    }

    public void UiStatusUpdate() //화면에 보여줄스테이스 를 업데이트한다.  main 화면에서 button을 누르면 해당 함수를 호출하면 플레이어에 정보를 받아와서 업데이트를 해준다.
    {

        Status status = DatabaseManager.Player.status;
        PlayerNameText.text = DefaultTextName + status.Name;
        PlayerHealth.text = DefaultTextHealth + (int)status.Health;
        PlayerAppearance.text = DefaultTextAppearance + status.Appearance.ToString("F1");//소수점한자리 까지만 표시 
        PlayerNarration.text = DefaultTextNarration + status.Narration;
        PlayerSubscriber.text = DefaultTextSubscriber + status.Subscriber;
        PlayerViews.text = DefaultTextViews + status.Views;
        //PlayerHP.text = status.MaxHP.ToString() + " / " + status.HP.ToString();
        //PlayerHPImage.fillAmount = (float)status.HP / (float)status.MaxHP;

    }

    public void UiRequiredUpdate() // 화면에 보여줄 장비를 업데이트 
    {
        //PlayerRequired.text = DefaultTextRequired + required.RequiredLevel;

        FurnitureInfo temp = DatabaseManager.SearchData("컴퓨터", DatabaseManager.Instance.my_furniture_list);

        if (temp != null)
        {
            int level = temp.level;

            PlayerRequired.text = DefaultTextRequired + level;
        }

    }

    public void UiWalletUpdate(Wallet wallet) //화면에 보여줄 돈을 업데이트 
    {
        PlayerMoney.text = wallet.ShowMoney();
    }
}
