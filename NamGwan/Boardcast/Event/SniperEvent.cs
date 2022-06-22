using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

public class SniperEvent : MonoBehaviour //저격 녹화본 별 1개 감소 
{
    public Sequence mySequence;
    public GameObject characterObject;
    public Text titleText;
    public Text inforText;

    public string getTile;
    public string getInfor;

    public const int STARTRDELETE = 1;

    public void Start()
    {
        getTile = titleText.text;
        getInfor = inforText.text;
        Vector3 goal = characterObject.transform.position;
        goal.y -= 600;
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
        .Append(characterObject.transform.DOScale(1, 1f).SetEase(Ease.OutElastic)).AppendInterval(1.0f)
        .Append(GetComponent<CanvasGroup>().DOFade(0, 1f)).OnComplete(() => {
            Destroy(this.gameObject);
        });
        EventActive();
    }
    public void EventActive()
    {
        BoardcastManager.Instance.RecordingEvent(STARTRDELETE, false);
    }

}
