using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public CanvasGroup fade_img;
    public Text loading_text;
    public Canvas canvas;

    private float fade_duration = 1.0f;

    #region 싱글톤

    private static GameSceneManager instance = null;

    public static GameSceneManager Instance
    {
        get
        {
            Init();

            return instance;
        }
    }

    static void Init()
    {
        if (instance == null)
        {
            GameObject go = GameObject.Find("GameSceneManager");

            if (go == null)
            {
                go = Instantiate(Resources.Load("Prefabs/Manager/GameSceneManager"), new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                go.GetComponent<Canvas>().worldCamera = Camera.main;
                go.name = "GameSceneManager";
            }

            DontDestroyOnLoad(go);
            instance = go.GetComponent<GameSceneManager>();

        }
    }

    private void Start()
    {
        canvas = GetComponent<Canvas>();

        if (instance != null)
            SceneManager.sceneLoaded += OnSceneLoaded;
    }

    #endregion

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void OnTitle()
    {
        ChangeScene(() =>
        {
            StartCoroutine(LoadScene("TitleScene"));
        });
    }

    // 게임 처음부터
    public void OnBegin()
    {
        AudioManager.Sound.Play("SE/button-3", E_SOUND.SE);
        ChangeScene(() =>
        {
            //DatabaseManager.Instance.BeginDataLoad();
            //DatabaseManager.Instance.DataSave();
            StartCoroutine(LoadScene("IntroScene"));
        });
    }

    // 게임 이어서(불러오기)
    public void OnContinue()
    {
        AudioManager.Sound.Play("SE/button-3", E_SOUND.SE);
        ChangeScene(() =>
        {
            DatabaseManager.DataLoad();
            StartCoroutine(LoadScene("MainScene"));
        });
    }

    public void ChangeScene(CallbackEvent _callback)
    {
        AudioManager.Instance.FadeOutSound(1.0f, true);
        DatabaseManager.Instance.isGameRun = false;

        fade_img.DOFade(1, fade_duration)
            .OnStart(() =>
            {
                fade_img.alpha = 0.0f;
                fade_img.blocksRaycasts = true;
            })
            .OnComplete(() =>
            {
                _callback();
            });
    }

    public IEnumerator LoadScene(string name)
    {
        loading_text.gameObject.SetActive(true);

        AsyncOperation async = SceneManager.LoadSceneAsync(name);
        async.allowSceneActivation = false;

        float past_time = 0.0f;
        float percentage = 0.0f;

        while (!(async.isDone))
        {
            yield return null;

            past_time += Time.deltaTime;

            if (percentage >= 90)
            {
                percentage = Mathf.Lerp(percentage, 100, past_time);

                if (percentage == 100)
                {
                    async.allowSceneActivation = true;
                }
            }
            else
            {
                percentage = Mathf.Lerp(percentage, async.progress * 100f, past_time);
                if (percentage >= 90)
                {
                    past_time = 0.0f;
                }
            }

            loading_text.text = percentage.ToString("0") + "%";
        }

        //yield return null;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(CoSceneLoaded());
    }

    private IEnumerator CoSceneLoaded()
    {
        while (DatabaseManager.Instance.isGameRun)
        {
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        fade_img.DOFade(0, fade_duration)
            .OnStart(() =>
            {
                fade_img.alpha = 1.0f;
                loading_text.gameObject.SetActive(false);
            })
            .OnComplete(() =>
            {
                fade_img.blocksRaycasts = false;
            });
    }
}