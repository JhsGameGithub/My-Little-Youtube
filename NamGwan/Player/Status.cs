using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class StatusData //JsonUtility 사용을 위해 만든 클래스 
{
    public StatusData(Status status)
    {
        playerName = status.Name;
        health = status.Health;
        appearance = status.Appearance;
        narration = status.Narration;
        subscriber = status.Subscriber;
        views = status.Views;
        currentHp = status.HP;
    }
    public string playerName; // 플레이어 이름 
    public float health; // 건강 
    public float appearance; // 외모 
    public int narration; //화술 
    public int subscriber; //구독자
    public int views; // 조회수 
    public int currentHp; //현재 체력
}


public class Status : Observer, ISubjectUI //  스텟과 관련된 클래스 
{

    #region 스탯 프로퍼티
    private string playerName; // 플레이어 이름 
    private float health; // 건강 
    private float appearance; // 외모 
    private int narration; //화술 
    private int subscriber; //구독자
    private int views; // 조회수 
    private int currentHp; //현재 체력
    private int maxHp; //최대 HP
    public ChallengeSubject challenge_subject = new ChallengeSubject();

    public bool havePaint; //액자아이템을 가지고있는가 
    // UI 옵저버들
    protected List<IObserverUI> observer_list;

    //처음 가지고 시작하는 HP
    private const int BasicHp = 59;

    //GetSet
    public string Name
    {
        get
        {
            return playerName;
        }
        set => playerName = value;
    }
    public float Health
    {
        get
        {
            return health;
        }
        set
        {
            challenge_subject.challenge_handler(ChallengeBroker.Item.Status, "건강", (int)(value - health) < 0 ? 0 : (int)(value - health));
            health = value;

            if (health < 0)
                health = 0;
            // UI 업데이트
            NotifyObservers();
        }
    }
    public float Appearance
    {
        get
        {
            return appearance;
        }
        set
        {
            appearance = value;
            if (appearance > 20f) //외모 최대치는 20 이다 .
                appearance = 20f;
        }
    }
    public int Narration
    {
        get
        {
            return narration;
        }
        set => narration = value;
    }
    
    public int Subscriber
    {
        get
        {
            return subscriber;
        }
        set
        {
            challenge_subject.challenge_handler(ChallengeBroker.Item.Status, "구독자", value - subscriber);
            subscriber = value;
        }
    }
    public int Views
    {
        get
        {
            return views;
        }
        set
        {
            challenge_subject.challenge_handler(ChallengeBroker.Item.Status, "조회수", value - views);
            views = value;
        }
    }
    public int MaxHP
    {
        get
        {
            return maxHp = BasicHp + (int)health + (havePaint ? 10 : 0);
        }
    }

    public int HP // hp를 감소시키고 싶을떄 HP = -1 하면 된다. 반대의 경우 HP = 1 하면된다.
    {
        get
        {
            return currentHp;
        }
        set
        {
            currentHp = (currentHp + value);
            LimitHP();

            // UI 업데이트
            NotifyObservers();

            //PlayerInfoUI.Instance.hp.Hp_Update(currentHp , MaxHP);
            //DatabaseManager.Instance.player_info.hp.Hp_Update(currentHp / maxHp);
        }

    }

    #endregion
    public void LimitHP()
    {
        if (currentHp > MaxHP)
        {
            currentHp = MaxHP;
        }

        if (currentHp < 0)
        {
            if(BoardcastManager.Instance.canBoardcast==true)
            {
                BoardcastManager.Instance.OutBoardcast();
            }

            Sleep();
        }
    }
    public void Sleep() // 체력이 0이되면 해당 함수  호출 
    {
        currentHp = 0;

        AudioManager.Sound.Play("SE/gameover_1", E_SOUND.SE);

        // 시간 정지
        Clock.Instance.TimeStopper();

        //생성자의 매개변수로 팝업의 하이라키 위치 설정 -> 최상단 canvase
        PopupBuilder popupBuilder = new PopupBuilder(GameObject.Find("PopupCanvas").transform);
        popupBuilder.SetTitle("강제로 수면에 들어갑니다.");
        popupBuilder.SetDescription("(12시간 경과)");
        popupBuilder.SetAnim("FadeIn", "FadeOut", ()=> 
        {
            Clock.Instance.state_machine.ChangeState("SleepPlayerPage");
        });
        popupBuilder.SetAutoDestroy(true);
        popupBuilder.Build("SleepPopupUI");
    }


    public void SaveData() // 스텟을 저장한다. 
    {
        string save = JsonUtility.ToJson(new StatusData(DatabaseManager.Player.status), true);
        File.WriteAllText(DatabaseManager.Instance.GetPathObserver(ObserverVariety.STATUS), save);
    }
    public void LodeData() // 스탯을 불러온다.
    {
        string file_name = DatabaseManager.Instance.GetPathObserver(ObserverVariety.STATUS);
        FileInfo file_info = new FileInfo(file_name);

        // 파일 존재 여부 확인
        if (file_info.Exists)
        {
            string load = File.ReadAllText(file_name);
            InsertData(JsonUtility.FromJson<StatusData>(load));
        }
    }
    private void InsertData(StatusData data) //파일을 불러와서 데이터를 덮어씌운다.
    {
        playerName = data.playerName;
        health = data.health;
        appearance = data.appearance;
        narration = data.narration;
        subscriber = data.subscriber;
        views = data.views;
        currentHp = data.currentHp;
    }
    public void NewGame() // 처음 게임을 시작할떄 
    {
        playerName = "";
        health = 1;
        appearance = 3;
        narration = 0;
        subscriber = 0;
        views = 0;
        currentHp = maxHp = BasicHp + (int)health;
    }

    public void PotionEvent() //포션 먹기 이벤트시 
    {
        HP = 20;
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
            case Message.POTION:
                PotionEvent();
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
