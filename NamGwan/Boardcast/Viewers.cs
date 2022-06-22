using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Viewers : MonoBehaviour
{
    public List<People> people; //방송을 시청하고 있는 사람들 
    public List<People> waitRoom; //대기실로 방송을 시작할때 대기실에 있는 사람들중 방송 컨텐츠에 흥미가 있는 사람만 입장한다.
    const bool SUBSCRIBER = true;
    public ChallengeSubject challenge_subject=new ChallengeSubject();
    int highest_viewer = 0;

    private void Start()
    {
        ChallengeBroker.Instance.Init_Subject(challenge_subject);
    }
    public void Init()
    {
        people = new List<People>();
        waitRoom = new List<People>();
     //   JoinSubscriber(); 해당 함수는 사용하지 않는데 일단 보류함 
        JoinBeauty();
    }
    public void JoinPeople()
    {
        int add = DatabaseManager.Player.status.Subscriber / 10000;
        //Debug.Log("add = " + add);
        PushPeople(1 + add); //대기실에 사람을 넣는다. 해당 매개변수에 들어가는 값이 커질수록 방송을 보는 시청자가 증가할 확률이 높다.
        PeopleAction();
        WaitRoomAction();
    }
    public void PeopleAction()
    {
        for (int i = people.Count - 1; i >= 0; i--)
        {
            people[i].Action();
        }
    }
    public void WaitRoomAction()
    {
        for(int i= waitRoom.Count-1;i >= 0;i--)
        {
            waitRoom[i].Action();
        }
     
    }
    public void PushPeople(int count) //대기실에 사람들을 넣는 함수이다.
    {
        for(int i=0;i<count;i++ )
        {
            waitRoom.Add(new People());
        }
    }
    public void Attention(People waitPeople) //관심을 가졌을때 방에 입장 
    {
        people.Add(new People(waitPeople));
        waitRoom.Remove(waitPeople);
        if (highest_viewer < people.Count)
        {
            challenge_subject.challenge_handler(ChallengeBroker.Item.Status, "시청자", people.Count - highest_viewer);
            highest_viewer = people.Count;
        }
    }
    public void OutPeople(People outPeople)
    {
        people.Remove(outPeople);
    }
   
    
    public void JoinSubscriber()//구독자의 1/3 은 방송을 시작하자마자 입장한다. 
    {
        for (int sub=0;sub<  DatabaseManager.Player.status.Subscriber/3;sub++ ) 
        {
            people.Add(new People(SUBSCRIBER));
        }
    }
    public void JoinBeauty() //외모 수치에 따라 입장하는 사람수가 많아짐 
    {
        if(DatabaseManager.Instance.my_add_contents_list.Find(x => x.itemType == FlexItemList.CAMERA) != null)
        {
            for (int beauty = 0; beauty < DatabaseManager.Player.status.Appearance * 25; beauty++) // 입장시 외무 수치만큼 시청자수를 일시적으로 늘려줌 
            {
                people.Add(new People(SUBSCRIBER));
            }
        }       
    }
    public void ViewersEvent(int per, bool isAdd)
    {
        if (people == null)
            return;

        int result = (people.Count * per) / 100;

        if (isAdd)
        {
           
            for(int i=0; i< result; i++)
            {
              people.Add(new People(SUBSCRIBER));
            }
        }
        else
        {
           
            for(int i=0; i< result;i++)
            {
                if (people.Count <= 0)
                {
                    return;
                }
                people.RemoveAt(0);
            }
        }
    }
    public void ShowCount()
    {
        BoardcastManager.Instance.peopleCount.text = "시청자 : "+people.Count;
    }
   
}
