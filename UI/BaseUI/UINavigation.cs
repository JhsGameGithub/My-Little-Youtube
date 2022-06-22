using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UINavigation : MonoBehaviour
{
    #region 싱글톤
    private static UINavigation instance = null;

    public static UINavigation Instance
    {
        get
        {
            if (instance == null)
            {
                //var obj = FindObjectOfType<UINavigation>();

                //instance = (obj != null) ? obj : new GameObject().AddComponent<UINavigation>();

                return null;
            }

            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    #endregion

    //일단 임시로 스택으로 구현.. 
    private Stack<UIView> viewStack = new Stack<UIView>();
    public UIView currentView = null;
    
    public string start_view_name = "";

    public void Start()
    {
        StartCoroutine(StartCor());
    }

    IEnumerator StartCor()
    {
        yield return new WaitForSeconds(0.2f);

        if (start_view_name != "")
        {
            ViewPush(start_view_name);
        }
    }

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
    public UIView Push(string viewName)
    {
        //if (currentView != null && currentView.name == viewName && currentView.gameObject.activeSelf) return currentView;

        var page = GameObject.Find(this.gameObject.name).transform.Find(viewName);


        if (page != null)
        {
            viewStack.Push(page.GetComponent<UIView>());

            currentView = viewStack.Peek();

            currentView.Show();
        }


        return currentView;
    }

    //스택 Pop ->  view는 Hide
    public UIView Pop()
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
