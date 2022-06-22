using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

public class Overdo : MonoBehaviour //불타는 채팅창 
{
    public Sequence mySequence;
    public Text titleText;
    public Text inforText;

    public string getTile;
    public string getInfor;

    public const int REMOVE = 30;


    public void Start()
    {
        getTile = titleText.text;
        getInfor = inforText.text;
        GetComponent<CanvasGroup>().alpha = 0;
        mySequence = DOTween.Sequence().OnStart(() =>
        {
            transform.localScale = Vector3.zero;
            titleText.text = "";
            inforText.text = "";
        }).Append(transform.DOScale(1, 1).SetEase(Ease.OutBounce))
        .Join(GetComponent<CanvasGroup>().DOFade(1, 1))
        .Append(titleText.DOText(getTile, 1.5f))
        .Join(inforText.DOText(getInfor, 1.5f)).AppendInterval(1.0f)
         .Append(GetComponent<CanvasGroup>().DOFade(0, 1f)).OnComplete(() => {
            Destroy(this.gameObject);
        });
        EventActive();
    }
    public void EventActive()
    {
        BoardcastManager.Instance.viewers.ViewersEvent(REMOVE, false);
    }

}

