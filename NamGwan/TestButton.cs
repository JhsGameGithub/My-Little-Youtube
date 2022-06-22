using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eventList
{
    BigMoneyObject,
    ClipObject,
    FunnyObject,
    HealingObject,
    HostingObject,
    OhbangObject,
    OverdoObject,
    PeaceObject,
    SayNameObject,
    SniperObject,
    EndBoardcastObject,
}
public class TestButton : MonoBehaviour
{

    public void TestButtonAction()
    {
        BoardcastManager.Instance.eventListener.Spawn("Prefabs/Boardcast/Event/OverdoObject");
    }
    public void SpawnButton(int i)
    {
        BoardcastManager.Instance.eventListener.Spawn("Prefabs/Boardcast/Event/"+(eventList)i);
    }
}
