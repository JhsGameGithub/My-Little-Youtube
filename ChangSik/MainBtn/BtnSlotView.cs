using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class BtnSlotView : MonoBehaviour
{
    protected RectTransform rectTransform;

    private Button[] btns;

    private Sequence show_sequence;
//    private Sequence hide_sequence;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        btns = GetComponentsInChildren<Button>();


        // 활성시 보여주는 트윈 애니메이션
        show_sequence = DOTween.Sequence().SetAutoKill(false);
        show_sequence.Append(ShowSlotSequence());
        ShowBtnInit();

        // 비활성시 보여주는 트윈 애니메이션
        //hide_sequence = DOTween.Sequence().SetAutoKill(false);
        //hide_sequence.Append(HideSlotSequence());
        //HideBtnInit();

        gameObject.SetActive(false);
    }

    public void Show()
    {
        ShowAnim();
        gameObject.SetActive(true);

    }

    public void Hide()
    {

        HideAnim();
        gameObject.SetActive(false);
    }

    protected void ShowAnim()
    {
        show_sequence.Restart();
    }

    protected void HideAnim()
    {
        //hide_sequence.Restart();
    }

    private void ShowBtnInit()
    {
        for (int i = 0; i < btns.Length; i++)
        {
            show_sequence.Append(btns[i].transform.DOScale(new Vector3(1.0f, 1.0f, 1.0f), 0.05f));
        }
    }

    //private void HideBtnInit()
    //{
    //    for(int i=0; i < btns.Length; i++)
    //    {
    //        hide_sequence.Append(btns[i].transform.DOScale(new Vector3(0.0f, 0.0f, 0.0f), 0.05f));
    //    }
    //}

    private Sequence ShowSlotSequence()
    {
        return DOTween.Sequence().OnStart(() =>
        {
            for (int i = 0; i < btns.Length; i++)
            {
                btns[i].transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
            }

            rectTransform.sizeDelta = new Vector2(0.0f, rectTransform.sizeDelta.y);

        })
        .Append(rectTransform.DOSizeDelta(new Vector2(660.0f, rectTransform.sizeDelta.y), 0.2f));
    }

    //private Sequence HideSlotSequence()
    //{
    //    return DOTween.Sequence().OnStart(() =>
    //    {
    //        for (int i = 0; i < btns.Length; i++)
    //        {
    //            btns[i].transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
    //        }

    //        rectTransform.sizeDelta = new Vector2(660.0f, rectTransform.sizeDelta.y);
    //    })
    //    .Append(rectTransform.DOSizeDelta(new Vector2(0.0f, rectTransform.sizeDelta.y), 0.2f));
    //}

}
