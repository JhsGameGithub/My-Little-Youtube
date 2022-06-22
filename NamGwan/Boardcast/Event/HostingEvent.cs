using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

public class HostingEvent : MonoBehaviour //난하~ 시청자 +50% 
{
    public Sequence mySequence;
    public GameObject characterObject;
    public Text titleText;
    public Text inforText;

    public string getTile;
    public string getInfor;

    public const int ADDVIEWRS = 50;

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
        .Append(titleText.DOText(getTile, 1f))
        .Join(inforText.DOText(getInfor, 1f))
        .Append(characterObject.transform.DORotate(new Vector3(0, 180, 0), 1.5f))
        .Append(characterObject.transform.DORotate(new Vector3(0, 0, 0), 1.5f))
         .Append(GetComponent<CanvasGroup>().DOFade(0, 1f)).OnComplete(() => {
             Destroy(this.gameObject);
         });
        EventActive();
    }
    public void EventActive()
    {
        BoardcastManager.Instance.viewers.ViewersEvent(ADDVIEWRS, true);
    }

}


