using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoEditorInfoListFactory : AbsInfoListFactory
{
    public override List<Info> CreateInfo()
    {
        List<Info> temp_list = new List<Info>();

        for (int i = 0; i < DatabaseManager.Instance.editor_list.Count; i++)
        {
            if (DatabaseManager.Instance.editor_list[i].id != DatabaseManager.PLAYER_ID)
            {
                temp_list.Add(new VideoEditorInfo(DatabaseManager.Instance.editor_list[i]));
            }
        }

        foreach (VideoEditorInfo myEditor in DatabaseManager.Instance.my_editor_list)
        {
            foreach (VideoEditorInfo temp_info in temp_list)
            {
                if (temp_info.name == myEditor.name)
                {
                    temp_info.is_employ = true;
                    break;
                }
            }
        }

        return temp_list;
    }
}
