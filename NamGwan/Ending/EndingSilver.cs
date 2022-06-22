using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

public enum EndingSound
{ 
    BGM = 0 ,
    EFFECT,
    DEFUALT,
}

public class EndingSilver : MonoBehaviour
{
    AudioSource[] source = new AudioSource[(int)EndingSound.DEFUALT];

    public AudioClip background;
    public AudioClip click;

    public List<GameObject> branch;

    GameObject cusor;
    GameObject goal;

    Text text;

    public Sequence mySequence;
    

    public  string select;
    public void SetEndingText(string text)
    {
        select = text;
    }
    private void OnEnable()
    {
        SettingAudio();
        SearchBranch();
        FirstBranch();
    }
    private void SearchBranch()
    {
        for(int i=0; i<transform.childCount;i++)
        {
            branch.Add(transform.GetChild(i).gameObject);
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }
    private void SettingAudio()
    {
        source[(int)EndingSound.BGM] = gameObject.AddComponent<AudioSource>();
        source[(int)EndingSound.EFFECT] = gameObject.AddComponent<AudioSource>();
        source[(int)EndingSound.BGM].clip = background;
        source[(int)EndingSound.EFFECT].clip = click;

        source[(int)EndingSound.BGM].volume = AudioManager.Sound.GetVolume(E_SOUND.BGM);
        source[(int)EndingSound.EFFECT].volume = AudioManager.Sound.GetVolume(E_SOUND.SE);
    }
    void FirstBranch()
    {
        branch[0].SetActive(true);

        PlaySound(EndingSound.BGM);

        cusor = branch[0].transform.Find("cursor").gameObject;
        goal = branch[0].transform.Find("Youtube").gameObject;

        cusor.transform.DOMove(goal.transform.position, 2f).OnComplete(() =>
        {

            PlaySound(EndingSound.EFFECT);

            cusor.GetComponent<Image>().DOColor(new Color(0.5f, 0.5f, 0.5f, 1f), 0.3f).OnComplete(()=> {
                SecondBranch();
            });
        });
    }
    void SecondBranch()
    {
        branch[1].SetActive(true);
        branch[1].transform.Find("Video").gameObject.SetActive(false);
        SecondOpenYoutube();
    }
    void SecondOpenYoutube()
    {
        mySequence = DOTween.Sequence().OnStart(() =>
        {
            branch[1].transform.localScale = Vector3.zero;

            cusor = branch[1].transform.Find("cursor").gameObject;
            goal = branch[1].transform.Find("Frame1").gameObject;

        }).Append(branch[1].transform.DOScale(1, 1)).OnComplete(() => {
            SecondClickYoutube();
        });
    }
    void SecondClickYoutube()
    {
        cusor.transform.DOMove(goal.transform.position, 2f).OnComplete(() =>
      {
          PlaySound(EndingSound.EFFECT);
          cusor.GetComponent<Image>().DOColor(new Color(0.5f, 0.5f, 0.5f, 1f), 0.3f).OnComplete(() =>
          {
              cusor = branch[1].transform.Find("Video").gameObject;
              cusor.SetActive(true);    
              cusor.transform.DOMove(goal.transform.position, 1f).OnComplete(() => 
              {
                  ShowText();
                  StartCoroutine(ChangeToTileScene());
              });
          });
      });
    }
    void ShowText()
    {
        text = cusor.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>();
        text.text = select;
        GameObject.Find("EndingCanvas").GetComponent<EndingIntro>().EffectText(text);
    }
    void PlaySound(EndingSound src)
    {
        source[(int)src].Play();
    }
    IEnumerator ChangeToTileScene()
    {
        yield return new WaitForSeconds(5.0f);
        DOTween.KillAll();

        GameSceneManager.Instance.OnTitle();
    }
}

