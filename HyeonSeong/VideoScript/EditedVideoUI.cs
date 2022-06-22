using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditedVideoUI : VideoUI
{
    private List<EditedVideoInfo> info_list;
    private List<EditedVideoSlot> slot_list;
    public Queue<IProfitingVideo> profiting_video_slots = new Queue<IProfitingVideo>();

    private void Start()
    {
        Init();

        info_list = VideoManager.Instance.my_edited_list;
        slot_list = new List<EditedVideoSlot>();

        foreach (EditedVideoInfo info in info_list)
            Slot_Insert(info, true);

        foreach (EditedVideoSlot slot in slot_list)
            if (slot.Info.repeat != 43200)
                slot.ProfitingLoad();

        Clock.Instance.video_handler += new Clock.VideoHandler(ProfitingVideo);
    }

    public void Slot_Insert(EditedVideoInfo info, bool is_load)
    {
        GameObject slot_obj;
        if (slot_pool.Count == 0)
        {
            slot_obj = Instantiate(slot_prefab, new Vector3(0, 0, 0), Quaternion.identity);
            slot_obj.GetComponent<EditedVideoSlot>().video_ui = this;
        }
        else
        {
            slot_obj = slot_pool.Pop();
        }
        slot_obj.transform.SetParent(slot_parent.transform, false);

        EditedVideoSlot slot = slot_obj.GetComponent<EditedVideoSlot>();
        slot.SetInfo(info);
        slot.SetUI();
        slot_list.Add(slot);
        if (!is_load)
            profiting_video_slots.Enqueue(slot);
    }

    public void Video_Insert(RecordedVideoInfo recorded_video_info)
    {
        string current_date =
            DatabaseManager.Current_date.Year + "-" +
            DatabaseManager.Current_date.Month + "-" +
            DatabaseManager.Current_date.Day + "-" +
            DatabaseManager.Current_date.Hour + "-";

        EditedVideoInfo temp = new EditedVideoInfo(current_date, recorded_video_info);
        info_list.Add(temp);
        Slot_Insert(temp, false);
    }

    public void ProfitingVideo(bool is_temp = false)
    {
        for (int i = profiting_video_slots.Count; i > 0; i--)
        {
            IProfitingVideo temp = profiting_video_slots.Dequeue();
            if (temp.ProfitingVideo())
                continue;
            profiting_video_slots.Enqueue(temp);
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
                foreach (EditedVideoSlot slot in slot_list)
                    slot_obj.Add(slot.gameObject);
                slot_sort.SortType = temp.e_sort;
                slot_obj.Sort(slot_sort.Sort);
                foreach (GameObject slot in slot_obj)
                    slot.transform.SetAsLastSibling();
            }
        }
    }
}
