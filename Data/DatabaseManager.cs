using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
public class DatabaseManager : MonoBehaviour, IObserverData, ISubjectData
{
    public static readonly int PLAYER_ID = 5000;

    // 저장할 시간 정보
    [SerializeField]
    private DateTime current_date;
    public static DateTime Current_date
    {
        get
        {
            if (Instance.current_date == null)
            {
                DataLoader.CSVLoadDateTimeData(path, "DateTimeData.csv");
            }

            return Instance.current_date;
        }

        set => Instance.current_date = value;
    }

    private int d_day;

    // 프리펩 데이터 저장용
    public List<TagInfo> tag_list = new List<TagInfo>();
    public List<ContentsInfo> contents_list = new List<ContentsInfo>();
    public List<AddContentsInfo> add_contents_list = new List<AddContentsInfo>();
    public List<VideoEditorInfo> editor_list = new List<VideoEditorInfo>();
    public List<List<FurnitureInfo>> furniture_list = new List<List<FurnitureInfo>>();
    public List<ChallengeInfo> challenge_list = new List<ChallengeInfo>();

    // 자신이 소지한 컨텐츠
    public List<ContentsInfo> my_contents_list = new List<ContentsInfo>();
    // 자신이 소지한 추가 컨텐츠
    public List<AddContentsInfo> my_add_contents_list = new List<AddContentsInfo>();
    // 자신이 소지한 가구
    public List<FurnitureInfo> my_furniture_list = new List<FurnitureInfo>();
    // 자신이 고용한 편집자
    public List<VideoEditorInfo> my_editor_list = new List<VideoEditorInfo>();
    // 달성한 도전과제
    public List<ChallengeInfo> my_challenge_list = new List<ChallengeInfo>();

    private Player player = new Player(); //플레이어 돈 =Wallet 장비 =Required 스탯 =Status 을 가진다 . 플레이어가 가진 정보를 uidata 오브젝트에서 LateUpdate에서 값을 참조해서 ui update를 하고있다.
    public static Player Player 
    { 
        get 
        {
            if (Instance.player == null)
            {
                Instance.player = new Player();
                Instance.player.Notify(Message.LOAD);
            }

            return Instance.player; 
        } 
    }


    // 임시임시 플레이어 재화
    public bool isGameRun = false;
    public bool isLoad = false;
    public bool isNewsUpdate = true;

    // CSV 데이터 저장 경로
    public static string path;

    // 옵저버들
    private List<IObserverData> observers = new List<IObserverData>();

    #region 싱글톤
    private static DatabaseManager instance = null;

    public static DatabaseManager Instance
    {
        get
        {
            Init();

            return instance;
        }
    }


    public int D_day
    {
        get
        {
            return d_day;
        }
        set
        {
            d_day = value;
        }

    }
    static void Init()
    {
        if (instance == null && Time.timeScale != 0)
        {
            GameObject go = GameObject.Find("DatabaseManager");

            if (go == null)
            {
                go = new GameObject { name = "DatabaseManager" };
                go.AddComponent<DatabaseManager>();
            }

            path = Application.persistentDataPath + "/";

            DontDestroyOnLoad(go);
            instance = go.GetComponent<DatabaseManager>();

            // 프리펩 데이터 가지고 오기
            LoadPrefabs();

            // CSV 데이터 가지고 오기 (소지 컨텐츠, 가구, 편집자) 
            DataLoad();

        }
    }

    private void OnApplicationQuit()
    {
        // 종료 시 
        Time.timeScale = 0;
    }

    #endregion

    #region 프리팹 불러오기

    public static void LoadPrefabs()
    {
        DataLoader.LoadPrefabs<Tag, TagInfo>("Prefabs/Tag", Instance.tag_list);
        DataLoader.LoadPrefabs<Contents, ContentsInfo>("Prefabs/Contents", Instance.contents_list);
        DataLoader.LoadPrefabs<AddContents, AddContentsInfo>("Prefabs/Shop", Instance.add_contents_list);
        DataLoader.LoadPrefabs<VideoEditor, VideoEditorInfo>("Prefabs/VideoEditor", Instance.editor_list);
        DataLoader.LoadPrefabs<Furniture, FurnitureInfo>("Prefabs/Furniture", Instance.furniture_list);
        DataLoader.LoadPrefabs<Challenge, ChallengeInfo>("Prefabs/Challenge", Instance.challenge_list);
    }

    #endregion

    #region CSV Save

