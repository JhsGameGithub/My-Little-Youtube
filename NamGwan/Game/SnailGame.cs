using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

enum BoosterPer
{
    First = 1,
    Second = 2,
    Thrid = 3,

    SIZE,
}

public class SnailGame : MonoBehaviour
{
    public List<GameObject> snailObject = new List<GameObject>();
    public List<Vector2> initpos = new List<Vector2>();

    public GameObject goal;

    public bool endGame = false;

    public GameObject winSnail;

    public bool StartGame = false;

    public GameObject selectSnail;

    public GameObject cusor;

    public SnailGameEvent gameEvent;
    public bool IsGame //현재 게임을 하고있는가 확인함
    {
        get
        {
            return StartGame;
        }
    }
    public void OnEnable()
    {
        SpeedSet();
    }
    private void Start()
    {
        foreach (var s in snailObject)
        {
            initpos.Add(s.transform.localPosition);
        }
        gameEvent = GetComponent<SnailGameEvent>();
    }
    void InitPos()
    {
        for(int i=0; i<initpos.Count;i++)
        {
            snailObject[i].transform.localPosition = initpos[i];
        }
    }
    void Racing()
    {
        if(StartGame==true && endGame == false)
        {
            foreach (var s in snailObject)
            {
                s.GetComponent<Snail>().RaceStart(goal);
            }
            gameEvent.ActionEvent();
        }
    }

    public void FinishSnail(GameObject temp) //경주가 끝났을때 
    {
        if(endGame == false)
        {
            winSnail = temp;
            endGame = true;

            foreach (var s in snailObject)
            {
                s.GetComponent<Snail>().RaceEnd();
            }
            WinGameScreen();
        }
    }
    public void WinGameScreen() //이긴 화면을 띄어준다.
    {
        GameObject eg = transform.Find("EndGame").GetComponent<EndGame>().gameObject;
        eg.SetActive(true);
        eg.GetComponent<EndGame>().ResultPrint(winSnail, selectSnail);
    }
    public void ResetGame()
    {
        winSnail = null;
        endGame = false;
        InitPos();
      
    }
    public void SpeedSet()
    {
        snailObject[0].GetComponent<Snail>().SetSnail(0.14f,
          Random.Range(20, 31));
        snailObject[1].GetComponent<Snail>().SetSnail(0.28f,
            Random.Range(10, 21));
        snailObject[2].GetComponent<Snail>().SetSnail(0.42f,
                    Random.Range(5, 11));
    }
    public void GameStart()
    {
        ResetGame();
        if (selectSnail == null)
            SelectSnail();

        StartGame = true;
        AudioManager.Sound.Play("SE/snailStart", E_SOUND.SE);
    }
    public void SelectSnail(GameObject getSnail = null)
    {
        if (IsGame == true)
            return;

        if(getSnail==null) //처음시작할떄 자동으로 선택 
        {
            selectSnail=snailObject[0];
        }
        else //만약 플레이어가 선택을 했다면 
        {
            selectSnail = getSnail;
            AudioManager.Sound.Play("SE/snailSelect", E_SOUND.SE);
        }
        cusor.transform.SetParent(selectSnail.transform);
        cusor.transform.localPosition = new Vector2(0, 108);
    }
    public void NewGame()
    {
        ResetGame();
        gameEvent.NewGame();
        SpeedSet();
        StartGame = false;
    }
    // Update is called once per frame
    void Update()
    {
        Racing();
    }

}
