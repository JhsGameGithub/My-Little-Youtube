using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

public class Peace : MonoBehaviour //아무일도 일어나지 않았다 이벤트 
{
    public Sequence mySequence;
    public Text titleText;
    public Text inforText;

    public string getTile;
    public string getInfor;


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
    }
}

