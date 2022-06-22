using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class RequiredData //JsonUtility 사용을 위해 만든 클래스 
{
    public RequiredData(Required required)
    {
        equipment = required.equipment;
    }
      public List<Equipment> equipment; 
}

public class Required : Observer //  장비와 관련된 클래스 
{
    private int requiredLevel; //장비 평균 레벨 
    public int RequiredLevel
    {
        get
        {
            AvgLevel();
            return requiredLevel;
        }
    }

    public List<Equipment> equipment = new List<Equipment>(); //보유 장비들을 리스트로 관리한다.

    public void SaveData() // 장비를 저장한다. 
    {
        string save = JsonUtility.ToJson(new RequiredData(DatabaseManager.Player.required), true);
        File.WriteAllText(DatabaseManager.Instance.GetPathObserver(ObserverVariety.REQUIRED), save);
    }
    public void LodeData() // 장비를 불러온다.
    {
        string file_name = DatabaseManager.Instance.GetPathObserver(ObserverVariety.REQUIRED);
        FileInfo file_info = new FileInfo(file_name);

        // 파일 존재 여부 확인
        if (file_info.Exists)
        {
            string load = File.ReadAllText(file_name);
            RequiredData get = JsonUtility.FromJson<RequiredData>(load);
            equipment = get.equipment;
        }
    }
    public void NewGame() // 처음 게임을 시작할떄 장비들을 등록한다.
    {
        equipment.Add(MakeEquipmentFactory(EquipmentType.CHAIR));
        equipment.Add(MakeEquipmentFactory(EquipmentType.COMPUTER));
        equipment.Add(MakeEquipmentFactory(EquipmentType.DESK));
        equipment.Add(MakeEquipmentFactory(EquipmentType.EXERCISE));
        equipment.Add(MakeEquipmentFactory(EquipmentType.BED));
   }
    public void AvgLevel() //가지고 있는 장비의 합을 더한다. 
    {
        int avg = 0;

        for (int i=0; i<equipment.Count;i++)
        {
            avg += equipment[i].level;
        }
     
        requiredLevel = avg / equipment.Count;
    }
    public override void Notify(Message msg) // 메세지를 받는다.
    {
        switch (msg)
        {
            case Message.NEW:
                NewGame();
                break;
            case Message.SAVE:
                SaveData();
                break;
            case Message.LOAD:
                LodeData();
                break;
            default:
                break;
        }
    }
    public Equipment MakeEquipmentFactory(EquipmentType makeType) //장비를 만든다
    {
        Equipment make = null;
        switch (makeType)
        {
            case EquipmentType.CHAIR:
                make = new Chair();
                break;
            case EquipmentType.COMPUTER:
                make = new Computer();
                break;
            case EquipmentType.DESK:
                make = new Desk();
                break;
            case EquipmentType.EXERCISE:
                make = new Exercise();
                break;
            case EquipmentType.BED:
                make = new Bed();
                break; 
        }
        return make;
    }
}