using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentsInfoListFactory : AbsInfoListFactory
{
    public override List<Info> CreateInfo()
    {
        List<Info> temp_list = new List<Info>();

        foreach (ContentsInfo all_contents_data in DatabaseManager.Instance.contents_list)
        {
            if (DatabaseManager.SearchData(all_contents_data.name, DatabaseManager.Instance.my_contents_list) == null)
            {
                temp_list.Add(new ContentsInfo(all_contents_data));
            }
        }

        return temp_list;
    }
}
