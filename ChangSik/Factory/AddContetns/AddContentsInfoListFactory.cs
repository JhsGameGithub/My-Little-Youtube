using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddContentsInfoListFactory : AbsInfoListFactory
{
    List<Info> temp_list = new List<Info>();

    public override List<Info> CreateInfo()
    {
        foreach(AddContentsInfo all_add_con_data in DatabaseManager.Instance.add_contents_list)
        {
            if(DatabaseManager.SearchData(all_add_con_data.name, DatabaseManager.Instance.my_add_contents_list) == null)
            {
                temp_list.Add(new AddContentsInfo(all_add_con_data));
            }
        }

        return temp_list;
    }
}
