using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_INFO_TYPE
{
    E_CONTENTS,
    E_FURNITURE,
    E_VIDEO_EDITOR,
    E_RECORDED_VIDEO,
    E_EDITED_VIDEO,
    E_ADD_CONTENTS,
}

// 창식
// 난 이제 안사용함;;
// 컴포넌트 부착식으로 바꿈
// GetComponent로 받아오는중
// infoFactory = GetComponent<AbsInfoListFactory>();
// sortFactory = GetComponent<AbsSortFactory>();

public class FactoryManager
{
    public AbsInfoListFactory info_factory;
    public AbsSortFactory sort_factory;

    //슬롯 정렬 팩토리 - 현성
    public SlotSortFactory slot_factory;

    public FactoryManager(E_INFO_TYPE type)
    {
        SelectFactory(type);
    }

    private void SelectFactory(E_INFO_TYPE type)
    {
        switch (type)
        {
            case E_INFO_TYPE.E_CONTENTS:
                info_factory = new ContentsInfoListFactory();
                sort_factory = new ContentsSortFactory();
                break;
            case E_INFO_TYPE.E_FURNITURE:
                info_factory = new FurnitureInfoListFactory();
                sort_factory = new FurnitureSortFactory();
                break;
            case E_INFO_TYPE.E_VIDEO_EDITOR:
                info_factory = new VideoEditorInfoListFactory();
                sort_factory = new VideoEditorSortFactory();
                break;
            case E_INFO_TYPE.E_RECORDED_VIDEO:
                info_factory = new RecordedVideoInfoListFactory();
                slot_factory = new RecordedVideoSortFactory();
                break;
            case E_INFO_TYPE.E_EDITED_VIDEO:
                info_factory = new EditedVideoInfoListFactory();
                slot_factory = new EditedVideoSortFactory();
                break;
            case E_INFO_TYPE.E_ADD_CONTENTS:
                info_factory = new ContentsInfoListFactory();
                sort_factory = new ContentsSortFactory();
                break;
        }
    }
}
