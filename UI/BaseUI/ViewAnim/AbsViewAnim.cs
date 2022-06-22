using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbsViewAnim
{
    protected UIView uiview;
    protected RectTransform rectTransform;
    protected Vector3 startPosition;

    public AbsViewAnim(UIView _parent)
    {
        Init(_parent);
    }

    protected virtual void Init(UIView _parent)
    {
        uiview = _parent;

        rectTransform = uiview.GetComponent<RectTransform>();
        startPosition = rectTransform.position;
    }

    public abstract void ShowAnim();

    public abstract void HideAnim();
}
