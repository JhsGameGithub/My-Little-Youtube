using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureInfoListFactory : AbsInfoListFactory
{
    public override List<Info> CreateInfo()
    {
        List<Info> temp_list = new List<Info>();

        string _name = "";
        int _level = 0;

        for (int i = 0; i < DatabaseManager.Instance.my_furniture_list.Count; i++)
        {
            _name = DatabaseManager.Instance.my_furniture_list[i].name;
            _level = DatabaseManager.Instance.my_furniture_list[i].level;


            FurnitureInfo temp = DatabaseManager.Instance.SearchFurnitureData(_name, _level);

            if (temp != null)
            {
                if (temp.name == "포션" || temp.name == "페이스 크림"  || temp.level < DatabaseManager.Instance.GetFurnitureMaxLevel(temp.name))
                {
                    temp_list.Add(new FurnitureInfo(temp));
                }
            }
        }

        return temp_list;
    }
}
