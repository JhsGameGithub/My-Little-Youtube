using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

public class HealingEvent : MonoBehaviour //힐-링 체력 +10 
{
    public Sequence mySequence;
    public GameObject characterObject;
    public Text titleText;
    public Text inforText;

    public string getTile;
    public string getInfor;

    public const int HEAL = 10;

    public void Start()
    {
        getTile = titleText.text;
        getInfor = inforText.text;
        Vector3 goal = characterObject.transform.position;
        goal.x += 50;

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
        .Join(characterObject.transform.DOLocalMove(new Vector3(goal.x,goal.y,goal.z),1f))
        .Append(characterObject.transform.DOLocalMove(new Vector3(goal.x+100f, goal.y, goal.z), 2.5f))
        .Append(GetComponent<CanvasGroup>().DOFade(0, 1f)).OnComplete(() => {
            Destroy(this.gameObject);
        });
        EventActive();
    }
    public void EventActive()
    {
        DatabaseManager.Player.status.HP = HEAL;
    }

}


