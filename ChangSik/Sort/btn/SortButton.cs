using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum E_SORT
{
    DEFAULT = -1,
    LEVEL = 0,
    PRICE = 1,
    POPULARITY = 2,
    ABILITY,
    DATE,//날짜_현성
    FUNNY,//유튭각_현성
    GOOD,//좋아요_현성
    VIEW,//조회수_현성
    SIZE,
}

public class SortButton : MonoBehaviour
{
    public Button button;
    public Image checkmark;
    public E_SORT e_sort;
    public bool isSelect;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnSelectBtn);
        checkmark.gameObject.SetActive(false);
    }

    public void OnSelectBtn()
    {
        isSelect = !isSelect;
        checkmark.gameObject.SetActive(isSelect);
    }

}
