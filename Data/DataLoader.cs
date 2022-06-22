using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DataLoader
{
    public static void LoadPrefabs<T, I>(string path, List<I> source_list) where T : InfoPrefab<I> where I : Info
    {
        T[] temp = Resources.LoadAll<T>(path);
        source_list.Clear();

        for (int i = 0; i < temp.Length; i++)
        {
            source_list.Add(temp[i].info);
        }
    }

    public static void LoadPrefabs<T, I>(string path, List<List<I>> source_list) where T : InfoArrPrefab<I> where I : Info
    {
        T[] temp = Resources.LoadAll<T>(path);
        source_list.Clear();

        for (int i = 0; i < temp.Length; i++)
        {
            List<I> temp_list = new List<I>();

            for (int j = 0; j < temp[i].info.Length; j++)
            {
                temp_list.Add(temp[i].info[j]);
            }

            source_list.Add(temp_list);
        }
    }

    public static void CSVLoadDateTimeData(string path, string file_name)
    {
        //저장된 데이터 가져오기
        List<Dictionary<string, object>> dictionary_data = CSVReader.Read(path + file_name);

        if (dictionary_data == null)
        {
            DataInit.DateTimeInit();
            return;
        }

        DatabaseManager.Current_date = new DateTime(
        int.Parse(dictionary_data[0]["YEAR"].ToString()),
        int.Parse(dictionary_data[0]["MONTH"].ToString()),
        int.Parse(dictionary_data[0]["DAY"].ToString()),
        int.Parse(dictionary_data[0]["HOUR"].ToString()),
        int.Parse(dictionary_data[0]["MINUTE"].ToString()), 0
        );

        //for (int i = 0; i < dictionary_data.Count; i++)
        //{
        //    DatabaseManager.Instance.Current_date = new DateTime(
        //    int.Parse(dictionary_data[i]["YEAR"].ToString()),
        //    int.Parse(dictionary_data[i]["MONTH"].ToString()),
        //    int.Parse(dictionary_data[i]["DAY"].ToString()),
        //    int.Parse(dictionary_data[i]["HOUR"].ToString()),
        //    int.Parse(dictionary_data[i]["MINUTE"].ToString()), 0
        //    );
        //}
    }

    public static void CSVLoadyMyContentsData(string path, string file_name, List<ContentsInfo> my_contents_list, List<ContentsInfo> contents_list)
    {
        //저장된 데이터 가져오기
        List<Dictionary<string, object>> dictionary_data = CSVReader.Read(path + file_name);

        if (dictionary_data == null)
        {
            DataInit.MyContentsDataInit(my_contents_list);
            return;
        }

        my_contents_list.Clear();

        for (int i = 0; i < dictionary_data.Count; i++)
        {
            foreach (ContentsInfo info in contents_list)
            {
                if (dictionary_data[i]["NAME"].ToString() == info.name)
                {
                    my_contents_list.Add(new ContentsInfo(info));
                    break;
                }
            }
        }
    }

    // 자신이 소지한 가구 데이터 불러오기
    public static void CSVLoadyMyFurnitureData(string path, string file_name, List<FurnitureInfo> my_furniture_list)
    {
        //저장된 데이터 가져오기
        List<Dictionary<string, object>> dictionary_data = CSVReader.Read(path + file_name);

        if (dictionary_data == null)
        {
            DataInit.MyFurnitureDataInit(my_furniture_list);
            return;
        }

        my_furniture_list.Clear();

        for (int i = 0; i < dictionary_data.Count; i++)
        {
            string _name = dictionary_data[i]["NAME"].ToString();
            int _level = int.Parse(dictionary_data[i]["LEVEL"].ToString());

            FurnitureInfo temp = DatabaseManager.Instance.SearchFurnitureData(_name, _level);

            my_furniture_list.Add(new FurnitureInfo(temp));
        }
    }

    public static void CSVLoadyMyEditorData(string path, string file_name, List<VideoEditorInfo> my_editor_list, List<VideoEditorInfo> editor_list)
    {
        //저장된 데이터 가져오기
        List<Dictionary<string, object>> dictionary_data = CSVReader.Read(path + file_name);

        if (dictionary_data == null)
        {
            DataInit.MyEditorDataInit(my_editor_list);
            return;
        }

        my_editor_list.Clear();

        for (int i = 0; i < dictionary_data.Count; i++)
        {
            foreach (VideoEditorInfo info in editor_list)
            {
                if (dictionary_data[i]["NAME"].ToString() == info.name)
                {
                    VideoEditorInfo temp = new VideoEditorInfo(info);
                    temp.is_employ = true;


                    if (bool.Parse(dictionary_data[i]["IS_WORKING"].ToString()))
                    {
                        temp.is_working = true;
                    }
                    else
                    {
                        temp.is_working = false;
                    }

                    my_editor_list.Add(temp);
                    break;
                }
            }
        }
    }

    public static void CSVLoadMyChallengeData(string path, string file_name, List<ChallengeInfo> my_challenge_list)
    {
        List<Dictionary<string, object>> dictionary_data = CSVReader.Read(path + file_name);

        if (dictionary_data == null)
        {
            DataInit.MyChallengeDataInit(my_challenge_list);
            return;
        }

        my_challenge_list.Clear();

        for (int i = 0; i < dictionary_data.Count; i++)
        {
            string _title = dictionary_data[i]["TITLE"].ToString();
            int _achieve = int.Parse(dictionary_data[i]["ACHIEVE"].ToString());
            int _index = int.Parse(dictionary_data[i]["INDEX"].ToString());
            my_challenge_list.Add(new ChallengeInfo(_title, _achieve, _index));
        }
        my_challenge_list.Sort((x, y) => { return x.index.CompareTo(y.index); });
    }
}
