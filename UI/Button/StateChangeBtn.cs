using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateChangeBtn : MonoBehaviour
{
    public string state_name = "";
    private Button btn;

    // Start is called before the first frame update
    void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(OnStateChange);
    }

    public void OnStateChange()
    {
        Clock.Instance.state_machine.ChangeState(state_name);
        AudioManager.Sound.Play("SE/button-3", E_SOUND.SE);
    }
}
