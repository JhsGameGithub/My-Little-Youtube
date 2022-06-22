using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.IO;

[System.Serializable]
public class SaveNews //아이템을 산 목록  //JsonUtility 사용을 위해 만든 클래스  구매시 true 구매가 아닐시 false 
{
    public SaveNews(ContentsInfo set, NewsTag get)
    {
        select = set;
        up = new string[2];
        down = new string[2];
        get.SaveTag(up,down);

    }
    public ContentsInfo select;
    public string[] up;
    public string[] down;
}


public class NewsEvent : MonoBehaviour
{
    public ContentsInfo select; //랜덤으로 하나의 게임을 가져오기 위한 변수 
    public GameObject icon;
    public Text detail;
    public NewsTag tags; //여기에 뉴스에 영향을 받는 태그들이 있음 

   
    public void SaveData() // my_add_contents_list 에 데이터를 바탕으로 텍스트 파일 저장  
    {
        string save = JsonUtility.ToJson(new SaveNews(select,tags), true);
        File.WriteAllText(DatabaseManager.Instance.GetPathNews(), save);
    }
    public bool LodeData() // 불러온 텍스트 파일을 my_add_contents_list 에 넣어줌 
    {
        string file_name = DatabaseManager.Instance.GetPathNews();
        FileInfo file_info = new FileInfo(file_name);

        // 파일 존재 여부 확인
         if (file_info.Exists)
        {
            string load = File.ReadAllText(file_name);
            InsertData(JsonUtility.FromJson<SaveNews>(load));
            return true;
        }
        return false;
    }
    public void InsertData(SaveNews get)
    {
        select = get.select;
        tags.LoadTag(get.up, get.down);
        
        icon.GetComponent<Image>().sprite = select.icon;
        detail.text = "최근'" + select.name + "' (이)가 큰 인기를 끌고 있습니다!";
    }


    

    public void DayEvent() //7일 마다 해당함수를 실행시킨다. 랜덤으로 이벤트를 가져온다.
    {
        select.SetInfo(DatabaseManager.Instance.contents_list[Random.Range(0, DatabaseManager.Instance.contents_list.Count)]);
        OpenNews();
        SaveData();
    }
    void OpenNews() //이벤트 실행 
    {
        icon.GetComponent<Image>().sprite = select.icon;
        detail.text = "최근'" + select.name + "' (이)가 큰 인기를 끌고 있습니다!";
        tags.OnNewsPage(select.con_tag);
    }



    void Start()
    {
        select = new ContentsInfo();
        tags = transform.GetComponent<NewsTag>();
        if(LodeData()==false) //불러올 뉴스가없을떄 
        {
            DayEvent();
        }
    }
}
