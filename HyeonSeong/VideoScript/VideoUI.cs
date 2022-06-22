using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class VideoUI : MonoBehaviour
{
    [SerializeField]
    private string slot_name;


    [SerializeField]
    protected E_INFO_TYPE e_info_type;


    protected Button[] sort_btns;
    private UIView uiview;


    public GameObject sort_parent;

    public GameObject slot_parent;

    public GameObject slot_prefab;

    protected SlotSort slot_sort;


    public SlotPool slot_pool;


    protected void Init()
    {
        slot_prefab = Resources.Load("Prefabs/UI/Slot/" + slot_name) as GameObject;

        uiview = transform.parent.GetComponent<UIView>();

        slot_pool.Init();
    }
    
    public void OnEnable()
    {
        slot_parent.GetComponent<RectTransform>().localPosition = new Vector3(432.0f, -900.0f);
    }
}
