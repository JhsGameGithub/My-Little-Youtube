using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DataSaver
{
    public static void CSVSaveDateTimeData(string path, string file_name, DateTime current_data)
    {
        List<string[]> row_data = new List<string[]>();
        string[] title_data = { "YEAR", "MONTH", "DAY", "HOUR", "MINUTE" };

        row_data.Add(title_data);

        string[] row_data_temp = new string[title_data.Length];

        row_data_temp[0] = current_data.Year.ToString();
        row_data_temp[1] = current_data.Month.ToString();
        row_data_temp[2] = current_data.Day.ToString();
        row_data_temp[3] = current_data.Hour.ToString();
        row_data_temp[4] = current_data.Minute.ToString();

        //추가
        row_data.Add(row_data_temp);

        //파일 저장 ( CSV 파일 쓰기)
        CSVSaver.WriteCSV(row_data, path + file_name);
    }

    public static void CSVSaveMyContentsData(string path, string file_name, List<ContentsInfo> my_contents_list)
    {
        List<string[]> row_data = new List<string[]>();
        string[] title_data = { "NAME" };

        row_data.Add(title_data);

        for (int i = 0; i < my_contents_list.Count; i++)
        {
            string[] row_data_temp = new string[title_data.Length];

            //이름 저장
            row_data_temp[0] = my_contents_list[i].name;

            //추가
            row_data.Add(row_data_temp);
        }

        //파일 저장 ( CSV 파일 쓰기)
        CSVSaver.WriteCSV(row_data, path + file_name);
    }

    public static void CSVSaveMyFurnitureData(string path, string file_name, List<FurnitureInfo> my_furniture_list)
    {
        List<string[]> row_data = new List<string[]>();
        string[] title_data = { "NAME", "LEVEL" };

        row_data.Add(title_data);

        for (int i = 0; i < my_furniture_list.Count; i++)
        {
            string[] row_data_temp = new string[title_data.Length];

            //이름 저장
            row_data_temp[0] = my_furniture_list[i].name;

            //현재 레벨 저장
            row_data_temp[1] = my_furniture_list[i].level.ToString();

            //추가
            row_data.Add(row_data_temp);
        }

        //파일 저장 ( CSV 파일 쓰기)
        CSVSaver.WriteCSV(row_data, path + file_name);
    }

    public static void CSVSaveMyEditorData(string path, string file_name, List<VideoEditorInfo> my_editor_list)
    {
        List<string[]> row_data = new List<string[]>();
        string[] title_data = { "NAME", "IS_EMPLOY", "IS_WORKING" };

        row_data.Add(title_data);

        for (int i = 0; i < my_editor_list.Count; i++)
        {
            string[] row_data_temp = new string[title_data.Length];

            //이름 저장
            row_data_temp[0] = my_editor_list[i].name;

            //고용 여부 저장
            row_data_temp[1] = my_editor_list[i].is_employ.ToString();

            //작업 여부 저장
            row_data_temp[2] = my_editor_list[i].is_working.ToString();

            //추가
            row_data.Add(row_data_temp);
        }

        //파일 저장 ( CSV 파일 쓰기)
        CSVSaver.WriteCSV(row_data, path + file_name);
    }

    public static void CSVSaveMyChallengeData(string path, string file_name, List<ChallengeInfo> my_challenge_list)
    {
        List<string[]> row_data = new List<string[]>();
        string[] title_data = { "TITLE","ACHIEVE","INDEX" };

        row_data.Add(title_data);

        for(int i=0;i< my_challenge_list.Count;i++)
        {
            string[] row_data_temp = new string[title_data.Length];

            row_data_temp[0] = my_challenge_list[i].title;
            row_data_temp[1] = my_challenge_list[i].achieve.ToString();
            row_data_temp[2] = my_challenge_list[i].index.ToString();

            row_data.Add(row_data_temp);
        }
        CSVSaver.WriteCSV(row_data, path + file_name);
    }
}
