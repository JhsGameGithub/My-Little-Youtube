using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISubject
{
    void Notify(Message msg);
    void AddObserver(Observer observer);
}

public abstract class Observer
{
    public abstract void Notify(Message msg);
}

public enum Message
{
    NEW,  // 새로운 게임 
    SAVE, //저장 
    LOAD, //불러오기 
    POTION, //포션 먹는 이벤트 
}
public enum ObserverVariety
{
    STATUS,
    REQUIRED,
    WALLET,
}
