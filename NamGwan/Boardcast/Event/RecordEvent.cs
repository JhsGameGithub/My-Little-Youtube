using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

public class RecordEvent : MonoBehaviour
{
    public Sequence mySequence;
  
    public void Start()
    {
        GetComponent<CanvasGroup>().alpha = 0;
        mySequence = DOTween.Sequence();

        mySequence.Append(GetComponent<CanvasGroup>().DOFade(1, 1))
        .Join(gameObject.transform.DOLocalMoveY(150, 1f))
        .AppendInterval(1.5f)
        .Append(gameObject.transform.DOLocalMoveY(300, 1f))
        .Join(GetComponent<CanvasGroup>().DOFade(0, 1)).OnComplete(() =>
         {
             Destroy(gameObject);
         });
    }
    
}
