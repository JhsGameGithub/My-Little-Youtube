using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

public class EndingClock : MonoBehaviour
{
     Text turnText;
     AudioSource audioSrc; 

    private void OnEnable()
    {
        turnText = transform.GetChild(0).GetComponent<Text>();
        audioSrc = GetComponent<AudioSource>();
        StartCoroutine(TurnChange());
    }
    IEnumerator TurnChange()
    {
        audioSrc.Play();
        turnText.text = "D + 364";
        yield return new WaitForSeconds(1.0f);
        audioSrc.Play(); 
        turnText.text = "D + 365";
        yield return new WaitForSeconds(1.0f);
        transform.parent.GetComponent<EndingIntro>().EndIntro();
    }
   
}
