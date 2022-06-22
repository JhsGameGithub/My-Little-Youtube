using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordedVideoCreator : MonoBehaviour
{
    public GameObject recorded_video;

    public void CreateRecordedVideo(RecordedVideoInfo info)
    {
        GameObject recorded_video_data = Instantiate(recorded_video, gameObject.transform);

        RecordedVideo temp = recorded_video_data.GetComponent<RecordedVideo>();
        temp.recorded_video_info.SetInfo(info);
    }
}