    //데이터 저장
    public void DataSave()
    {
        PlayerPrefs.SetInt("D_DAY", D_day);

        player.Notify(Message.SAVE);

        DataSaver.CSVSaveDateTimeData(path, "DateTimeData.csv", current_date);
        DataSaver.CSVSaveMyContentsData(path, "MyContentsData.csv", my_contents_list);
        DataSaver.CSVSaveMyFurnitureData(path, "MyFurnitureData.csv", my_furniture_list);
        DataSaver.CSVSaveMyEditorData(path, "MyEditorData.csv", my_editor_list);
        DataSaver.CSVSaveMyChallengeData(path, "MyChallengeData.csv", my_challenge_list);

        VideoManager.Instance.DataSave();

        NotifyObservers("Save");
    }

    #endregion

    #region CSV Load

    //데이터 로드
    public static void DataLoad()
    {
        Instance.D_day = PlayerPrefs.GetInt("D_DAY", 1);

        Player.Notify(Message.LOAD);

        DataLoader.CSVLoadDateTimeData(path, "DateTimeData.csv");
        DataLoader.CSVLoadyMyContentsData(path, "MyContentsData.csv", Instance.my_contents_list, Instance.contents_list);
        DataLoader.CSVLoadyMyFurnitureData(path, "MyFurnitureData.csv", Instance.my_furniture_list);
        DataLoader.CSVLoadyMyEditorData(path, "MyEditorData.csv", Instance.my_editor_list, Instance.editor_list);
        DataLoader.CSVLoadMyChallengeData(path, "MyChallengeData.csv", Instance.my_challenge_list);


        VideoManager.Instance.DataLoad();

        Instance.NotifyObservers("Load");

        Instance.isLoad = true;
    }

    #endregion

    #region 자신의 시작 데이터 로드

    // 시작 데이터 로드
    public void BeginDataLoad()
    {
        LoadPrefabs();

        D_day = 1;

        player.Notify(Message.NEW);

        DataInit.DateTimeInit();
        DataInit.MyContentsDataInit(my_contents_list);
        DataInit.MyFurnitureDataInit(my_furniture_list);
        DataInit.MyEditorDataInit(my_editor_list);
        DataInit.MyChallengeDataInit(my_challenge_list);

        VideoManager.Instance.BeginDataLoad();

        // flex 상점 초기화
        File.WriteAllText(GetPathFlexItem(), "");
        File.Delete(DatabaseManager.Instance.GetPathNews());
    }

    #endregion

    #region 데이터 검색

    public static T SearchData<T>(string name, List<T> target_list) where T : Info
    {
        foreach(T copy_list in target_list)
        {
            if(copy_list.name.GetHashCode() == name.GetHashCode())
            {
                return copy_list;
            }
        }

        return null;
    }

    public FurnitureInfo SearchFurnitureData(string name, int level_index)
    {
        int name_index = 0;

        for (int i = 0; i < furniture_list.Count; i++)
        {
            if (furniture_list[i][0].name == name)
            {
                name_index = i;
                break;
            }
        }

        if (furniture_list[name_index].Count <= level_index)
            return null;

        return furniture_list[name_index][level_index];
    }

    public int GetFurnitureMaxLevel(string name)
    {
        int max_level = 0;

        for (int i = 0; i < furniture_list.Count; i++)
        {
            if (furniture_list[i][0].name == name)
            {
                max_level = furniture_list[i].Count - 1;
                break;
            }
        }

        return max_level;
    }

    #endregion

    #region 데이터 옵저버
    public void ObserverUpdate(string message = "")
    {
        // Tag 일 경우
        if(message.GetHashCode() == "Tag".GetHashCode())
        {
            foreach(var copy in contents_list)
            {
                copy.CalculatePopularity();
            }

            foreach (var copy in my_contents_list)
            {
                copy.CalculatePopularity();
            }
        }
    }

    public string GetPathObserver(ObserverVariety v) // 저장 불러오기 할떄 사용될 경로로 플레이어가가 가지고잇는각 옵저버에 메세지인 save lode 에서 사용됨 
    {
        string send = path;
        switch (v)
        {
            case ObserverVariety.STATUS:
                send += "MyStatus.txt";
                break;
            case ObserverVariety.REQUIRED:
                send += "MyRequired.txt";
                break;
            case ObserverVariety.WALLET:
                send += "MyWallet.txt";
                break;
        }
        return send;
    }
    public string GetPathFlexItem() 
    {
        string send = path;
        return send += "FlexItem.txt";
    }
    public string GetPathNews()
    {
        string send = path;
        return send += "News.txt";
    }
    #endregion

    #region 데이터 서브젝트
    public void AddObserver(IObserverData observer)
    {
        if(observers == null)
        {
            observers = new List<IObserverData>();
        }

        observers.Add(observer);
    }

    public void RemoveObserver(IObserverData observer)
    {
        if(observers == null)
        {
            return;
        }

        observers.Remove(observer);
    }

    public void NotifyObservers(string message = "")
    {
        if (observers == null)
        {
            return;
        }

        foreach(IObserverData observer in observers)
        {
            observer.ObserverUpdate(message);
        }
    }

    #endregion
}