using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObserverData 
{
    public void ObserverUpdate(string message = "");
}
