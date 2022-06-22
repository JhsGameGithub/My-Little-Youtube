using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Linq;

// 인트로 연출 2번
// 아들과 엄마의 대화
public class Intro_2 : MonoBehaviour
{
    public Text name_text;
    public Text speech_text;
    public GameObject intro_3;

    private float delay = 0.2f;

    private readonly string[] INVALID_CHARS = {
        " ", "　", "\"", "\'", "\\","\n"
    };

    // Start is called before the first frame update
    void Start()
    {
        name_text.text = "";
        speech_text.text = "";

        string user_name = DatabaseManager.Player.status.Name + "_";

        AudioManager.Sound.Play("BGM/PWAA OST 04  Examination  Moderate 2001", E_SOUND.BGM);
        StartCoroutine(SonAndMonTalk("엄마_", "너 임마 언제까지\n 방구석에서 게임\n만 할꺼야!!", "mother voice", 0.1f));
        StartCoroutine(SonAndMonTalk(user_name, "엄마는 아무것도 몰라요\n 전 유튜버로 성공\n 할꺼라고요!!", "voice", 0.1f));
        StartCoroutine(SonAndMonTalk("엄마_", "시끄럽고 너 취업못하면\n 군대보내고 기숙학원\n 보낼줄알아라!", "mother voice", 0.1f));
        StartCoroutine(SonAndMonTalk("엄마_", "300일뒤에!", "mother voice", 0.3f, 100));
        StartCoroutine(NextIntro(delay));
    }

    IEnumerator SonAndMonTalk(string name, string talk, string sound, float speed, int font_size = 60)
    {
        float temp_delay = delay;
        delay += 1.0f + (float)(talk.Length * speed);

        yield return new WaitForSeconds(temp_delay);

        Speech(name, talk, sound, speed, font_size);
    }

    IEnumerator NextIntro(float duration)
    {
        yield return new WaitForSeconds(duration);

        intro_3.SetActive(true);
    }

    void Speech(string name, string talk, string sound, float speed, int font_size = 60)
    {

        name_text.text = name;
        speech_text.fontSize = font_size;

        float duration = (float)(talk.Length * speed);
        string before_text = speech_text.text = "";

        speech_text.DOText(talk, duration)
            .SetEase(Ease.Linear)
            .OnUpdate(() =>
            {
                string current_text = speech_text.text;

                if (before_text == current_text)
                {
                    return;
                }

                string new_text = current_text[current_text.Length - 1].ToString();

                if (!INVALID_CHARS.Contains(new_text))
                {
                    AudioManager.Sound.Play("SE/" + sound, E_SOUND.SE, 1, true);
                }

                before_text = current_text;
            });
    }

    //public Sequence Speech(string name, string talk, float speed, int font_size = 60)
    //{
    //    return DOTween.Sequence().OnStart(() =>
    //    {
    //        name_text.text = name;
    //        speech_text.text = "";
    //        speech_text.fontSize = font_size;
    //    })
    //    .Join(speech_text.DOText(talk, (float)(talk.Length * speed))).SetEase(Ease.Linear);
    //}


}
