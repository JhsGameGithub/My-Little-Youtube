using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;


public class Donaiton : MonoBehaviour
{
    public GameObject coin;
    public GameObject moneyText;
    public List<GameObject> fadeBox;
    // Start is called before the first frame update
    void Start()
    {
        Fade(0, 0);
        Action();
    }
    public void SetMoney(int money)
    {
        moneyText.GetComponent<Text>().text = string.Format("{0:#,0}", money);
        UpdateMoney(money);
    }
    public void UpdateMoney(int money)
    {
         DatabaseManager.Player.wallet.Money = money;
    }
    void Fade(float value , float durate)
    {
        foreach(var obj in fadeBox)
        {

            if (obj.GetComponent<Image>() != null)
            {
                obj.GetComponent<Image>().DOFade(value, durate);
            }
            else if (obj.GetComponent<Text>() != null)
            {
                obj.GetComponent<Text>().DOFade(value, durate);
            }
            else if (obj.GetComponent<SpriteRenderer>() != null)
            {
                obj.GetComponent<SpriteRenderer>().DOFade(value, durate);
            }

        }
    }
    void Action()
    {
        Vector3 goal = transform.position;
        goal.y -= 1f;

        Fade(1, 2);
        transform.DOMove(goal, 1.0f).OnComplete(() => {
            coin.transform.DORotate(new Vector3(0, 180, 0), 1.5f).OnComplete(() =>
              {
                  coin.transform.DORotate(new Vector3(0, 0, 0), 1.5f).OnComplete(() =>
                  {
                      Fade(0, 1);
                      goal.y += 1f;
                      transform.DOMove(goal, 1.0f).OnComplete(() => {
                          DestroyObject();
                          });
                  });
              });
        });
      
    }
    public void DestroyObject()
    {
        Destroy(this.gameObject);
    }
}
