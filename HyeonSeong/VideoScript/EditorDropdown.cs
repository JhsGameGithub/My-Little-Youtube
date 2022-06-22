using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditorDropdown : MonoBehaviour, IObserverUI
{
    public Dropdown editor_list;

    public void ObserverUpdate(string message)
    {
        InitEditorDropdown();
    }

    public void InitEditorDropdown()
    {
        editor_list.options.Clear();

        // 플레이어가 직접 편집하는 경우
        if(DatabaseManager.Player.selfEditor.is_working == false)
        {
            Dropdown.OptionData temp = new Dropdown.OptionData();
            temp.text = DatabaseManager.Player.selfEditor.name;
            editor_list.options.Add(temp);
        }


        for (int i = 0; i < DatabaseManager.Instance.my_editor_list.Count; i++)
        {
            if (DatabaseManager.Instance.my_editor_list[i].is_working == false)
            {
                Dropdown.OptionData temp = new Dropdown.OptionData();
                temp.text = DatabaseManager.Instance.my_editor_list[i].name;
                editor_list.options.Add(temp);
            }
        }

        editor_list.RefreshShownValue();
    }
}
