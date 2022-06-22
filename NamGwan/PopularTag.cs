using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PopularTag : MonoBehaviour
{
    public GameObject TagBoxParent;



    private void OnEnable() //오브젝트가 활성화되면 뉴스에서 인기태그를 불러온다.
    {
        if (BoardcastManager.Instance.news == null)
            return;
        if (BoardcastManager.Instance.news.NewsUpTag.Count <= 0)
            return;

        for(int i=0; i< TagBoxParent.transform.childCount;i++)
        {
            Destroy(TagBoxParent.transform.GetChild(i).gameObject);
        }
        foreach (var data in BoardcastManager.Instance.news.NewsUpTag) //태그 불러오기 
        {
            GameObject obj = Instantiate(Resources.Load("Prefabs/UI/TagBoxUI"), new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            obj.transform.SetParent(TagBoxParent.transform, false);
            obj.GetComponentInChildren<Text>().text = data;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
