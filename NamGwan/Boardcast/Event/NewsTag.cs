using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

[System.Serializable]
public class NewsTag : MonoBehaviour
{
    public List<GameObject> uptag = new List<GameObject>();  // Newspage 안에 있는 게임오브젝트 태그들 
    public List<GameObject> downtag = new List<GameObject>();
    Queue<string> randomTag = new Queue<string>(); //랜덤 태그를 발생시키기 위한 자료구조 

    public List<string> NewsUpTag = new List<string>(); // 현재 뉴스에 좋은 영향을 받고 있는 태그들 
    public List<string> NewsDownTag = new List<string>(); //현재 뉴스에 안좋은 영향을 받고 있는 태그들 

    
    public void SaveTag(string[] up , string [] down)
    {
        up[0] = NewsUpTag[0];
        up[1] = NewsUpTag[1];

        down[0] = NewsDownTag[0];
        down[1] = NewsDownTag[1];
    }
    public void LoadTag(string[] up, string[] down)
    {
        for(int i=0; i<up.Length;i++)
        {
            NewsUpTag.Add(up[i]);
            NewsDownTag.Add(down[i]);

            uptag[i].transform.Find("TitleText").GetComponent<Text>().text = up[i];

            TagInfo tempUp = DatabaseManager.SearchData(up[i], DatabaseManager.Instance.tag_list);
            tempUp.Popularity += 1;

            downtag[i].transform.Find("TitleText").GetComponent<Text>().text = down[i];
            TagInfo tempDown = DatabaseManager.SearchData(down[i], DatabaseManager.Instance.tag_list);
            tempDown.Popularity -= 0.5f;
        }
    }
    public void ResetTag() //일주일이 지났을떄 기존 태그들요소를 삭제해준다.
    {
        randomTag.Clear();
        NewsUpTag.Clear();
        NewsDownTag.Clear();
        DataLoader.LoadPrefabs<Tag, TagInfo>("Prefabs/Tag", DatabaseManager.Instance.tag_list);
    }
    void CopyRandomData(string[] data) //태그를 랜덤으로 만들어준다. 
    {
        for (List<string> temp = new List<string>(data); temp.Count > 0;)
        {
            int index = Random.Range(0, temp.Count);
            randomTag.Enqueue(temp[index]);
            temp.RemoveAt(index);
        }
    }
    public void SetNewsTag(NewsTag get)
    {
        uptag = get.uptag;
        downtag = get.downtag;
        NewsUpTag = get.NewsUpTag;
        NewsDownTag = get.NewsDownTag;
    }
    string GetNotOverlapTag(List<string> get) //태그를 중복이 안되게 처리해준다. 
    {
        string temp = "";
        do
        {
            temp = get[Random.Range(0, get.Count)];
        } while (randomTag.Contains(temp) || NewsDownTag.Contains(temp)); //태그가 중복되면 다시 받아온다.
        return temp;
    }

    public void OnNewsPage(string[] data)
    {
        ResetTag();
        CopyRandomData(data);
        string tag = "";

        // 인기도 하락 태그
        for (int i = 0; i < downtag.Count; i++)
        {
            tag = GetNotOverlapTag(BoardcastManager.Instance.data.con_tag); //모든 태그 요소를 넣어준다. 
            NewsDownTag.Add(tag);
            downtag[i].SetActive(true);
            downtag[i].transform.Find("TitleText").GetComponent<Text>().text = tag;

            // 태그 인기도 하락
            TagInfo temp = DatabaseManager.SearchData(tag, DatabaseManager.Instance.tag_list);
            temp.Popularity -= 0.5f;
        }

        // 인기도 상승 태그
        for (int i = 0; i < uptag.Count; i++)
        {
            tag = randomTag.Dequeue();
            NewsUpTag.Add(tag);
            uptag[i].SetActive(true);
            uptag[i].transform.Find("TitleText").GetComponent<Text>().text = tag;

            // 태그 인기도 상승
            TagInfo temp = DatabaseManager.SearchData(tag, DatabaseManager.Instance.tag_list);
            temp.Popularity += 1;
        }

        DatabaseManager.Instance.ObserverUpdate("Tag");
    }
}
