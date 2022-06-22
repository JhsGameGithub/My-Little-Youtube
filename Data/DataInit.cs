using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DataInit
{
    public static void DateTimeInit()
    {
        DatabaseManager.Current_date = new DateTime(2022, 1, 1);
    }

    public static void MyContentsDataInit(List<ContentsInfo> source_list)
    {
        source_list.Clear();

        source_list.Add(new ContentsInfo(DatabaseManager.SearchData("JustChatting", DatabaseManager.Instance.contents_list)));
    }

    public static void MyFurnitureDataInit(List<FurnitureInfo> source_list)
    {
        source_list.Clear();

        for (int i = 0; i < DatabaseManager.Instance.furniture_list.Count; i++)
        {
            source_list.Add(new FurnitureInfo(DatabaseManager.Instance.furniture_list[i][1]));
        }

        DatabaseManager.SearchData("포션", source_list).level = 0;
        DatabaseManager.SearchData("페이스 크림", source_list).level = 0;
    }

    public static void MyEditorDataInit(List<VideoEditorInfo> source_list)
    {
        source_list.Clear();

        for (int i = 0; i < DatabaseManager.Instance.editor_list.Count; i++)
        {
            if (DatabaseManager.Instance.editor_list[i].id == DatabaseManager.PLAYER_ID)
            {
                DatabaseManager.Instance.editor_list[i].name = DatabaseManager.Player.status.Name;

                source_list.Add(new VideoEditorInfo(DatabaseManager.Instance.editor_list[i]));

                return;
            }
        }
    }
    public static void MyChallengeDataInit(List<ChallengeInfo> source_list)
    {
        source_list.Clear();
        for (int i = 0; i < DatabaseManager.Instance.challenge_list.Count; i++)
        {
            ChallengeInfo temp = DatabaseManager.Instance.challenge_list[i];
            source_list.Add(temp);
        }
        source_list.Sort((x, y) => { return x.index.CompareTo(y.index); });
    }
}
