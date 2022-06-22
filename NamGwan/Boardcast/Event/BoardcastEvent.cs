using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public enum BoardcastEventEnumType
{
    POSITIVE=0,//긍정이벤트
    NEGATIVE,//부정이벤트
    NOTHING, //아무일도일어나지않음
    RECORDING, // 녹화본 호출 
    DEFAULT,
}

public class BoardcastEvent : MonoBehaviour
{
    public int eventCount;

    public BoardcastEventList handle;
    public Queue<int> donaiton;
    public Stack<GameObject> donaObj;
    public Stack<GameObject> eventObj;
    const string PATH = "Prefabs/Boardcast/Event/";
    public bool haveRecord; //녹화가 되었는가 ? 
    private void Start()
    {
        eventCount = 1;

        handle = new BoardcastEventList();
        donaiton = new Queue<int>();
        donaObj = new Stack<GameObject>();
        eventObj = new Stack<GameObject>();
        StartCoroutine(SpawnDonation(1.0f));//도네이션 나오는 딜레이
    }
    public void OnAir()
    {
        handle.AddEvent(PATH + "BigMoneyObject", BoardcastEventEnumType.POSITIVE);
        handle.AddEvent(PATH + "HealingObject", BoardcastEventEnumType.POSITIVE);
        handle.AddEvent(PATH + "HostingObject", BoardcastEventEnumType.POSITIVE);
        handle.AddEvent(PATH + "OhbangObject", BoardcastEventEnumType.POSITIVE);

        handle.AddEvent(PATH + "OverdoObject", BoardcastEventEnumType.NEGATIVE);
        handle.AddEvent(PATH + "SayNameObject", BoardcastEventEnumType.NEGATIVE);
        handle.AddEvent(PATH + "SniperObject", BoardcastEventEnumType.NEGATIVE);
        
        
        handle.AddEvent(PATH + "ClipObject", BoardcastEventEnumType.NOTHING);
        handle.AddEvent(PATH + "FunnyObject", BoardcastEventEnumType.NOTHING);
        handle.AddEvent(PATH + "PeaceObject", BoardcastEventEnumType.NOTHING);

        handle.AddEvent(PATH + "RecordEventObject", BoardcastEventEnumType.RECORDING);
    }
    public void OutBoardcast() //방송이 끝났을떄 
    {
        handle.ClearEvent();
        donaiton.Clear();

       DOTween.KillAll();
       StackDelete(eventObj);
       StackDelete(donaObj);
          eventCount = 1;
        haveRecord = false;
    }
    public void StackDelete(Stack<GameObject> temp)
    {
        while (temp.Count > 0)
        {
            GameObject t = temp.Pop();
            if (t == null)
            {
                donaObj.Clear();
            }
            else
            {
                Destroy(t);
            }
        }
    }
    public void DonationEvent(int money) //도네이션 이벤트를 호출한다.
    {
        donaiton.Enqueue(money);
    }
    IEnumerator SpawnDonation(float delay) //일정시간마다 도네이션 출력 
    {
        yield return new WaitForSeconds(delay);
        DonatoinSpawn();
        StartCoroutine(SpawnDonation(delay));
    }
    public void DonatoinSpawn() //도네이션이 있으면 출력 
    {
        if (donaiton.Count == 0 || BoardcastManager.Instance.canBoardcast==false || Clock.Instance.GetTimeStop())
            return;
        int get = donaiton.Dequeue();
        GameObject obj = Instantiate(Resources.Load("Prefabs/UI/Broadcast/DonaitonObject"), new Vector3(Random.Range(-200, 200), 400, 0), Quaternion.identity) as GameObject;
        obj.transform.SetParent(GameObject.Find("EventCanvas").transform, false);
        obj.GetComponent<Donaiton>().SetMoney(get);
        donaObj.Push(obj);
    }
    public void Spawn(string path) //이벤트 호출 
    {
        GameObject obj = Instantiate(Resources.Load(path), new Vector3(0, 0, -500), Quaternion.identity) as GameObject;
        obj.transform.SetParent(GameObject.Find("InGameCanvas").transform, false);
        eventObj.Push(obj);
    }

    public void RandomEvent(int time)
    {
        if (CanEvent(time,120) == false) //120 분에 한번씩 이벤트 호출 
            return;
        else
            eventCount++;
       
        handle.ActionEvent((BoardcastEventEnumType)Random.Range(0, (int)BoardcastEventEnumType.RECORDING)); //등록되어 있는 랜덤 이벤트 발생          
     }

    public void RecordEvent()
    {
        if (haveRecord == true) //이미 한번 녹화가 되었으면 탈출 
            return;
        if (BoardcastManager.Instance.boardcastTime > 140) //140초가 지나면 녹화본이 되었다고 나옴 
        {
            haveRecord = true;
            handle.ActionEvent(BoardcastEventEnumType.RECORDING);
        }
    }

    private bool CanEvent(int time, int HowTime) 
    {
        return eventCount * HowTime < time ? true : false;
    }
    
}