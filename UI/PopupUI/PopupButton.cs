using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupButton : MonoBehaviour
{
    [SerializeField]
    private Text buttonText = null;

    private GameObject target = null;
    private CallbackEvent callbackEvent = null;

    public void Init(string _text, CallbackEvent _callback, GameObject _target)
    {
        //초기화 - 매개변수로 받은 이름과 콜백함수로 클릭시 콜백함수를 호출해주는 팝업버튼
        this.buttonText.text = _text;
        this.callbackEvent = _callback;
        this.target = _target;

        GetComponent<Button>().onClick.AddListener(OnButton);
    }

    public void OnButton()
    {
        //초기화된 콜백함수 호출
        this.callbackEvent();

        //AudioManager.Instance.OnClickButton();

        target.GetComponent<PopupUI>().DestroyAnim();
    }

}
