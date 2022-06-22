using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
public class Snail : MonoBehaviour
{
    public float speed;
    public float perBoost;

    public bool isStart = false;
    public bool isBoost = false;
    public bool gollin = false;
    Animator animator;

    public Text detailText;

    public void Start()
    {
        animator = GetComponent<Animator>();
        detailText = transform.Find("detail").GetComponent<Text>();
    }

    public void SetSnail(float _speed, float _perBoost)
    {
        speed = _speed;
        perBoost = _perBoost;
        isBoost = false;
        gollin = false;
        isStart = false;
        gameObject.SetActive(true);
        if (detailText == null)
            return;
        detailText.text = "이동속도 : " + speed*10 + "\n부스터확률 : " + perBoost + "%";
    }

    public void RaceStart(GameObject goal)
    {
        if(isStart == false)
        {
            detailText.text = "";
            isStart = true;
            animator.SetBool("Move", true);
            StartCoroutine(Booster());
        }
       gameObject.transform.Translate(new Vector3
           (goal.transform.position.x,
           0)
           * speed * Time.deltaTime);
    
            GoalIn(goal);
    }
    public void DieSnail()
    {
        perBoost = 0;
        speed = 0;
        gameObject.SetActive(false);
    }
    public void RaceEnd()
    {
        animator.SetBool("Move", false);
    }
    public void GoalIn(GameObject goal)
    {
        if (gameObject.transform.position.x >= goal.transform.position.x)
        {
            GameObject.Find("SnailGame").transform.Find("Game").GetComponent<SnailGame>().FinishSnail(gameObject);
        }
    }
    IEnumerator Booster()
    {
        yield return new WaitForSeconds(1f);

        int randPer = Random.Range(0, 100);

        if(perBoost >= randPer)
        {
            speed = 1f;
            isBoost = true;
        }
        if(gollin==false && isBoost ==false)
        {
            StartCoroutine(Booster());
        }
    }
}
