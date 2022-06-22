using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBarUI : MonoBehaviour, IObserverUI
{
    public Image hp_image;
    public Text hp_text;

    private void Start()
    {
        DatabaseManager.Player.status.AddObserver(this);
        ObserverUpdate();
    }

    private void OnDestroy()
    {
        DatabaseManager.Player.status.RemoveObserver(this);
    }

    public void ObserverUpdate(string message = "")
    {
        int curHp = DatabaseManager.Player.status.HP;
        int maxHp = DatabaseManager.Player.status.MaxHP;

        Hp_Update(curHp, maxHp);
    }

    public void Hp_Update(int current, int max)
    {
        hp_image.fillAmount = (float)current / max;
        hp_text.text = current + " / " + max;
    }

}
