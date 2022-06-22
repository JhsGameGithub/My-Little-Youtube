using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

//팝업창 객체가 들고있는 클래스 builder에서 가져온 정보로 팝업창을 꾸며준다.
public class PopupUI : MonoBehaviour
{
    //파넬 오브젝트
    [SerializeField]
    private GameObject panel;

    //제목 text 오브젝트
    [SerializeField]
    private Text titleText = null;

    //설명 text 오브젝트
    [SerializeField]
    private Text descriptionText = null;

    //버튼 생성시 버튼들의 부모, 레이아웃을 사용해 생성시 마다 위치를 잡아준다
    [SerializeField]
    private GameObject btnFiled = null;

    //버튼 프리팹
    [SerializeField]
    private GameObject buttonPrefab = null;

    private string show_anim = "";

    private string destroy_anim = "";

    private CallbackEvent callback = null;

    public void SetTitle(string _title)
    {
        this.titleText.text = _title;
    }

    public void SetDescription(string _description)
    {
        this.descriptionText.text = _description;
    }

    public void SetAnim(string _show, string _destroy, CallbackEvent _callbackEvent = null)
    {
        show_anim = _show;
        destroy_anim = _destroy;
        callback = _callbackEvent;

    }

    public void SetButtons(List<PopupButtonInfo> _popupButtonInfos)
    {
        //버튼 초기화
        foreach (var info in _popupButtonInfos)
        {
            //버튼 동적생성
            GameObject buttonObject = Instantiate(this.buttonPrefab);
            buttonObject.transform.SetParent(this.btnFiled.transform, false);

            PopupButton popupButton = buttonObject.GetComponent<PopupButton>();
            popupButton.Init(info.text, info.callback, this.gameObject);
        }
    }

    //팝업등장 - 팝업창생성시 확대되는 느낌같은 연출 넣기에 좋다
    public virtual void ShowAnim()
    {
        switch (show_anim)
        {
            case "FadeIn":

                // 알파 값 0으로 초기화
                Image panel_image = panel.transform.GetComponent<Image>();
                panel_image.color = new Color(panel_image.color.r, panel_image.color.g, panel_image.color.b, 0.0f);
                titleText.color = new Color(titleText.color.r, titleText.color.g, titleText.color.b, 0.0f);
                descriptionText.color = new Color(descriptionText.color.r, descriptionText.color.g, descriptionText.color.b, 0.0f);

                panel_image.DOFade(1, 0.4f);
                titleText.DOFade(1, 0.6f);
                descriptionText.DOFade(1, 0.6f);
                break;

            case "MoveUp":
                panel.transform.DOLocalMoveY(50.0f, 0.8f).SetEase(Ease.OutExpo);
                break;
        }
    }

    public virtual void DestroyAnim()
    {
        switch (destroy_anim)
        {
            case "FadeOut":
                Image panel_image = panel.transform.GetComponent<Image>();


                panel_image.DOFade(0, 0.6f);
                titleText.DOFade(0, 0.4f);
                descriptionText.DOFade(0, 0.4f).OnComplete(() =>
                {
                    StartCoroutine(CorDestroy(0.3f));
                });
                break;

            default:
                PopupDestroy();
                break;
        }
    }

    public void SetAutoDestory(bool _auto)
    {
        if (_auto)
        {
            StartCoroutine(AutoDestroyAnim());
        }
    }

    IEnumerator AutoDestroyAnim()
    {
        yield return new WaitForSeconds(1.5f);

        DestroyAnim();
    }

    IEnumerator CorDestroy(float time)
    {
        yield return new WaitForSeconds(time);

        PopupDestroy();

    }
    public void PopupDestroy()
    {
        if (callback != null)
            callback();

        Destroy(this.gameObject);
    }
}
