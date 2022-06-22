using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BoardcastEndButton : MonoBehaviour
{

    private Button btn;

    // Start is called before the first frame update
    void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(OnPageChange);
        btn.gameObject.SetActive(false);
    }

    public void OnPageChange()
    {
        AudioManager.Sound.Play("SE/button-3", E_SOUND.SE);
        
        BoardcastManager.Instance.EndButtonClick();
    }
}
