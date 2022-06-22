using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameMoney : MonoBehaviour
{
    int money;
    public Text screenText;
    public int gameMoney;
    public int Money
    {
        get
        {       
            return money;
        }
        set
        {


            money = value; 
            
            if (money > GetMaxMoney)
            {
                money = GetMaxMoney;
            }
            if (money < GetMinMoney)
            {
                money = GetMinMoney;
            }
        }
    }
    public int GetMinMoney
    { 
        get
        {
            return 100000;
        }
    }
    public int GetMaxMoney
    {
        get
        {
            return 1000000;
        }
    }
    public void OnEnable()
    {
        NewGame();
    }
    public void NewGame()
    {
        if (transform.parent.GetComponent<SnailGame>().IsGame == true) //게임을 하고 있을떄는 새로운 게임을 세팅하지 않는다.
            return;

        money = GetMinMoney;
        TextUpdate();
    }
    public void UpGameMoney()
    {
        Money += GetMinMoney;
        TextUpdate();
        AudioManager.Sound.Play("SE/snailCoin", E_SOUND.SE);
    }
    public void DownGameMoney()
    {
        Money -= GetMinMoney;
        TextUpdate();
        AudioManager.Sound.Play("SE/snailCoin", E_SOUND.SE);
    }
    public void TextUpdate()
    {
        screenText.text = "$" + money.ToString("#,##0");
    }
    public void GameStart() //게임을 시작한다 
    {
        if(DatabaseManager.Player.wallet.Money - Money >= 0 && transform.parent.GetComponent<SnailGame>().StartGame ==false)
        {
            transform.parent.GetComponent<SnailGame>().GameStart();
            DatabaseManager.Player.wallet.Money = -Money; //돈이 차감 
            gameMoney = Money;
        } 
    }
    public void WinGame()
    {
        DatabaseManager.Player.wallet.Money = gameMoney * 2;
    }
}
