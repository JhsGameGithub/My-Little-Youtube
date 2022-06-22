using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BoardcastDropdawn : MonoBehaviour
{
    public ChallengeSubject challenge_subject = new ChallengeSubject();
    public GameObject TagBoxParent;
    public Queue<GameObject> haveTags;
    Dropdown dropdown;

    void Start()
    {
        dropdown = GetComponent<Dropdown>();
        haveTags = new Queue<GameObject>();
        ChallengeBroker.Instance.Init_Subject(challenge_subject);
        //GetContens();
    }

    public void OnEnable()
    {
        GetContens();
    }

    void GetContens()
    {
        if (dropdown == null)
        {
            return;
        }

        dropdown.options.Clear(); //기존 리스트를 삭제해준다. 

        foreach (var data in DatabaseManager.Instance.my_contents_list) //데이터 베이스 메니저에 MyContens 리스트에 있는 목록을 가져와 등록한다. dropdown.value
        {
            Dropdown.OptionData temp = new Dropdown.OptionData();
            temp.text = data.name;
            dropdown.options.Add(temp);
        }
        dropdown.RefreshShownValue(); // 화면에 컨텐츠를 보이게한다. 
        UpdateTags();
    }
    public void UpdateTags() //다른 콘텐츠로 변경을 눌렀을경우 실행한다. 해당 함수에서 태그를 불러온다.
    {
        ContentsInfo find = DatabaseManager.Instance.my_contents_list.Find(x => x.name == dropdown.options[dropdown.value].text); //현재 선택되어 있는 컨텐츠를 가져온다.

        if (find == null)
        {
            Debug.Log("구입한 컨텐츠가 없음");
            return;
        }

        while (haveTags.Count > 0)//기존 태그를 삭제한다.
        {
            GameObject temp = haveTags.Dequeue();
            Destroy(temp);
        }

        foreach (var data in find.con_tag) //태그 불러오기 
        {
            GameObject obj = Instantiate(Resources.Load("Prefabs/UI/TagBoxUI"), new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            obj.transform.SetParent(TagBoxParent.transform, false);
            obj.GetComponentInChildren<Text>().text = data;
            haveTags.Enqueue(obj);
        }

    }

    public void SendInfo()
    {
        BoardcastManager.Instance.boardcastContens = DatabaseManager.Instance.my_contents_list.Find(x => x.name == dropdown.options[dropdown.value].text); //현재 선택되어 있는 컨텐츠를 가져온다.
        challenge_subject.challenge_handler(ChallengeBroker.Item.Broadcast, BoardcastManager.Instance.boardcastContens.name, 1);
    }
}
