using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public enum PeopleClass
{
    SUDRA,
    BAISHA,
    KSHATRITA,
    BRAHMIN
}
public class BoardcastManager : MonoBehaviour
{
    #region Singleton
    private static BoardcastManager instance = null;
    public static BoardcastManager Instance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }
            return instance;
        }
    }
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            instance.Initalize();
            Destroy(this.gameObject);
        }
    }
    #endregion
    public void Start()
    {
        Initalize();
        ChallengeBroker.Instance.Init_Subject(challenge_subject);
    }
    public void Initalize()
    {
        peopleCount = GameObject.Find("InGameCanvas").transform.Find("SteamingPlayerPage").transform.Find("Viewers").GetChild(0).GetComponent<Text>();
        viewers = GetComponent<Viewers>();
        eventListener = GetComponent<BoardcastEvent>();//이벤트를 관리해줄 클래스 
        GameObject obj = Instantiate(Resources.Load("Prefabs/Contents/Data"), new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        data = obj.GetComponent<ContentsEnum>(); //프리팹 데이터 가져온다.
        data.Setting();
        news = GameObject.Find("MainCanvas").transform.Find("NewsPage").GetComponent<NewsTag>();
    }
    #region 녹화본 확률 
    const float ONE = 10;
    const float TWO = 20;
    const float THREE = 40;
    const float FOUR = 20;
    const float FIVE = 10;
    #endregion
    #region 사람역활부여  

    const float Sudra = 50; //인도의 카스트제도 밑에서부터 수드라, 바이샤 ,크샤트리아, 브라만 채용 사람들의 역할을 정하고 역할에 맞춰 돈을 설정할떄 사용  
    const float Baisha = 40;
    const float Kshatriya = 9;
    const float Brahmin = 1;
    #endregion

    public BoardcastEvent eventListener; //이벤트를 관리해줄 클래스 도네이션 , 시간마다 등장하는 이벤트를 실행한다.
    public Viewers viewers;//방송중 시청자를 관리해줄 객체 
    public string title; //현재 방송중인 제목  ChangeData 함수의 ValueChange 이벤트 리스너를 통해 가져옴  
    public ContentsInfo boardcastContens; //현재 방송중인 컨텐츠
    public Text peopleCount; //시청자수 텍스트 
    public ContentsEnum data;
    public NewsTag news; //뉴스에 관련된 태그들 
    public int boardcastTime; //방송을 진행한 시간 
    public int recordingCount; //녹화본 생성 1~5 사이로 방송이 끝나면 결정된다.
    public bool canBoardcast; //방송을 껏는지 확인용도 
    public const int MinBoardcastTime = 120; //최소 방송시간  
    private ChallengeSubject challenge_subject = new ChallengeSubject();
    public RecordedVideoUI recorded_video_UI;
    public void OnBoardcast() //방송이 시작했을떄 
    {
        eventListener.OnAir(); //진행중 이벤트 넣어주기 
        boardcastTime = 0;
        viewers.Init();
        GetRecordingStar();
        canBoardcast = true;
    }
    string GetTime()
    {
        string getTime = DatabaseManager.Current_date.Year + "-" +
        DatabaseManager.Current_date.Month + "-" +
        DatabaseManager.Current_date.Day + "-" +
        DatabaseManager.Current_date.Hour;
        return getTime;
    }
    public void OutBoardcast() //방송이 꺼졌을떄 해당 함수가 실행된다.
    {
        if (boardcastTime >= MinBoardcastTime && DatabaseManager.Player.status.HP > 0)
        {
            //Debug.Log(recorded_video_UI);
            recorded_video_UI.Video_Insert(new RecordedVideoInfo(GetTime(), boardcastContens, recordingCount));//방송이끝나고 녹화본 데이터 넘기기
            challenge_subject.challenge_handler(ChallengeBroker.Item.Video, "첫영상", 1);
        }  
        boardcastTime = 0; //방송시간 초기화 
        eventListener.OutBoardcast(); //진행중 이벤트 뺴주기 
    }
    //확률 
    int Choose(float[] probs)
    {
        float total = 0;

        foreach (float elem in probs)
        {
            total += elem;
        }

        float randomPoint = Random.value * total;

        for (int i = 0; i < probs.Length; i++)
        {
            if (randomPoint < probs[i])
            {
                return i;
            }
            else
            {
                randomPoint -= probs[i];
            }
        }
        return probs.Length - 1;
    }
    public void RecordingEvent(int value, bool isPositive)
    {
        if(isPositive) //긍정이벤트일떄 
        {
            recordingCount += value;
            if (recordingCount > 5)
                recordingCount = 5;
        }
        else //부정이벤트일떄 
        {
            recordingCount -= value;
            if (recordingCount <= 0)
                recordingCount = 1;
        }
    }
    public void GetRecordingStar() // 방송이 시작되면 해당 함수가 실행되고 몇성짜리 방송인지 미리 결정해놓는다.
    {
        float[] star = { ONE, TWO, THREE, FOUR, FIVE };
        recordingCount = Choose(star)+1;
    }
    public PeopleClass GetPeopleClass()
    {
        float[] pclass = { Sudra, Baisha, Kshatriya, Brahmin};
        return (PeopleClass)Choose(pclass);
    }
    public void EndButtonClick() //방송이 끝나면 나오는 이벤트 
    {
        if (canBoardcast == false) //방송이 꺼졌는가 ? 
            return;

        canBoardcast = false;

        if (boardcastTime>= MinBoardcastTime && DatabaseManager.Player.status.HP > 0)//방송시간이 2시간 이상되었고 체력이 0보다 클떄 방송종료버튼을 눌렀을경우 
        {
            if (GameObject.Find("InGameCanvas").transform.Find("Wall").GetComponent<SpriteRenderer>().sprite.name.Contains("New") == true)//벽지가 있을떄 별갯수 +1 
            {
                recordingCount += 1;
                if (recordingCount > 5)
                    recordingCount = 5; 
            }
            AudioManager.Sound.OverridePlay("SE/RecordSound", E_SOUND.SE);
            
            // 시간 정지
            Clock.Instance.TimeStopper();

            eventListener.Spawn("Prefabs/Boardcast/Event/EndBoardcastObject"); //방송 종료 이벤트 실행 
        }
        else
        {
            GameObject.Find("InGameCanvas").GetComponent<StateMachine>().ChangeState("RestPlayerPage"); //왜여기서 OutBoardcast 실행하냐..
        }
    }
    public void OnAir() //방송중일떄 실행 StreamingState 에서 StateUpdate함수를 실행할떄 해당함수를 호출하며 방송에 관한 이벤트를 관리하는 함수이다.
    {
        if (canBoardcast == false)
            return;

        viewers.JoinPeople(); //사람들이 입장한다.
        viewers.ShowCount();//시청자수를 갱신한다.
        TimeEvent();
    }
    public void TimeEvent()//시간이 경과하면서 나오는 이벤트들 
    {
        boardcastTime++;
        eventListener.RandomEvent(boardcastTime);//시간이 경과할떄마다 이벤트를 발생한다.
        eventListener.RecordEvent();
    }

}
