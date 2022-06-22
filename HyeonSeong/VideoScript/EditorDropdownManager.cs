using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorDropdownManager: MonoBehaviour, ISubjectUI
{
    //������ ��Ӵٿ�鿡�� ������Ʈ �޽����� �ѷ��ֱ� ���� Ŭ���� _ ����
    //VideoManager�� ��� �Ǿ��ִ�.
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
