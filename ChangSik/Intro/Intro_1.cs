using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

// 인트로 연출 1번
// 놀고만 있는 주인공과
// 그걸 탐착치 않은 엄마
// 말풍선 출력

public class Intro_1 : MonoBehaviour
{
    public CanvasGroup player_speech;
    public CanvasGroup mom_speech;

    public GameObject intro_2;

    private Text player_text;
    private TextMeshProUGUI[] mom_texts;

    private float move_val = 200.0f;
    private float fade_time = 0.5f;


    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Sound.Play("BGM/DSi Shop Theme High Quality", E_SOUND.BGM);

        player_speech.alpha = 0.0f;
        player_text = player_speech.GetComponentInChildren<Text>();

        mom_speech.alpha = 0.0f;
        mom_texts = mom_speech.GetComponentsInChildren<TextMeshProUGUI>();

        string user_name = DatabaseManager.Player.status.Name;
        foreach (TextMeshProUGUI temp in mom_texts)
        {
            temp.text = "야!!\n" + user_name + "!";
        }

        StartCoroutine(PlayerSpeech());
    }

    IEnumerator PlayerSpeech()
    {
        yield return new WaitForSeconds(1.0f);

        player_speech.transform.DOLocalMoveY(move_val, fade_time).OnStart(() =>
        {
            player_speech.alpha = 0.0f;
            player_text.text = "";
        });

        player_speech.DOFade(1.0f, fade_time).OnComplete(() =>
        {
            player_text.DOText("ㅎㅎ 게임 개꿀잼~", 1.0f).OnComplete(() =>
            {
                StartCoroutine(MomSpeech());
            });

        });
    }

    IEnumerator MomSpeech()
    {
        yield return new WaitForSeconds(1.0f);

        AudioManager.Sound.BGMStop();
        AudioManager.Sound.Play("SE/Punch_Sound_Effect", E_SOUND.SE);

        mom_speech.DOFade(1.0f, 0.1f);
        mom_speech.transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutBack, 5.0f)
            .OnComplete(() =>
            {
                StartCoroutine(NextIntro(0.8f));
            });
    }

    IEnumerator NextIntro(float duration)
    {
        yield return new WaitForSeconds(duration);

        intro_2.SetActive(true);
    }
}
