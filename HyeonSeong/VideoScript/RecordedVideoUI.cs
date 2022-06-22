using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordedVideoUI : VideoUI
{
    public EditedVideoUI edited_video_ui;

    private List<RecordedVideoInfo> info_list;
    private List<RecordedVideoSlot> slot_list = new List<RecordedVideoSlot>();
    public Queue<IEditingVideo> editing_video_slots = new Queue<IEditingVideo>();

    private void Start()
    {
        Init();

        info_list = VideoManager.Instance.my_recorded_list;

        foreach (RecordedVideoInfo info in info_list)
            Slot_Insert(info);


        foreach (RecordedVideoSlot slot in slot_list)
        {
            if (slot.Info.max_wait != 0)
            {
                slot.EditingLoad();
            }
            //Debug.Log(slot.title_text.text);
        }
        //Debug.Log(slot_parent);

        Clock.Instance.video_handler += new Clock.VideoHandler(EditingVideo);
        BoardcastManager.Instance.recorded_video_UI = this;
    }

    public void Slot_Insert(RecordedVideoInfo info)
    {
        GameObject slot_obj;
        if (slot_pool.Count == 0)
        {
            slot_obj = Instantiate(slot_prefab, new Vector3(0, 0, 0), Quaternion.identity);
            slot_obj.GetComponent<RecordedVideoSlot>().video_ui = this;
        }
        else
        {
            slot_obj = slot_pool.Pop();
            slot_obj.SetActive(true);
        }
        //Debug.Log(slot_obj);
        //Debug.Log(slot_parent);
        slot_obj.transform.SetParent(slot_parent.transform, false);

        RecordedVideoSlot slot = slot_obj.GetComponent<RecordedVideoSlot>();
        slot.SetInfo(info);
        slot.SetUI();
        slot_list.Add(slot);
    }

    public void Video_Insert(RecordedVideoInfo info)
    {
        info_list.Add(info);

        Slot_Insert(info);
    }

    public void Video_Remove(RecordedVideoSlot video)
    {
        slot_pool.Push(video.gameObject);
        info_list.Remove(video.Info);
        slot_list.Remove(video);
    }

    public void EditingVideo(bool is_player_edit = false)
    {

        for (int i = editing_video_slots.Count; i > 0; i--)
        {
            IEditingVideo temp = editing_video_slots.Dequeue();

            if (temp.Editing_Video(is_player_edit))
            {

                if (temp.Get_Editor_Info().name == DatabaseManager.Player.selfEditor.name)
                {
                    DatabaseManager.Player.selfEditor.is_working = false;
                }
                else
                {
                    foreach (VideoEditorInfo copy in DatabaseManager.Instance.my_editor_list)
                    {
                        if (copy.name == temp.Get_Editor_Info().name)
                        {
                            copy.is_working = false;
                            break;
                        }
                    }
                }


                RecordedVideoSlot edited_video = temp as RecordedVideoSlot;

                edited_video_ui.Video_Insert(edited_video.Info);

                Video_Remove(edited_video);


                //생성자의 매개변수로 팝업의 하이라키 위치 설정 -> 최상단 canvase
                PopupBuilder popupBuilder = new PopupBuilder(GameObject.Find("PopupCanvas").transform);
                popupBuilder.SetDescription("영상 업로드 완료!!");
                popupBuilder.SetAnim("FadeIn", "FadeOut");
                popupBuilder.SetAutoDestroy(true);
                popupBuilder.Build("UploadPopupUI");

                VideoManager.Instance.dropdown_manager.NotifyObservers();
                continue;
            }

            editing_video_slots.Enqueue(temp);
        }

    }

    public void Slot_Sort(SortButton select)
    {
        slot_sort.SortType = E_SORT.DEFAULT;

        for (int i = 0; i < sort_btns.Length; i++)
        {
            SortButton temp = sort_btns[i].GetComponent<SortButton>();

            if (select == null || temp.e_sort != select.e_sort)
            {
                if (temp.isSelect == true)
                    temp.OnSelectBtn();
            }
            else
            {
                List<GameObject> slot_obj = new List<GameObject>();
                foreach (RecordedVideoSlot slot in slot_list)
                    slot_obj.Add(slot.gameObject);
                slot_sort.SortType = temp.e_sort;
                slot_obj.Sort(slot_sort.Sort);
                foreach (GameObject slot in slot_obj)
                    slot.transform.SetAsLastSibling();
            }
        }
    }

    public RecordedVideoInfo GetPlayerEditing()
    {

        for(int i=0; i< slot_list.Count; i++)
        {
            if (slot_list[i].Info.is_player)
            {
                return slot_list[i].Info;
            }
        }

        return null;
    }
}
