using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//팝업창에 생기는 버튼이벤트를 위한 델리게이트 콜백함수
public delegate void CallbackEvent();

public class PopupButtonInfo 
{
    //버튼정보를 들고 있는 클래스 - Builder에서 popup객체로 정보를 보낼때 사용
    public string text = null;
    public CallbackEvent callback = null;

    public PopupButtonInfo(string _text, CallbackEvent _callback)
    {
        this.text = _text;
        this.callback = _callback == null ? () => { } : _callback;
    }
}
