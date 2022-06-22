using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending : MonoBehaviour
{
    public GameObject endingCanvas;

    public const int SILVERENDING = 100000;
    public const int GOLDENDING = 1000000;

    public void EndingButton()
    {
        endingCanvas.SetActive(true);
        Branch();
    }

    public void Branch() //엔딩 분기 
    {
        if(DatabaseManager.Player.status.Subscriber>= GOLDENDING) //구독자 100만이상
        {
            endingCanvas.GetComponent<EndingIntro>().SetEnding(EndingList.GOLD);
        }
        else if(DatabaseManager.Player.status.Subscriber > SILVERENDING)//구독자 10만이상 
        {
            endingCanvas.GetComponent<EndingIntro>().SetEnding(EndingList.SILVER);
        }
        else if(DatabaseManager.Player.status.Subscriber < SILVERENDING)//구독자 10만 미만 
        {
            endingCanvas.GetComponent<EndingIntro>().SetEnding(EndingList.BAD);
        }

    //    endingCanvas.GetComponent<EndingIntro>().SetEnding(EndingList.BAD); // 임시 테스트용 
    }
    private void Start()
    {
       // StartCoroutine(TestStart());
    }

    IEnumerator TestStart()
    {
        yield return new WaitForSeconds(1.0f);
        EndingButton();
    }
}
