using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VideoManager : MonoBehaviour
{
    public List<RecordedVideoInfo> recorded_video_list = new List<RecordedVideoInfo>();
    public List<EditedVideoInfo> edited_video_list = new List<EditedVideoInfo>();

    [SerializeField]
    public List<RecordedVideoInfo> my_recorded_list = new List<RecordedVideoInfo>();
    public List<EditedVideoInfo> my_edited_list = new List<EditedVideoInfo>();

    public EditorDropdownManager dropdown_manager;

    public string path;

    private static VideoManager instance = null;

    public static VideoManager Instance
    {
        get
        {
            Init();

            return instance;
        }
    }

    static void Init()
    {
        if (instance == null && Time.timeScale != 0)
        {
            GameObject go = GameObject.Find("VideoManager");
            if (go == null)
            {
                go = Instantiate(Resources.Load("Prefabs/Manager/VideoManager"), new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                go.name = "VideoManager";
            }
            DontDestroyOnLoad(go);
            instance = go.GetComponent<VideoManager>();
        }
    }

    private void Start()
    {
        path = Application.persistentDataPath + "/";
    }

    // ���Z���� 0�� ������� ���� CSV�� ��ȭ�� ������� ���� _ ����
    public void CSVSaveMyRecordedVideoData(string file_name)
    {
        List<string[]> row_data = new List<string[]>();
        string[] title_data = { "TITLE", "CONTENTS", "DATE", "FUNNY", "WAIT", "EDITOR" };
        row_data.Add(title_data);

        for (int i = 0; i < my_recorded_list.Count; i++)
        {
            string[] row_data_temp = new string[title_data.Length];


            row_data_temp[0] = my_recorded_list[i].title;
            row_data_temp[1] = my_recorded_list[i].contents.name;
            row_data_temp[2] = my_recorded_list[i].year + "-" +
                               my_recorded_list[i].month + "-" +
                               my_recorded_list[i].day + "-" +
                               my_recorded_list[i].hour;
            row_data_temp[3] = my_recorded_list[i].funny.ToString();
            row_data_temp[4] = my_recorded_list[i].wait.ToString();
            row_data_temp[5] = my_recorded_list[i].editor == null ? "" : my_recorded_list[i].editor.name;

            row_data.Add(row_data_temp);
        }
        CSVSaver.WriteCSV(row_data, path + file_name);
    }

    public void CSVSaveMyEditedVideoData(string file_name)
    {
        List<string[]> row_data = new List<string[]>();
        string[] title_data = { "TITLE", "VIEWS", "GOODS", "DATE", "EDITOR", "FUNNY", "CONTENTS", "REPEAT", "SUBSCRIBER", "POPULARITY", "SEED" };
        row_data.Add(title_data);

        for (int i = 0; i < my_edited_list.Count; i++)
        {
            string[] row_data_temp = new string[title_data.Length];

            //영상 제목
            row_data_temp[0] = my_edited_list[i].title;
            //조회수
            row_data_temp[1] = my_edited_list[i].views.ToString();
            //좋아요
            row_data_temp[2] = my_edited_list[i].goods.ToString();
            //업로드 날짜
            row_data_temp[3] = my_edited_list[i].year + "-" +
                               my_edited_list[i].month + "-" +
                               my_edited_list[i].day + "-" +
                               my_edited_list[i].hour;
            //편집자
            row_data_temp[4] = my_edited_list[i].editor;
            //유튭각
            row_data_temp[5] = my_edited_list[i].funny.ToString();
            //콘텐츠
            row_data_temp[6] = my_edited_list[i].contents.name;
            //계산횟수
            row_data_temp[7] = my_edited_list[i].repeat.ToString();
            //구독자
            row_data_temp[8] = my_edited_list[i].calculator.subscriber.ToString();
            //인기도
            row_data_temp[9] = my_edited_list[i].calculator.popularity.ToString();
            //난수
            row_data_temp[10] = my_edited_list[i].calculator.seed.ToString();

            row_data.Add(row_data_temp);
        }
        CSVSaver.WriteCSV(row_data, path + file_name);
    }

    //CSV�� ����Ǿ��ִ� ��ȭ�� ����� ��� �ҷ����� _ ����
    public void CSVLoadMyRecordedVideoData(string file_name)
    {
        List<Dictionary<string, object>> dictionary_data = CSVReader.Read(path + file_name);

        if (dictionary_data == null)
            return;

        my_recorded_list.Clear();

        for (int i = 0; i < dictionary_data.Count; i++)
        {
            string editor = dictionary_data[i]["EDITOR"].ToString();

            if (editor == "") //미편집 녹화 영상
            {
                my_recorded_list.Add(new RecordedVideoInfo(
                    dictionary_data[i]["DATE"].ToString(), //날짜
                    DatabaseManager.SearchData(dictionary_data[i]["CONTENTS"].ToString(), DatabaseManager.Instance.contents_list), //컨텐츠
                    int.Parse(dictionary_data[i]["FUNNY"].ToString()) //유툽각
                    ));
            }
            else //편집중 녹화 영상
            {
                VideoEditorInfo tempEditor = (editor == "플레이어") ? DatabaseManager.Player.selfEditor : DatabaseManager.SearchData(editor, DatabaseManager.Instance.editor_list);

                my_recorded_list.Add(new RecordedVideoInfo(
                    dictionary_data[i]["TITLE"].ToString(), //제목
                    dictionary_data[i]["DATE"].ToString(),  //날짜
                    DatabaseManager.SearchData(dictionary_data[i]["CONTENTS"].ToString(), DatabaseManager.Instance.my_contents_list), // 컨텐츠
                    int.Parse(dictionary_data[i]["FUNNY"].ToString()), //유툽각
                    int.Parse(dictionary_data[i]["WAIT"].ToString()), //남은 편집 시간
                    tempEditor));   // 편집자

            }
        }
    }

    public void CSVLoadMyEditedVideoData(string file_name)
    {
        List<Dictionary<string, object>> dictionary_data = CSVReader.Read(path + file_name);

        if (dictionary_data == null)
            return;

        my_edited_list.Clear();

        for (int i = 0; i < dictionary_data.Count; i++)
        {
            my_edited_list.Add(new EditedVideoInfo(
                dictionary_data[i]["TITLE"].ToString(),
                int.Parse(dictionary_data[i]["VIEWS"].ToString()),
                int.Parse(dictionary_data[i]["GOODS"].ToString()),
                dictionary_data[i]["DATE"].ToString(),
                dictionary_data[i]["EDITOR"].ToString(),
                int.Parse(dictionary_data[i]["FUNNY"].ToString()),
                DatabaseManager.SearchData(dictionary_data[i]["CONTENTS"].ToString(), DatabaseManager.Instance.contents_list),
                int.Parse(dictionary_data[i]["REPEAT"].ToString()),
                int.Parse(dictionary_data[i]["SUBSCRIBER"].ToString()),
                float.Parse(dictionary_data[i]["POPULARITY"].ToString()),
                int.Parse(dictionary_data[i]["SEED"].ToString())
                ));
        }
    }

    public RecordedVideoInfo SearchMyRecordedVideoData(int year, int month, int day, int hour)
    {
        foreach (RecordedVideoInfo my_recorded_data in my_recorded_list)
        {
            if (my_recorded_data.year == year &&
                my_recorded_data.month == month &&
                my_recorded_data.day == day &&
                my_recorded_data.hour == hour)
            {
                return my_recorded_data;
            }
        }

        return null;
    }

    public void DataSave()
    {
        CSVSaveMyEditedVideoData("MyEditedVideoData.csv");

        CSVSaveMyRecordedVideoData("MyRecordedVideoData.csv");
    }

    public void DataLoad()
    {
        CSVLoadMyEditedVideoData("MyEditedVideoData.csv");

        CSVLoadMyRecordedVideoData("MyRecordedVideoData.csv");
    }

    public void BeginDataLoad()
    {
        my_recorded_list.Clear();
        my_edited_list.Clear();
    }

}
