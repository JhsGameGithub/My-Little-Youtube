using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Intro_0 : MonoBehaviour
{
    public InputField input_field;
    public GameObject intro_1;
    public Text nick_text;

    private string input_name = "";

    private void Start()
    {
        nick_text.text = "";

    }

    private void Update()
    {
        if (CheckNickName(input_field.text))
        {
            nick_text.text = "";
        }
        else
        {
            nick_text.text = "한글만 입력이 가능합니다";
        }
    }

    public void InputName()
    {
        if (CheckNickName(input_field.text))
        {
            //생성자의 매개변수로 팝업의 하이라키 위치 설정 -> 최상단 canvase
            PopupBuilder popupBuilder = new PopupBuilder(GameObject.Find("PopupCanvas").transform);

            // 팝업창에 들어갈 내용
            popupBuilder.SetTitle("이 이름으로 할까요??");

            // 확인 버튼에 람다식으로 이벤트 작성
            popupBuilder.SetButton("네", () =>
            {
                AudioManager.Sound.Play("SE/button-3", E_SOUND.SE);

                input_name = input_field.text;

                DatabaseManager.Instance.BeginDataLoad();
                DatabaseManager.Player.status.Name = input_name;
                DatabaseManager.Instance.DataSave();

                gameObject.SetActive(false);
                intro_1.SetActive(true);
            });

            // 취소 버튼
            popupBuilder.SetButton("아니요", () =>
             {
                 AudioManager.Sound.Play("SE/button-3", E_SOUND.SE);
             });

            // 최종적으로 생성 (프리팹 이름)
            popupBuilder.Build("BasePopupUI");
        }
    }


    // 한글만 입력 가능하게
    private bool CheckNickName(string word)
    {
        return System.Text.RegularExpressions.Regex.IsMatch(word, "^[가-힣]*$");
    }
}
