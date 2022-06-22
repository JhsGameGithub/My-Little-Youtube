using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordedVideoInfoListFactory : AbsInfoListFactory
{
    // ã�ƺ��� ������ �߰�
    public override List<Info> CreateInfo()
    {
        List<Info> temp_list = new List<Info>();

        foreach (RecordedVideoInfo all_recorded_video_data in VideoManager.Instance.my_recorded_list)
        {
            temp_list.Add(new RecordedVideoInfo(all_recorded_video_data));
        }

        return temp_list;
    }
}
