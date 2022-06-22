using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ViewUpAnim : AbsViewAnim
{
    public ViewUpAnim(UIView _parent) : base(_parent)
    {

    }

    public override void ShowAnim()
    {
        uiview.CurViewState = ViewState.Appearing;

        rectTransform.position = new Vector3(0.0f, -10.0f, 90.0f);
        rectTransform.DOMoveY(0.0f, 0.1f)
            .OnComplete(() =>
            {
                uiview.CurViewState = ViewState.Appeared;
            });
    }

    public override void HideAnim()
    {
        rectTransform.position = startPosition;
        uiview.CurViewState = ViewState.Disappeared;
    }

}
