using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class WalletData //JsonUtility 사용을 위해 만든 클래스 
{
    public WalletData(Wallet getWallet)
    {
        money = getWallet.Money;
    }
    public int money;
}


public class Wallet : Observer, ISubjectUI // 돈을 관리하는 클래스 
{
    // UI 옵저버들
    protected List<IObserverUI> observer_list;

    private int money; //플레이어가 직접 가지고 있는 돈 

    public ChallengeSubject challenge_subject = new ChallengeSubject();

    public int Money  // 만약 돈을 더한다면 Money =100 을 하고 돈을 감소 시키고 싶으면 Money = -100 을하면된다.
    {
        get
        {
            return money;
        }
        set
        {
            challenge_subject.challenge_handler(ChallengeBroker.Item.Status, "현금", value);
            money = (money + value);
            
            // UI 업데이트
            NotifyObservers();
            //PlayerInfoUI.Instance.money.Money_Update(money);
            //DatabaseManager.Instance.player_info.money.Money_Update(money);
        }
    }

    public string ShowMoney() //화면에 표시될 금액 1000단위를 끊어서 보여준다.
    {
        return string.Format("{0:#,0}", money);
    }


    public void NewGame() // 처음 게임을 시작할떄 
    {
        //50만원으로 시작
        money = 500000;
    }

    public void SaveData() // 스텟을 저장한다. 
    {
        string save = JsonUtility.ToJson(new WalletData(DatabaseManager.Player.wallet), true);
        File.WriteAllText(DatabaseManager.Instance.GetPathObserver(ObserverVariety.WALLET), save);
    }
    public void LodeData() // 스탯을 불러온다.
    {
        string file_name = DatabaseManager.Instance.GetPathObserver(ObserverVariety.WALLET);
        FileInfo file_info = new FileInfo(file_name);

        if (file_info.Exists)
        {
            string load = File.ReadAllText(file_name);
            WalletData get = JsonUtility.FromJson<WalletData>(load);
            money = get.money;
        }
    }
    public override void Notify(Message msg) // 메세지를 받는다.
    {
        switch (msg)
        {
            case Message.NEW:
                NewGame();
                break;
            case Message.SAVE:
                SaveData();
                break;
            case Message.LOAD:
                LodeData();
                break;
            default:
                break;
        }
    }

    #region UI 옵저버 부분

    public void AddObserver(IObserverUI observer)
    {
        if (observer_list == null)
            observer_list = new List<IObserverUI>();

        observer_list.Add(observer);
    }

    public void RemoveObserver(IObserverUI observer)
    {
        if (observer_list == null)
            return;

        observer_list.Remove(observer);
    }

    public void NotifyObservers(string message = "")
    {
        if (observer_list == null)
            return;

        foreach (IObserverUI observer in observer_list)
        {
            observer.ObserverUpdate(message);
        }
    }

    #endregion
}
