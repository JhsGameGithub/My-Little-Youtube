using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PageChangeBtn : MonoBehaviour
{
    public string page_name;

    private Button btn;

    // Start is called before the first frame update
    void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(OnPageChange);
    }

    public void OnPageChange()
    {
        UINavigation.Instance.ViewChange(page_name);
        AudioManager.Sound.Play("SE/button-3", E_SOUND.SE);     
    }
}
