using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class TimeLineSlot : MonoBehaviour
{
    public E_TimeLine type;
    
    private Text text;
    private Color text_color;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponentInChildren<Text>();
        text_color = text.color;
        text_color.a = 0.0f;
        text.color = text_color;
    }

    public Sequence ShowAnim()
    {
        return DOTween.Sequence().OnStart(() =>
        {
            text_color.a = 0.0f;
            text.color = text_color;
        })
        .Join(text.DOFade(1.0f, 1.5f));
    }

    public Sequence HideAnim()
    {
        return DOTween.Sequence().OnStart(() =>
        {
            text_color.a = 1.0f;
            text.color = text_color;
        })
        .Join(text.DOFade(0.0f, 0.1f));
    }
}
