using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPTestUI : MonoBehaviour
{
    public Image hp_bar;
    public Text hp;

    // Update is called once per frame
    void Update()
    {
        hp_bar.fillAmount = (float) DatabaseManager.Player.status.HP / (float) DatabaseManager.Player.status.MaxHP;
        hp.text =  DatabaseManager.Player.status.HP + " / " +  DatabaseManager.Player.status.MaxHP;
    }
}
