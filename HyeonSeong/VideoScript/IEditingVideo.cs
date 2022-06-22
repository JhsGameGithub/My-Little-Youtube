using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEditingVideo
{
    public bool Editing_Video(bool is_player_edit = false);
    public VideoEditorInfo Get_Editor_Info();
}
