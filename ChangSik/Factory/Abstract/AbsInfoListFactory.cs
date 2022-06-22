using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public abstract class AbsInfoListFactory : MonoBehaviour
{
    public abstract List<Info> CreateInfo();
}
