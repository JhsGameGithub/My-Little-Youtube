using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyViewUI : MonoBehaviour, IObserverUI
{
    private Text money;

    private void Start()
    {
        DatabaseManager.Player.wallet.AddObserver(this);
        money = GetComponent<Text>();

        // 시작 했을 때 UI 초기화 해주기 위함
        ObserverUpdate();
    }

    private void OnDestroy()
    {
        DatabaseManager.Player.wallet.RemoveObserver(this);
    }

    public void Money_Update(int value)
    {
        money.text = string.Format("{0:#,0}", value);
    }

    public void ObserverUpdate(string message = "")
    {
        int money = DatabaseManager.Player.wallet.Money;

        Money_Update(money);
    }
}
