using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : ISubject
{
    List<Observer> handle = new List<Observer>(); //  스탯과 장비를 가지고 있다 해당 클래스들에게 이벤트발생시 메세지를 보내기 위해 사용 

    public Status status; //스탯에 관한 클래스 
    public Required required; //장비에 관한 클래스 
    public Wallet wallet; //돈에 관한 클래스 
    public VideoEditorInfo selfEditor;

    public Player()
    {
        Initialize();
    }

    public void AddObserver(Observer observer) 
    {
        handle.Add(observer);
    }
    public void Notify(Message msg) // 스텟과 장비과 등록되어있으며 해당 클래스들에게 이벤트 메세지를   보낼떄 사용한다.
    {
      for(int i=0; i<handle.Count;i++)
        {
            handle[i].Notify(msg);
        }   
    }
    public void Initialize()
    {
        status = new Status();
        required = new Required();
        wallet = new Wallet();
        selfEditor = new VideoEditorInfo();
        selfEditor.name = "플레이어";

        AddObserver(status);
        AddObserver(required);
        AddObserver(wallet);
        Notify(Message.NEW);//플레이어와 관련된 클래스들에게 새로 시작햇다고 알려준다.
    }

    public void InitSubject()
    {
        ChallengeBroker.Instance.Init_Subject(status.challenge_subject);
        ChallengeBroker.Instance.Init_Subject(wallet.challenge_subject);
    }

    public void UseItem() //플레이어가 포션 먹는 이벤트를 진행한다. 
    {
        Notify(Message.POTION);
    }
}
