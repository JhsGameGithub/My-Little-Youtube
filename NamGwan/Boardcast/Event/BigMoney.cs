using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

public class BigMoney : MonoBehaviour //트위치계의 큰손 1,000,000 후원을 해준다.
{
    public Sequence mySequence;
    public GameObject coinObject;
    public Text titleText;
    public Text inforText;
    public Text moneyText;
    public Text wonText;

    public string getTile;
    public string getInfor;
    public string getMoney;
    public string getWon;

    public const int MONEY= 1000000;

    public void Start()
    {
        getTile = titleText.text;
        getInfor = inforText.text;
        getMoney = moneyText.text;
        getWon = wonText.text;
        GetComponent<CanvasGroup>().alpha = 0;
        mySequence = DOTween.Sequence().OnStart(() =>
        {
            transform.localScale = Vector3.zero;
            titleText.text = "";
            inforText.text = "";
            moneyText.text = "";
            wonText.text = "";
        }).Append(transform.DOScale(1, 1).SetEase(Ease.OutBounce))
        .Join(GetComponent<CanvasGroup>().DOFade(1, 1)).SetDelay(0.5f)
        .Append(titleText.DOText(getTile, 1f))
        .Join(inforText.DOText(getInfor, 1f))
        .Join(coinObject.transform.DORotate(new Vector3(0, 180, 0), 1.5f))
       .Join(moneyText.DOText(getMoney, 1f))
       .Append(wonText.DOText(getWon, 0.5f))
        .Join(coinObject.transform.DORotate(new Vector3(0, 0, 0), 1.5f))
        .Append(GetComponent<CanvasGroup>().DOFade(0, 1f)).OnComplete(() => {
            Destroy(this.gameObject);
        });
        EventActive();
    }
    public void EventActive()
    {
        DatabaseManager.Player.wallet.Money = MONEY;
    }

}


