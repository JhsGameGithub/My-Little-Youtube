using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GotoEditState : MonoBehaviour
{
    public string state_name = "";
    private Button btn;

    // Start is called before the first frame update
    void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(OnStateChange);
    }

    public void OnStateChange()
    {

        if (DatabaseManager.Player.selfEditor.is_working)
        {
            UINavigation.Instance.ViewPop();
            Clock.Instance.state_machine.ChangeState(state_name);
        }
        else
        {
            //생성자의 매개변수로 팝업의 하이라키 위치 설정 -> 최상단 canvase
            PopupBuilder popupBuilder = new PopupBuilder(GameObject.Find("PopupCanvas").transform);
            popupBuilder.SetTitle("등록된 영상 없음!!");
            popupBuilder.SetDescription("먼저 편집할 영상을 등록해주세요.");

            popupBuilder.SetButton("확인", () =>
            {
                UINavigation.Instance.ViewPush("RecordedVideoPage");
            });

            popupBuilder.SetButton("취소");


          popupBuilder.Build("BasePopupUI");
        }





        AudioManager.Sound.Play("SE/button-3", E_SOUND.SE);
    }
}
