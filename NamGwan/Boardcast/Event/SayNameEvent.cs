using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

public class SayNameEvent : MonoBehaviour //뇌절채팅 체력 - 10 
{
    public Sequence mySequence;
    public GameObject characterObject;
    public Text titleText;
    public Text inforText;

    public string getTile;
    public string getInfor;

    public const int LOST = -10;

    public void Start()
    {
        getTile = titleText.text;
        getInfor = inforText.text;
        Vector3 goal = characterObject.transform.position;
        goal.y += 100f;
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
        .Join(characterObject.transform.DOShakePosition(3f,50)).AppendInterval(1.0f)
        .Append(GetComponent<CanvasGroup>().DOFade(0, 1f)).OnComplete(() => {
         Destroy(this.gameObject);
      });
        EventActive();
    }
    public void EventActive()
    {
        DatabaseManager.Player.status.HP = LOST;
    }

}



//SayNameEvent