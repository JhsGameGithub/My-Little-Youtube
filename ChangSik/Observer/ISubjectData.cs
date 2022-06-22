using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISubjectData 
{
    public void AddObserver(IObserverData observer);
    public void RemoveObserver(IObserverData observer);
    public void NotifyObservers(string message = "");
}
