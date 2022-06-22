using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContinueStartBtn : MonoBehaviour
{
    private Button continue_btn;

    // Start is called before the first frame update
    void Start()
    {
        continue_btn = GetComponent<Button>();
        continue_btn.onClick.AddListener(GameSceneManager.Instance.OnContinue);
    }

}
