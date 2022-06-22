using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

public class Ohbang : MonoBehaviour //오뱅알 이벤트 녹화본이 5개짜리가 나온다.
{
    public Sequence mySequence;
    public GameObject starObject;
    public Text titleText;
    public Text inforText;

    public string getTile;
    public string getInfor;

    public const int GETSTARS = 5; 

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
        .Append(starObject.transform.GetChild(0).DOScale(1, 0.5f).SetEase(Ease.OutElastic))
        .Append(starObject.transform.GetChild(1).DOScale(1, 0.4f).SetEase(Ease.OutElastic))
        .Append(starObject.transform.GetChild(2).DOScale(1, 0.3f).SetEase(Ease.OutElastic))
        .Append(starObject.transform.GetChild(3).DOScale(1, 0.2f).SetEase(Ease.OutElastic))
        .Append(starObject.transform.GetChild(4).DOScale(1, 0.1f).SetEase(Ease.OutElastic)).AppendInterval(1.0f)
         .Append(GetComponent<CanvasGroup>().DOFade(0, 1f)).OnComplete(() => {
             Destroy(this.gameObject);
         });
        EventActive();
    }
    public void EventActive()
    {
        BoardcastManager.Instance.RecordingEvent(GETSTARS, true);
    }
}


