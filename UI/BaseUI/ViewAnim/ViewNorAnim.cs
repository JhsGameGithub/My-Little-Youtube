using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewNorAnim : AbsViewAnim
{
    public ViewNorAnim(UIView _parent) : base(_parent)
    {

    }

    public override void ShowAnim()
    {
        rectTransform.position = uiview.transform.parent.position;
        uiview.CurViewState = ViewState.Appeared;
    }

    public override void HideAnim()
    {
        rectTransform.position = startPosition;
        uiview.CurViewState = ViewState.Disappeared;
    }
}
