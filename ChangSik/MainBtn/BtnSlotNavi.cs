using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnSlotNavi : MonoBehaviour
{
    //일단 임시로 스택으로 구현.. 
    private Stack<BtnSlotView> viewStack = new Stack<BtnSlotView>();
    public BtnSlotView currentView = null;

    public string start_view_name = "";


    public void ViewPush(string viewName)
    {
        Push(viewName);
    }

    public void ViewPop()
    {
        Pop();
    }

    //스택에서 Pop and Push, 단순 페이지 전환에 사용
    public void ViewChange(string objectName)
    {
        Pop();

        Push(objectName);
    }

    //스택 Push -> view는 Show
    public BtnSlotView Push(string viewName)
    {
        //if (currentView != null && currentView.name == viewName && currentView.gameObject.activeSelf) return currentView;

        var page = GameObject.Find(this.gameObject.name).transform.Find(viewName);


        if (page != null)
        {
            viewStack.Push(page.GetChild(0).GetComponent<BtnSlotView>());

            currentView = viewStack.Peek();

            currentView.Show();
        }


        return currentView;
    }

    //스택 Pop ->  view는 Hide
    public BtnSlotView Pop()
    {
        if (viewStack.Count != 0)
        {
            currentView.Hide();

            viewStack.Pop();

            currentView = viewStack.Count == 0 ? null : viewStack.Peek();
        }

        return currentView;
    }
}
