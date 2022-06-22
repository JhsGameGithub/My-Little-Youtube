using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeginStartBtn : MonoBehaviour
{
    private Button begin_btn;

    // Start is called before the first frame update
    void Start()
    {
        begin_btn = GetComponent<Button>();
        begin_btn.onClick.AddListener(GameSceneManager.Instance.OnBegin);
    }

}
