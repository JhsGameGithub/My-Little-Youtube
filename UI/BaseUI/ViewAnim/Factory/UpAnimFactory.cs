using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpAnimFactory : ViewAnimFactory
{
    public override AbsViewAnim CreateViewAnim(UIView _parent)
    {
        return new ViewUpAnim(_parent);
    }
}
