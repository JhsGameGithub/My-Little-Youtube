using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PageHideBtn : MonoBehaviour
{
    private Button btn;

    // Start is called before the first frame update
    void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(OnPageHide);
    }

    public void OnPageHide()
    {
        UINavigation.Instance.ViewPop();
        //AudioManager.Sound.Play("SE/button-3", E_SOUND.SE);
    }
}
