using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ViewAnimFactory : MonoBehaviour
{
    public abstract AbsViewAnim CreateViewAnim(UIView _parent);
}
