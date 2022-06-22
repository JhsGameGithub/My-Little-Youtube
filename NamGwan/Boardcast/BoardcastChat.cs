using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BoardcastChat : MonoBehaviour
{
    public GameObject layoutGroup;
    public Queue<GameObject> chatLog;
    public float nowHeight;
    public float maxHeight;

    IEnumerator SpawnBox(string send)
    {
        yield return new WaitForSeconds(0.1f);
        GameObject obj = Instantiate(Resources.Load("Prefabs/UI/TextBox"), new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        obj.transform.SetParent(layoutGroup.transform, false);
        obj.transform.GetChild(0).GetComponent<ChatDetail>().Setting(send);
        chatLog.Enqueue(obj);
        nowHeight += obj.GetComponent<RectTransform>().sizeDelta.y;
        IsFull();

    }
    public void Spawn(string sendMessage)
    {
        StartCoroutine(SpawnBox(sendMessage));
    }
    public void IsFull()
    {
        while(nowHeight > maxHeight)
        {
            GameObject temp = chatLog.Dequeue();
            nowHeight -= temp.GetComponent<RectTransform>().sizeDelta.y;
            Destroy(temp);
        }
    }
    public void Start()
    {
        chatLog = new Queue<GameObject>();
        nowHeight = 0;
        maxHeight = GetComponent<RectTransform>().sizeDelta.y;
    }

}
