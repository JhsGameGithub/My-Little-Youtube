using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public abstract class Item : MonoBehaviour
{
    public Place place;
    public abstract void ChangeSetting();//화면이 바뀌면 다른 동작을 할수도있음 
    public abstract void UpdateItem(); //아이템 동작

}
[System.Serializable]
public class ItemBuyList //아이템을 산 목록  //JsonUtility 사용을 위해 만든 클래스  구매시 true 구매가 아닐시 false 
{
    public ItemBuyList(List<AddContentsInfo> get)
    {
        foreach (var i in get) //해당 단어가 들어가있다면 구매했다고 표시해줌 
        {
            if (i.itemType == FlexItemList.CAT)
            {
                cat = true;
            }
            else if (i.itemType == FlexItemList.APRETMENT)
            {
                apretment = true;
            }
            else if (i.itemType == FlexItemList.MAID)
            {
                maid = true;
            }
            else if (i.itemType == FlexItemList.PAINTING)
            {
                painting = true;
            }
            else if(i.itemType == FlexItemList.CAMERA)
            {
                camera = true;
            }
        }
    }
    public bool cat;
    public bool maid;
    public bool apretment;
    public bool painting;
    public bool camera;
}

public enum Place
{
    RESTPLACE, //휴식에만 있는아이템
    BOARDCAST,  //방송에만 있는아이템 
    ALL, //전부 해당됌 
}

public enum FlexItemList
{
    CAT,
    MAID,
    APRETMENT,
    PAINTING,
    CAMERA,
}
public class CharacterItem : MonoBehaviour, IObserverData
{
    public Sprite wallNew; //바뀌는 벽 이미지 

    public List<Item> have;

    public void Start()
    {
        DatabaseManager.Instance.AddObserver(this);

        LodeData();

        ChangeItem();
    }

    private void OnDestroy()
    {
        // 삭제 시 옵저버 해지
        DatabaseManager.Instance.RemoveObserver(this);
    }

    public void UpdateItem()
    {
        //ChangeItem();
        ItemAction();
    }
    public void ItemAction()
    {
        foreach (var i in have)
        {
            i.UpdateItem();
        }
    }
    public void AddItem(Item newItem)
    {
        if (have.Contains(newItem))
            return;

        have.Add(newItem);
        newItem.ChangeSetting();
    }
    public void TestAddItem() //임시로 확인용으로 만들었다 
    {
        AddContentsInfo test1 = new AddContentsInfo();
        test1.itemType = FlexItemList.CAT;

        AddContentsInfo test2 = new AddContentsInfo();
        test2.itemType = FlexItemList.PAINTING;

        AddContentsInfo test3 = new AddContentsInfo();
        test3.itemType = FlexItemList.APRETMENT;

        AddContentsInfo test4 = new AddContentsInfo();
        test4.itemType = FlexItemList.MAID;


        DatabaseManager.Instance.my_add_contents_list.Add(test1);
        DatabaseManager.Instance.my_add_contents_list.Add(test2);
        DatabaseManager.Instance.my_add_contents_list.Add(test3);
        DatabaseManager.Instance.my_add_contents_list.Add(test4);
    }
    public void ChangeItem()
    {
        //TestAddItem(); //저장과불러오기가 만들어지면 해당 함수를 지운다 .

        foreach (var i in DatabaseManager.Instance.my_add_contents_list)
        {
            GameObject findObj = null;

            if (i.itemType == FlexItemList.CAT)
            {
                findObj = gameObject.transform.Find("Cat").gameObject;
            }
            else if (i.itemType == FlexItemList.APRETMENT)
            {
                GameObject.Find("InGameCanvas").transform.Find("Wall").GetComponent<SpriteRenderer>().sprite = wallNew;
            }
            else if (i.itemType == FlexItemList.MAID)
            {
                if (gameObject.transform.Find("Maid") == null)
                    continue;

                findObj = gameObject.transform.Find("Maid").gameObject;

            }
            else if (i.itemType == FlexItemList.PAINTING)
            {
                if (gameObject.transform.Find("Painting") == null)
                    continue;

                findObj = gameObject.transform.Find("Painting").gameObject;

            }
            else if (i.itemType == FlexItemList.CAMERA)
            {
                if (gameObject.transform.Find("Camera") == null)
                    continue;

                findObj = gameObject.transform.Find("Camera").gameObject;

            }


            if (findObj != null)
            {
                findObj.SetActive(true);
            }
        }

    }
    public void OnEnable()
    {
        foreach (var i in have)
        {
            i.ChangeSetting();
        }
    }

    //밑에 부분은 저장과 불러오기 부분 나중에 데이터베이스 메니저에 추가하든 어떻게 하든 해서 추가하자 . 
    public void SaveData() // my_add_contents_list 에 데이터를 바탕으로 텍스트 파일 저장  
    {
        string save = JsonUtility.ToJson(new ItemBuyList(DatabaseManager.Instance.my_add_contents_list), true);
        File.WriteAllText(DatabaseManager.Instance.GetPathFlexItem(), save);
    }

    public void LodeData() // 불러온 텍스트 파일을 my_add_contents_list 에 넣어줌 
    {
        string file_name = DatabaseManager.Instance.GetPathFlexItem();
        FileInfo file_info = new FileInfo(file_name);

        // 파일 존재 여부 확인
        if (file_info.Exists)
        {
            string load = File.ReadAllText(file_name);

            if (load != "")
                InsertData(JsonUtility.FromJson<ItemBuyList>(load));
        }


    }
    private void InsertData(ItemBuyList data) //파일을 불러와서 데이터를 덮어씌운다.
    {
        if (data.apretment) //텍스트 파일내에 해당 데이터가 True일시 정보 불러옴 
        {
            DatabaseManager.Instance.my_add_contents_list.Add(Resources.Load<AddContents>("Prefabs/Shop/House").info);
        }
        if (data.cat)
        {
            DatabaseManager.Instance.my_add_contents_list.Add(Resources.Load<AddContents>("Prefabs/Shop/cat").info);
        }
        if (data.maid)
        {
            DatabaseManager.Instance.my_add_contents_list.Add(Resources.Load<AddContents>("Prefabs/Shop/maid").info);
        }
        if (data.painting)
        {
            DatabaseManager.Instance.my_add_contents_list.Add(Resources.Load<AddContents>("Prefabs/Shop/Picture").info);
        }
        if(data.camera)
        {
            DatabaseManager.Instance.my_add_contents_list.Add(Resources.Load<AddContents>("Prefabs/Shop/Camera").info);
        }
    }

    public void ObserverUpdate(string message = "")
    {
        if (message == "Save")
        {
            SaveData();
        }
        else if (message == "Load")
        {
            LodeData();
        }
        else if (message == "Update")
        {
            ChangeItem();
        }
    }
}
