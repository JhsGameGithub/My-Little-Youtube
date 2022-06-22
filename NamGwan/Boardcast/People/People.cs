using UnityEngine;

public enum State
{
    WAIT, //방에 입장전 상태  
    BOARDCAST, // 방에 입장한 상태 
    CHAT, //채팅 혹은 도네이션을 하는 상태
    OUT, //방에서 퇴장한 상태 
}

public class People 
{
   private State state;

    #region 변수 
    PeopleClass pc;
    public string favoriteGame;//좋아하는 게임 
    public string favoriteTag; //좋아하는 카테고리 
    public int remainingTime; //방송을 시청할수 있는 시간이다. 시간이 다줄어들면 시청자는 나간다. 피시방에 남은 시간을 생각하면된다.
    public float interest; // 흥미도로 흥미도를 바탕으로 시청을 할것인지 정한다.
    public bool subscriber; //구독자인지 알려준다.
    public int money; //도네이션을 할수있는 금액 
    public const int LOVEGAME = 5; //좋아하는 게임을 발견했을경우 흥미도를 50% 올린다.
    public const int LOVETAG = 2; //좋아하는 테그를 발견햇을경우 흥미도를 20% 올린다.
    public const int MAXINTEREST = 10;
    public const int MULTIPLYMONEY = 1000;
    #endregion
    #region 생성자
    public People(bool sub=false)
    {
        if(sub)//구독자인경우 
        {
            state = State.BOARDCAST;
        }
        else
        {
            state = State.WAIT;
        }
      
        subscriber = sub; //구독자인가 
        interest = 0;
        SetPeople();
    }
    public People(People get) //얕은복사 
    {
        state = State.BOARDCAST;
        favoriteGame = get.favoriteGame;
        remainingTime = get.remainingTime;
        interest = get.interest;
        favoriteTag = get.favoriteTag;
        subscriber = get.subscriber;
        money = get.money;
        pc = get.pc;
    }
    #endregion
    public void SetMoney()
    {
        switch (pc)
        {
            case PeopleClass.SUDRA:  //없음 
                money = 0; 
                break;
            case PeopleClass.BAISHA:
                money = Random.Range(1, 5); //천원에서 5천원 
                break;
            case PeopleClass.KSHATRITA: //만원에서 5만원 
                money = Random.Range(10, 50);
                break;
            case PeopleClass.BRAHMIN://십만원에서 오십만원
                money = Random.Range(100, 500);
                break;
        }
        money *= MULTIPLYMONEY;
    }
        public void SetPeople() //랜덤으로 좋아하는 게임 ,좋아하는 태그 , 시청 가능 시간 을 설정한다.
    {
        favoriteGame = BoardcastManager.Instance.data.gameName[Random.Range(0, BoardcastManager.Instance.data.gameName.Count)]; //좋아하는 게임을 랜덤으로 설정한다. 
        favoriteTag = BoardcastManager.Instance.data.con_tag[Random.Range(0, BoardcastManager.Instance.data.con_tag.Count)]; //좋아하는 태그를 랜덤으로 하나를 설정한다.
        remainingTime = Random.Range(10, 100); //시청 시간은 짧게는 10분에서 길게는 100분 즉 1시간40분이다 
        pc = BoardcastManager.Instance.GetPeopleClass();
        SetMoney(); //역할에 따른 돈설정 
    }
    public void ChangeState(State set)
    {
        state = set;
    }
    public State GetState()
    {
        return state;
    }

    public void Action()
    {
        switch (state)
        {
            case State.WAIT:
                Wait();
                break;
            case State.BOARDCAST:
                Boardcast();
                break;
            case State.CHAT:
                Chat();
                break;
            case State.OUT:
                Out();
                break;
        }
        RemainingTime();
    }

    private void Wait() //대기실에 있는 상태
    {
        if(BoardcastManager.Instance.boardcastContens.name == favoriteGame) //내가 좋아하는 게임과 현재 하는 방송의 게임이 일치하는가 ? 
        {
            interest += LOVEGAME;
        }
        if (BoardcastManager.Instance.boardcastContens.con_tag.Equals(favoriteTag)) //내가 좋아하는 태그와 현재 하는 방송의 태그와 일치하는가 ?  
        {
            interest += LOVETAG;
        }
        interest += BoardcastManager.Instance.boardcastContens.Popularity;//현재 컨텐츠 인기도 인기도  수치가 2면 흥미를 2를 더해준다. 
        if(MAXINTEREST < interest) //흥미도의 최대는 10을 넘기지 않는다.
        {
            interest = MAXINTEREST;
        }
        if (Random.Range(0, 10) <= interest) // 랜덤으로 흥미가 생기는지 판단 
        {
            BoardcastManager.Instance.viewers.Attention(this);
        }
    }
    private void Boardcast() //1분마다 3% 확률로 채팅을 입력하는 상태로 변한다. 
    {
        if(Random.Range(0,100) <= 3)
        {
            ChangeState(State.CHAT);
        }
    }
    private void Chat() //3%확률로 이벤트가 발생한다.
    {
        if(Random.Range(0,100) > 3) //일반채팅 
        {
            //
        }
        else //3% 확률로 도네이션을 한다.
        {
            if (money <= 0)
                return;
            BoardcastManager.Instance.eventListener.DonationEvent(money);
            money = 0;
        }
        ChangeState(State.BOARDCAST); //다시 시청하는 상태가됌 
    }
    private void Out() //시간이 없는 사람들은 나가는 함수 
    {
        BoardcastManager.Instance.viewers.OutPeople(this);
    }

    public void RemainingTime() //시간이 감소하는 함수 
    {
        remainingTime--;

        if(remainingTime<=0)
        {
            ChangeState(State.OUT);
        }
    }




    
}