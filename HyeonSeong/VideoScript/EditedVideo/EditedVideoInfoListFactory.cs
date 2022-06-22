using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditedVideoInfoListFactory : AbsInfoListFactory
{
    public override List<Info> CreateInfo()
    {
        List<Info> temp_list = new List<Info>();
        foreach(EditedVideoInfo all_edited_video_data in VideoManager.Instance.my_edited_list)
        {
            temp_list.Add(new EditedVideoInfo(all_edited_video_data));
        }
        return temp_list;
    }
}
