using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ViewState
{
    Appearing,
    Appeared,
    Disappearing,
    Disappeared,
}

public class UIView : MonoBehaviour
{
    private ViewAnimFactory animFactory;

    private AbsViewAnim viewAnim;

    private ViewState curViewState = ViewState.Disappeared;

    public ViewState CurViewState { get => curViewState; set => curViewState = value; }

    // Start is called before the first frame update
    void Start()
    {
        animFactory = GetComponent<ViewAnimFactory>();

        viewAnim = animFactory == null ? new ViewNorAnim(this) : animFactory.CreateViewAnim(this);
        
        StartCoroutine(StartCor());
    }

    IEnumerator StartCor()
    {
        yield return new WaitForSeconds(0.2f);
        gameObject.SetActive(false);
    }

    public void Show()
    {
        ShowAnim();
        gameObject.SetActive(true);

    }

    public void Hide()
    {
        HideAnim();
        gameObject.SetActive(false);
    }

    protected virtual void ShowAnim()
    {
        viewAnim.ShowAnim();
    }

    protected virtual void HideAnim()
    {
        viewAnim.HideAnim();
    }
}
