using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditState : PlayerState
{
    public Image progressBar;
    public RecordedVideoUI recorded_video_ui;
    public float fatigue_value;

    private RecordedVideoInfo playerEditInfo;

    public override void StateEnter()
    {

        base.StateEnter();

        ProgressUI();

        // 시간 재생
        Clock.Instance.TimeNormalizer();
    }

    public override void StateUpdate()
    {
        check_time++;
        if (fatigue_time + DatabaseManager.SearchData("의자", DatabaseManager.Instance.my_furniture_list).skill[0].ability_value < check_time)
        {
            check_time = 0.0f;

            DatabaseManager.Player.status.HP = -(int)fatigue_value;

        }

        recorded_video_ui.EditingVideo(DatabaseManager.Player.selfEditor.is_working);
        ProgressUI();

        if (!DatabaseManager.Player.selfEditor.is_working)
        {
            // 시간 정지
            Clock.Instance.TimeStopper();


            AudioManager.Sound.Play("SE/Challenge", E_SOUND.SE);
            //생성자의 매개변수로 팝업의 하이라키 위치 설정 -> 최상단 canvase
            PopupBuilder popupBuilder = new PopupBuilder(GameObject.Find("PopupCanvas").transform);
            popupBuilder.SetTitle("편집 완료");
            popupBuilder.SetDescription("편집된 영상은 바로 업로드됩니다.");

            popupBuilder.SetButton("확인", () =>
            {
                Clock.Instance.state_machine.ChangeState("RestPlayerPage");
            });

            popupBuilder.Build("BasePopupUI");
        }

    }

    public override void StateExit()
    {
        base.StateExit();


    }

    public void ProgressUI()
    {
        playerEditInfo = recorded_video_ui.GetPlayerEditing();

        if (playerEditInfo != null)
            progressBar.fillAmount = (float)playerEditInfo.wait / playerEditInfo.max_wait;
    }
}
