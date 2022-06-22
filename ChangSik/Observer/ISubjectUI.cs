using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISubjectUI
{
    public void AddObserver(IObserverUI observer);
    public void RemoveObserver(IObserverUI observer);
    public void NotifyObservers(string message = "");
}
