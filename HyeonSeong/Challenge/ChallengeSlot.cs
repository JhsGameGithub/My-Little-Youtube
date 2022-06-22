using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChallengeSlot : Slot
{
    public Text title_text;
    public Image tropi_img;
    public Image ui_img;
    public Sprite tropi_source;
    public Sprite ui_source;

    private ChallengeInfo _info;

    public ChallengeInfo info
    {
        get { return _info; }
        set { _info = value; }
    }


    public override void SetInfo(Info copy)
    {
        if (_info == null)
            _info = new ChallengeInfo();

        info.SetInfo(copy);
        title_text.text = _info.title;
    }
    public void SetInfo(ChallengeInfo copy)
    {
        _info = copy;
        title_text.text = _info.title;
    }
    public override void SetUI()
    {
        ui_img.sprite = ui_source;
        tropi_img.sprite = tropi_source;
    }
}
