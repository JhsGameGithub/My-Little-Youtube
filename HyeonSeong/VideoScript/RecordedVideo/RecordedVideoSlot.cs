using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecordedVideoSlot : Slot, IEditingVideo
{
    [Header("TextField")]
    public Text title_text;
    public Text contents_text;
    public Text date_text;
    public Text editor_text;
    public InputField title_input;

    [Header("FunnyField")]
    public int funny_int;
    public Sprite blanked_star_spr;
    public Image[] funny_image = new Image[5];
    public EditorDropdown editor_dropdown;


    [Header("ProgressField")]
    public Image progress_bar;

    public GameObject TagBoxParent;
    public RecordedVideoUI video_ui;
    private RecordedVideoInfo info;

    public RecordedVideoInfo Info
    {
        get { return info; }
    }

    private void Start()
    {
        VideoManager.Instance.dropdown_manager.AddObserver(editor_dropdown);
        editor_dropdown.InitEditorDropdown();
    }

    private void OnEnable()
    {
        editor_dropdown.InitEditorDropdown();
    }

    public override void SetInfo(Info copy)
    {
        if (info == null)
            info = new RecordedVideoInfo();

        info.SetInfo(copy);
    }

    public void SetInfo(RecordedVideoInfo copy)
    {
        info = copy;
    }

    // 일반 UI 초기화 - 현성
    public override void SetUI()
    {
        title_text.text = "제목 : ";
        editor_text.text = "편집자 : ";
        title_input.gameObject.SetActive(true);
        editor_dropdown.gameObject.SetActive(true);
        contents_text.text = "컨텐츠 : " + info.contents.name;
        date_text.text = "촬영일 : " + info.year.ToString() + "년 " + info.month.ToString() + "월 " + info.day.ToString() + "일";
        funny_int = info.funny;
        progress_bar.fillAmount = 0.0f;
        SetFunnyUI();
        SetTagBoxUI();
    }

    // 편집 UI 초기화 - 현성
    public void SetEditUI()
    {

        title_text.text = "제목 : " + info.title;
        editor_text.text = "편집자 : " + info.editor.name;
        //Debug.Log(title_text.text);
        //Debug.Log(editor_text.text);
        title_input.gameObject.SetActive(false);
        editor_dropdown.gameObject.SetActive(false);

        VideoManager.Instance.dropdown_manager.NotifyObservers();
    }

    // 유툽각 초기화 - 현성
    public void SetFunnyUI()
    {
        for (int i = funny_int; i < 5; i++)
        {
            funny_image[i].sprite = blanked_star_spr;
        }
    }

    // 슬롯 태그 초기화 - 현성
    public void SetTagBoxUI()
    {
        for (int i = 0; i < info.contents.con_tag.Length; i++)
        {
            GameObject obj = Instantiate(Resources.Load("Prefabs/UI/TagBoxUI"), new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            obj.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 180);
            obj.transform.SetParent(TagBoxParent.transform, false);
            obj.GetComponentInChildren<Text>().text = info.contents.con_tag[i];
        }

    }

    public bool Editing_Video(bool is_player_edit = false)
    {
        bool temp = info.EditingVideo(is_player_edit);
        progress_bar.fillAmount = (float)info.wait / info.max_wait;


        return temp;
    }

    public VideoEditorInfo Get_Editor_Info()
    {
        return info.editor;
    }

    //편집 버튼 _ 현성
    public void OnEditBtn()
    {
        //편집 전 영상에 대해서만 버튼 반응
        if (info.max_wait == 0)
        {

            if (editor_dropdown.editor_list.options.Count != 0)
            {
                string editor_name = editor_dropdown.editor_list.captionText.text;

                if (DatabaseManager.Player.selfEditor.name == editor_name)
                {
                    info.Editor = DatabaseManager.Player.selfEditor;
                    info.max_wait = DatabaseManager.Player.selfEditor.time * 12 * 60;
                    DatabaseManager.Player.selfEditor.is_working = true;
                    info.is_player = true;
                }
                else
                {
                    foreach (VideoEditorInfo editor in DatabaseManager.Instance.my_editor_list)
                    {
                        if (editor.name == editor_name)
                        {
                            info.Editor = editor;
                            info.max_wait = editor.time * 24 * 60;
                            editor.is_working = true;
                            info.is_player = false;
                            break;
                        }
                    }
                }

                if (title_input.text != "")
                    info.title = title_input.text;

                video_ui.editing_video_slots.Enqueue(this);
                SetEditUI();
            }
            else
            {
                //생성자의 매개변수로 팝업의 하이라키 위치 설정 -> 최상단 canvase
                PopupBuilder popupBuilder = new PopupBuilder(GameObject.Find("PopupCanvas").transform);
                popupBuilder.SetTitle("작업을 수행할 편집자가 없음!!");
                popupBuilder.SetDescription("더 많은 편집자를 고용하시거나\n 작업이 마무리될 때까지 기다려주세요");
                popupBuilder.SetAnim("FadeIn", "FadeOut");
                popupBuilder.SetAutoDestroy(true);
                popupBuilder.Build("BasePopupUI");
            }
        }
    }

    public void OnRemoveBtn()
    {
        if (info.max_wait == 0)
        {
            string date = info.year + "-" +
                info.month.ToString() + "-" +
                info.day.ToString() + "-" +
                info.hour.ToString();
            video_ui.Video_Remove(this);
            NotifyObservers();
        }
    }

    //편집중이었던 슬롯 불러오기
    public void EditingLoad()
    {
        string editor_name = editor_dropdown.editor_list.captionText.text;

        if (DatabaseManager.Player.selfEditor.name == info.editor.name)
        {
            // 이거 편집 끝나면 false
            DatabaseManager.Player.selfEditor.is_working = true;
            info.is_player = true;
        }
        else
        {
            foreach (VideoEditorInfo editor in DatabaseManager.Instance.my_editor_list)
            {
                if (editor.name == info.editor.name)
                {
                    editor.is_working = true;
                    info.is_player = false;
                    break;
                }
            }
        }

        progress_bar.fillAmount = (float)info.wait / info.max_wait;
        video_ui.editing_video_slots.Enqueue(this);
        SetEditUI();
    }
}
