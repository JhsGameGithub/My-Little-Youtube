using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseUI : MonoBehaviour, IObserverUI
{
    //생성 슬롯 프리팹 이름
    [SerializeField]
    private GameObject slot_prefab;

    //정렬 토글 부모
    [SerializeField]
    private GameObject sort_parent;
    private Button[] sort_btns;
    private InfoSort info_sort;

    //UIView 애니메이션 상태 확인을 위해 필요
    private UIView uiview;

    //슬롯 부모
    [SerializeField]
    private GameObject slot_parent;

    //생성된 슬롯을 담는 리스트
    [SerializeField]
    private List<Slot> slot_list = new List<Slot>();

    [SerializeField]
    private List<Info> info_list = new List<Info>();

    // 데이터 세팅 알고리즘 팩토리
    private AbsInfoListFactory infoFactory;

    // 슬롯 정렬 알고리즘 팩토리
    private AbsSortFactory sortFactory;

    protected void Start()
    {
        uiview = transform.parent.GetComponent<UIView>();

        infoFactory = GetComponent<AbsInfoListFactory>();
        sortFactory = GetComponent<AbsSortFactory>();

        InfoListInit();
        SortInit();
        sort_btns = sort_parent.GetComponentsInChildren<Button>();
    }

    // 오브젝트 활성화시 슬롯 초기화
    public void OnEnable()
    {
        InfoListInit();

        SlotsInit();
    }

    public void OnDisable()
    {
        SlotsClear();
    }

    //옵저버 업데이트 (데이터 변화에 따른 업데이트)
    public void ObserverUpdate(string message)
    {
        InfoListInit();

        OnSortButton(null);

        if (message != "not_slot_init")
        {
            SlotsInit();
        }
    }

    // 데이터 초기화
    private void InfoListInit()
    {
        if (infoFactory != null)
        {
            info_list.Clear();
            info_list = infoFactory.CreateInfo();
        }
    }

    private void SortInit()
    {
        if (sortFactory != null)
        {
            info_sort = sortFactory.CreateSort();
        }
    }

    // 각 슬롯 오브젝트 생성 및 초기화 
    private void SlotsInit()
    {
        if (this.gameObject.activeInHierarchy)
        {
            StartCoroutine(CoSlotInit());
        }
    }

    // 각 슬롯 오브젝트 삭제
    private void SlotsClear()
    {
        for (int i = 0; i < slot_list.Count; i++)
        {
            if (slot_list[i] != null)
            {

                Destroy(slot_list[i].gameObject);
            }
        }

        slot_list.Clear();
    }
    private IEnumerator CoSlotInit()
    {
        SlotsClear();

        //무슨 코드지? - 현성
        if (uiview != null)
        {
            while (uiview.CurViewState == ViewState.Appearing || uiview.CurViewState == ViewState.Disappearing)
            {
                yield return null;
            }
        }

        for (int i = 0; i < info_list.Count; i++)
        {
            GameObject obj = Instantiate(slot_prefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            obj.transform.SetParent(slot_parent.transform, false);
            obj.GetComponent<Slot>().SetInfo(info_list[i]);
            obj.GetComponent<Slot>().AddObserver(this);
            slot_list.Add(obj.GetComponent<Slot>());
        }

        // 스크롤 위치 초기화
        slot_parent.transform.position = new Vector3(0.0f, 0.0f, 90.0f);
    }

    // 버튼 클릭 이벤트 (하나의 이벤트만 활성화)
    public void OnSortButton(SortButton select)
    {
        info_sort.SortType = E_SORT.DEFAULT;

        for (int i = 0; i < sort_btns.Length; i++)
        {
            SortButton temp = sort_btns[i].GetComponent<SortButton>();

            if (select == null || temp.e_sort != select.e_sort)
            {
                if (temp.isSelect == true)
                    temp.OnSelectBtn();
            }
            else
            {
                info_sort.SortType = temp.e_sort;
                info_list.Sort(info_sort.Sort);
                SlotsInit();
            }
        }
    }

}
