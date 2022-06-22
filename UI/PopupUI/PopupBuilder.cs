using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PopupBuilder 
{
    private Transform target = null;

    //Build 메소드 호출할때 팝업창을 꾸며주기 위한 멤버변수
    private string title = null;
    private string description = null;
    private List<PopupButtonInfo> buttonInfoList = null;

    private string show_anim = "";
    private string destroy_anim = "";
    private bool is_auto_destory = false;

    private CallbackEvent callback;

    //생성자에서 부모타겟 매개변수로 가져온다. (보통 tmepCavas를 지정한다)
    public PopupBuilder(Transform _target)
    {
        this.target = _target;
        this.buttonInfoList = new List<PopupButtonInfo>();
    }

    //파업창 속성을 지정한 다음 최종적으로 처리하는 함수
    //팝업 프리펩 지정
    public void Build(string popupUI_prefab_name, float x = 0.0f, float y = 0.0f, float z = 0.0f)
    {
        //최종적으로 모든정보를 가지고 팝업창생성

        //monoBehaviour 제거로 인해 Instantiate 사용불가
        //프리팹생성을 위해 GameObject의 Static메소드로 호출
        GameObject popupObj = GameObject.Instantiate(Resources.Load("Prefabs/UI/Popup/" + popupUI_prefab_name), new Vector3(x,y,z), Quaternion.identity) as GameObject;

        //생성한 오브젝트에 부모 설정
        popupObj.transform.SetParent(this.target, false);

        //PopupUi 컴포넌트 가져오기
        PopupUI popupUI = popupObj.GetComponent<PopupUI>();

        //UI 설정 (초기화)
        popupUI.SetTitle(this.title);
        popupUI.SetDescription(this.description);
        popupUI.SetAnim(this.show_anim, this.destroy_anim, this.callback);
        popupUI.SetAutoDestory(this.is_auto_destory);
        popupUI.SetButtons(this.buttonInfoList);
        popupUI.ShowAnim();
    }

    public void SetTitle(string _title)
    {
        //타이틀 정보 초기화
        this.title = _title;
    }

    public void SetDescription(string _description)
    {
        //설명정보 초기화
        this.description = _description;
    }

    public void SetAnim(string _show, string _destory = "", CallbackEvent _callbackEvent = null)
    {
        show_anim = _show;
        destroy_anim = _destory;
        callback = _callbackEvent;
    }

    public void SetAutoDestroy(bool _auto)
    {
        is_auto_destory = _auto;
    }

    public void SetButton(string _text, CallbackEvent _callbackEvent = null)
    {
        //버튼정보 초기화 - 호출할때마다 버튼 하나씩 추가
        this.buttonInfoList.Add(new PopupButtonInfo(_text, _callbackEvent));
    }
}
