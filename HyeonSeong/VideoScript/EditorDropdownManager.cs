using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorDropdownManager: MonoBehaviour, ISubjectUI
{
    //편집자 드롭다운들에게 업데이트 메시지를 뿌려주기 위한 클래스 _ 현성
    //VideoManager에 등록 되어있다.
    public List<IObserverUI> observer_list;

    public void AddObserver(IObserverUI observer)
    {
        if (observer_list == null)
            observer_list = new List<IObserverUI>();

        observer_list.Add(observer);
    }

    public void RemoveObserver(IObserverUI observer)
    {
        if (observer_list == null)
            return;

        observer_list.Remove(observer);
    }

    public void NotifyObservers(string message = "")
    {
        if (observer_list == null)
            return;

        foreach (IObserverUI observer in observer_list)
        {
            observer.ObserverUpdate(message);
        }
    }
}
