using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengeUI : MonoBehaviour
{
    //생성 슬롯 프리팹 이름
    [SerializeField]
    private string slot_name;

    private UIView uiview;
    [SerializeField]
    private GameObject slot_parent;
    //생성된 슬롯을 담는 리스트
    [SerializeField]
    private List<ChallengeSlot> slot_list = new List<ChallengeSlot>();
    private GameObject slot_prefab;

    public ChallengeObserver observer;

    private void Start()
    {
        List<int> achieve_list = new List<int>();
        //프리팹 불러오기 - 현성
        slot_prefab = Resources.Load("Prefabs/UI/Slot/" + slot_name) as GameObject;

        uiview = transform.parent.GetComponent<UIView>();

        foreach (ChallengeInfo info in DatabaseManager.Instance.my_challenge_list)
            Slot_Insert(info);
        observer.Init(slot_list);
        DatabaseManager.Player.InitSubject();
    }

    public void Slot_Insert(ChallengeInfo info)
    {
        GameObject prefab = Instantiate(slot_prefab, new Vector3(0, 0, 0), Quaternion.identity);
        ChallengeSlot slot = prefab.GetComponent<ChallengeSlot>();
        prefab.transform.SetParent(slot_parent.transform, false);
        slot.SetInfo(info);
        slot_list.Add(slot);
    }
}
