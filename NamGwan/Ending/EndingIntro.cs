using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

public enum EndingList
{ 
    Clock, //처음에 나오는 클락 인트로 
    SILVER,//실버버튼 받았습니다. 구독자수 10만이상 
    GOLD,//골드버튼 받았습니다. 구독자수 100만이상
    BAD,//유튜브 접습니다.
}

public class EndingIntro : MonoBehaviour
{
    GameObject current;
    EndingList ending;
    AudioSource audiosrc;

    public const string SILVER = "엔딩 실버버튼 \n축하드립니다";
    public const string GOLD = "엔딩 골드버튼 \n축하드립니다";

    private void OnEnable()
    {
        AudioManager.Sound.BGMStop(); //사운드를 꺼준다.
        audiosrc = GetComponent<AudioSource>();
        GetComponent<Image>().color = new Color(0, 0, 0, 0);

        for(int i=0; i<transform.childCount;i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        StartIntro();
    }
   
    public void StartIntro()
    {
        GetComponent<Image>().DOFade(1, 2).OnComplete(() => {
            Branch(EndingList.Clock);
        });
    }
    public void EndIntro() //clock 이 끝났을때 
    {
        current.SetActive(false);
        Branch(ending);
    }
    public void SetEnding(EndingList list)
    {
        ending = list;
    }
    public void Branch(EndingList list)
    {
        switch (list)
        {
            case EndingList.Clock:
                current =transform.Find("IntroTurnUi").gameObject;
                break; 
            case EndingList.SILVER:
                current =transform.Find("EndingSilver").gameObject;
                current.GetComponent<EndingSilver>().SetEndingText(SILVER);
                break;
            case EndingList.GOLD:
                current = transform.Find("EndingGold").gameObject;
                current.GetComponent<EndingSilver>().SetEndingText(GOLD);
                break;
            case EndingList.BAD:
                current = transform.Find("EndingBad").gameObject;
                break;
            default:
                Debug.LogError("EndingIntro Branch is Not Do");
                break;
        }
        current.SetActive(true);
    }
    public void EffectText(Text text , float speed = 0.1f)
    {
        StartCoroutine(ShowText(text, speed));
    }
    IEnumerator ShowText(Text text,float speed = 0.1f)
    {
        string temp = text.text;

        text.text = "";

        foreach(char t in temp)
        {
            text.text += t;
            audiosrc.Play();
            yield return new WaitForSeconds(speed);
        }
    }
}