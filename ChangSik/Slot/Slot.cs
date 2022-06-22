using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Slot : MonoBehaviour, ISubjectUI
{
    protected List<IObserverUI> observer_list;

    public abstract void SetInfo(Info copy);

    public abstract void SetUI();


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

    // 돈 차감 팝업창
    public void PricePopup(int price)
    {
        //돈 차감 팝업창
        PopupBuilder popupBuilder = new PopupBuilder(GameObject.Find("PopupCanvas").transform);
        popupBuilder.SetDescription(ColorText(price.ToString(), "#FF3333"));
        popupBuilder.SetAnim("MoveUp");
        popupBuilder.SetAutoDestroy(true);
        popupBuilder.Build("TextPopupUI", 325.0f, -650.0f);
    }

    // 돈이 부족할 때 나오는 팝업창
    public void NotBuyPopup()
    {
        //생성자의 매개변수로 팝업의 하이라키 위치 설정 -> 최상단 canvase
        PopupBuilder popupBuilder = new PopupBuilder(GameObject.Find("PopupCanvas").transform);
        popupBuilder.SetDescription("돈이 부족합니다!!");
        popupBuilder.SetAnim("FadeIn", "FadeOut");
        popupBuilder.SetAutoDestroy(true);
        popupBuilder.Build("BasePopupUI");
    }

    public string ColorText(string data, string color)
    {
        return "<color=" + color + ">" + data + "</color>";
    }

    public string GetThousandCommaText(int data)
    {
        return string.Format("{0:#,0}", data);
    }

}
