using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengeBroker : MonoBehaviour
{
    public enum Item { Contents, Broadcast, Furniture, Status, Video, Editor }
    public ChallengeObserver challenge_observer;
    private Dictionary<string, ChallengeEnum.Challenge> notify_broadcast = new Dictionary<string, ChallengeEnum.Challenge>();
    private Dictionary<string, ChallengeEnum.Challenge> notify_contents = new Dictionary<string, ChallengeEnum.Challenge>();
    private Dictionary<string, ChallengeEnum.Challenge> notify_furniture = new Dictionary<string, ChallengeEnum.Challenge>();
    private Dictionary<string, ChallengeEnum.Challenge> notify_status = new Dictionary<string, ChallengeEnum.Challenge>();
    private Dictionary<string, ChallengeEnum.Challenge> notify_video = new Dictionary<string, ChallengeEnum.Challenge>();
    private Dictionary<string, ChallengeEnum.Challenge> notify_editor = new Dictionary<string, ChallengeEnum.Challenge>();
    private static ChallengeBroker instance = null;

    public static ChallengeBroker Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = GameObject.Find("ChallengeBroker");
                if (go == null)
                {
                    go = new GameObject { name = "ChallengeBroker" };
                    go.AddComponent<ChallengeBroker>();
                }

                DontDestroyOnLoad(go);
                instance = go.GetComponent<ChallengeBroker>();
            }
            return instance;
        }
    }
    public void Start()
    {
        Init_Notify_Broadcast();
        Init_Notify_Contents();
        Init_Notify_Editor();
        Init_Notify_Furniture();
        Init_Notify_Status();
        Init_Notify_Video();
    }
    public void Init_Notify_Broadcast()
    {
        notify_broadcast.Add("첫방송", ChallengeEnum.Challenge.BROADCAST_FIRST);
        notify_broadcast.Add("레스토랑스", ChallengeEnum.Challenge.MASTER_CHEF);
        notify_broadcast.Add("엘데의 제왕", ChallengeEnum.Challenge.KING_OF_ELDE);
        notify_broadcast.Add("와! 파피!", ChallengeEnum.Challenge.IDOL_OF_KIDS);
        notify_broadcast.Add("와!!!!!!", ChallengeEnum.Challenge.IDOL_OF_KIDS);
        notify_broadcast.Add("머쉬룸스토리", ChallengeEnum.Challenge.MAPLE_RANKER);
        notify_broadcast.Add("킹근갓택2", ChallengeEnum.Challenge.FLASH_MANNER);
        notify_broadcast.Add("YCOM", ChallengeEnum.Challenge.ALIEN_KILLER);
        notify_broadcast.Add("공익과 싸움꾼", ChallengeEnum.Challenge.HOW_FAST_NUGOL);
        notify_broadcast.Add("Deep Dark Soul", ChallengeEnum.Challenge.FINAL_FIREWOOD);
        notify_broadcast.Add("LOSTANG", ChallengeEnum.Challenge.SCULPTOR);
        notify_broadcast.Add("치킨그라운드", ChallengeEnum.Challenge.CHICKEN_OVERDOSE);
        notify_broadcast.Add("어둠의 듀얼", ChallengeEnum.Challenge.MY_DREAM_IS_KAIBAR);
        notify_broadcast.Add("호블린", ChallengeEnum.Challenge.KING_OF_HOGLIN);
        notify_broadcast.Add("이삭의 구속", ChallengeEnum.Challenge.MASTER_TEAR);
        notify_broadcast.Add("흡혈귀의 생존", ChallengeEnum.Challenge.KING_VAMPIRE);
    }
    public void Init_Notify_Contents()
    {
        notify_contents.Add("컨텐츠", ChallengeEnum.Challenge.CONTENTS_BANK);
    }
    public void Init_Notify_Status()
    {
        notify_status.Add("건강", ChallengeEnum.Challenge.HEALTH_FREAK);
        notify_status.Add("왼손", ChallengeEnum.Challenge.LEFT_HAND_1000_USD);
        notify_status.Add("오른손", ChallengeEnum.Challenge.RIGHT_HAND_10000_USD);
        challenge_observer.Unlock_Challenge(15, true, DatabaseManager.Player.wallet.Money);
        challenge_observer.Unlock_Challenge(16, true, DatabaseManager.Player.wallet.Money);

        notify_status.Add("조회수10000", ChallengeEnum.Challenge.ALL_VIEW_10000);
        notify_status.Add("조회수100000", ChallengeEnum.Challenge.ALL_VIEW_100000);
        notify_status.Add("조회수1000000", ChallengeEnum.Challenge.ALL_VIEW_1000000);
        notify_status.Add("조회수10000000", ChallengeEnum.Challenge.ALL_VIEW_10000000);
        notify_status.Add("구독자1", ChallengeEnum.Challenge.SUBSCRIBER_FIRST);
        notify_status.Add("구독자1000", ChallengeEnum.Challenge.SUBSCRIBER_1000);
        notify_status.Add("구독자50000", ChallengeEnum.Challenge.SUBSCRIBER_50000);
        notify_status.Add("구독자500000", ChallengeEnum.Challenge.SUBSCRIBER_500000);
        notify_status.Add("실버버튼 달성!", ChallengeEnum.Challenge.ACHIEVE_SILVER_BUTTON);
        notify_status.Add("골드버튼 달성!", ChallengeEnum.Challenge.ACHIEVE_GOLD_BUTTON);
        notify_status.Add("시청자100", ChallengeEnum.Challenge.VIEWER_100);
        notify_status.Add("시청자1000", ChallengeEnum.Challenge.VIEWER_1000);
        notify_status.Add("시청자10000", ChallengeEnum.Challenge.VIEWER_10000);
    }
    public void Init_Notify_Furniture()
    {
        notify_furniture.Add("포션", ChallengeEnum.Challenge.POTION_ADDICT);
        notify_furniture.Add("침대", ChallengeEnum.Challenge.FIVE_STAR);
        notify_furniture.Add("장비", ChallengeEnum.Challenge.SUPER_COMPUTER);
    }
    public void Init_Notify_Video()
    {
        notify_video.Add("첫영상", ChallengeEnum.Challenge.VIDEO_FIRST);
    }
    public void Init_Notify_Editor()
       {
        notify_editor.Add("편집자", ChallengeEnum.Challenge.GOSHUJIN_SAMA);
    }

    public void Init_Subject(ChallengeSubject subject)
    {
        subject.challenge_handler += new ChallengeSubject.ChallengeHandler(Notify);
    }
    public void Notify(Item item, string name, int achieve)
    {
        switch (item)
        {
            case Item.Broadcast:
                if (notify_broadcast.ContainsKey(name))
                    challenge_observer.Unlock_Challenge((int)notify_broadcast[name], false, achieve);
                challenge_observer.Unlock_Challenge((int)notify_broadcast["첫방송"], false, achieve);
                break;
            case Item.Contents:
                challenge_observer.Unlock_Challenge((int)notify_contents["컨텐츠"], false, achieve);
                break;
            case Item.Furniture:
                challenge_observer.Unlock_Challenge((int)notify_furniture[name], false, achieve);
                if (name == "침대")
                    challenge_observer.Unlock_Challenge((int)notify_furniture["장비"], false, achieve);
                break;
            case Item.Status:
                if (name == "조회수")
                {
                    challenge_observer.Unlock_Challenge((int)notify_status["조회수10000"], false, achieve);
                    challenge_observer.Unlock_Challenge((int)notify_status["조회수100000"], false, achieve);
                    challenge_observer.Unlock_Challenge((int)notify_status["조회수1000000"], false, achieve);
                    challenge_observer.Unlock_Challenge((int)notify_status["조회수10000000"], false, achieve);
                }
                else if (name == "구독자")
                {
                    challenge_observer.Unlock_Challenge((int)notify_status["구독자1"], false, achieve);
                    challenge_observer.Unlock_Challenge((int)notify_status["구독자1000"], false, achieve);
                    challenge_observer.Unlock_Challenge((int)notify_status["구독자50000"], false, achieve);
                    challenge_observer.Unlock_Challenge((int)notify_status["구독자500000"], false, achieve);
                    challenge_observer.Unlock_Challenge((int)notify_status["실버버튼 달성!"], false, achieve);
                    challenge_observer.Unlock_Challenge((int)notify_status["골드버튼 달성!"], false, achieve);
                }
                else if (name == "시청자")
                {
                    challenge_observer.Unlock_Challenge((int)notify_status["시청자100"], false, achieve);
                    challenge_observer.Unlock_Challenge((int)notify_status["시청자1000"], false, achieve);
                    challenge_observer.Unlock_Challenge((int)notify_status["시청자10000"], false, achieve);
                }
                else if (name == "현금")
                {
                    challenge_observer.Unlock_Challenge((int)notify_status["왼손"], false, achieve);
                    challenge_observer.Unlock_Challenge((int)notify_status["오른손"], false, achieve);
                }
                else
                {
                    challenge_observer.Unlock_Challenge((int)notify_status[name], false, achieve);
                }
                break;
            case Item.Video:
                challenge_observer.Unlock_Challenge((int)notify_video["첫영상"], false, achieve);
                break;
            case Item.Editor:
                challenge_observer.Unlock_Challenge((int)notify_editor[name], false, achieve);
                break;
        }
    }
}
