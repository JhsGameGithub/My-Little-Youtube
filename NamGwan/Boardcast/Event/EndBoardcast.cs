using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

public class EndBoardcast : MonoBehaviour //방송이 끝나고 녹화본 생성 
{
    public Sequence mySequence;
    public GameObject starEmpty;  
    public GameObject starGet;
    public Text titleText;

    public string getTile;


    public void Start()
    {
        getTile = titleText.text;

        GetComponent<CanvasGroup>().alpha = 0;
        mySequence = DOTween.Sequence().OnStart(() =>
        {
            transform.localScale = Vector3.zero;
            titleText.text = "";
        }).Append(transform.DOScale(1, 1).SetEase(Ease.OutBounce))
        .Join(GetComponent<CanvasGroup>().DOFade(1, 1))
        .Append(titleText.DOText(getTile, 1f));
        StarAction();
    }
    public void StarAction()
    {

        StartCoroutine(StarSound(mySequence.Duration(),false));
        mySequence
        .Append(starEmpty.transform.GetChild(0).DOScale(1, 0.5f).SetEase(Ease.OutElastic))
        .Append(starEmpty.transform.GetChild(1).DOScale(1, 0.4f).SetEase(Ease.OutElastic))
        .Append(starEmpty.transform.GetChild(2).DOScale(1, 0.3f).SetEase(Ease.OutElastic))
        .Append(starEmpty.transform.GetChild(3).DOScale(1, 0.2f).SetEase(Ease.OutElastic))
        .Append(starEmpty.transform.GetChild(4).DOScale(1, 0.1f).SetEase(Ease.OutElastic))
        .AppendInterval(0.5f);
        StartCoroutine(StarSound(mySequence.Duration()));
         StarGet();
    }
    public void StarGet()
    {
        for (int i=0; i<BoardcastManager.Instance.recordingCount;i++)
        {
            mySequence.Append(starGet.transform.GetChild(i).DOScale(1, 0.5f).SetEase(Ease.OutElastic));      
        }
        mySequence.Append(GetComponent<CanvasGroup>().DOFade(0, 1f)).OnComplete(() => {
                SendMessageBoardcast();
                Destroy(this.gameObject);
            });

        }
    public void SendMessageBoardcast()
    {
        GameObject.Find("InGameCanvas").GetComponent<StateMachine>().ChangeState("RestPlayerPage"); //왜여기서 OutBoardcast 의  OutBoardcast 실행하냐..
    }
    IEnumerator StarSound(float startTime, bool IsGold =true)
    {
        yield return new WaitForSeconds(startTime);
        if (IsGold)
        {
           
            for (int i = 0; i < BoardcastManager.Instance.recordingCount; i++)
            {
                AudioManager.Sound.OverridePlay("SE/GetCoin", E_SOUND.SE);
                yield return new WaitForSeconds(0.5f);
            }
        }
       else
        {
            float delaySound = 0.5f; 
            for(int i=0; i<5;i++)
            {
                AudioManager.Sound.OverridePlay("SE/jap", E_SOUND.SE);
                yield return new WaitForSeconds(delaySound);
                delaySound -= 0.1f;
            }

        }
    }
}



//EndBoardcast