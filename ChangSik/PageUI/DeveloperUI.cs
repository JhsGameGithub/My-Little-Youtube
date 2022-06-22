using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeveloperUI : MonoBehaviour
{
    public Text moneyText;
    public Text subscriberText;

    public Button addMoneyBtn;
    public Button addSubscriberBtn;
    public Button endingBtn;

    // Start is called before the first frame update
    void Start()
    {
        addMoneyBtn.onClick.AddListener(OnAddMoney);
        addSubscriberBtn.onClick.AddListener(OnAddSubscriber);
        endingBtn.onClick.AddListener(OnEnding);
    }

    private void OnEnable()
    {
        InitUI();
    }

    public void InitUI()
    {
        moneyText.text = string.Format("{0:#,0}", DatabaseManager.Player.wallet.Money);
        subscriberText.text = DatabaseManager.Player.status.Subscriber + "명";
    }

    public void OnAddMoney()
    {
        DatabaseManager.Player.wallet.Money = 100000;
        InitUI();
    }

    public void OnAddSubscriber()
    {
        DatabaseManager.Player.status.Subscriber += 100000;
        InitUI();
    }

    public void OnEnding()
    {

    }
}
