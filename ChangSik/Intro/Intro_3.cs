using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Linq;

// 인트로 연출 3번
// 독백하며 결심을 하는 주인공
public class Intro_3 : MonoBehaviour
{
    public Text name_text;
    public Text speech_text;
    private float delay = 0.0f;

    private readonly string[] INVALID_CHARS = {
        " ", "　", "\"", "\'", "\\","\n"
    };

    // Start is called before the first frame update
    void Start()
    {
        speech_text.text = "";
        string user_name = DatabaseManager.Player.status.Name + "_";

        StartCoroutine(SonAndMonTalk(user_name, "엄마를 설득하려면", "voice", 0.1f));
        StartCoroutine(SonAndMonTalk(user_name, "300일안에 뭔가 보여드려야해", "voice", 0.1f));
        StartCoroutine(IntroEnd());
    }

    IEnumerator IntroEnd()
    {
        yield return new WaitForSeconds(delay);
        GameSceneManager.Instance.ChangeScene(() =>
        {
            StartCoroutine(GameSceneManager.Instance.LoadScene("MainScene"));
        });
    }

    IEnumerator SonAndMonTalk(string name, string talk, string sound, float speed, int font_size = 60)
    {
        float temp_delay = delay;
        delay += 1.0f + (float)(talk.Length * speed);

        yield return new WaitForSeconds(temp_delay);

        Speech(name, talk, sound, speed, font_size);
    }

    void Speech(string name, string talk, string sound, float speed, int font_size = 60)
    {
        if (name_text != null)
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
}
