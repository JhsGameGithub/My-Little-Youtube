using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SettingUI : MonoBehaviour
{
    public CanvasGroup fade_img;
    public Button quit_btn;
    public Button load_btn;

    // Start is called before the first frame update
    void Start()
    {
        quit_btn.onClick.AddListener(OnSaveAndQuit);
        load_btn.onClick.AddListener(GameSceneManager.Instance.OnContinue);
    }

    public void OnSaveAndQuit()
    {
        Save();

        fade_img.DOFade(1.0f, 1.0f)
            .OnStart(() =>
            {
                fade_img.blocksRaycasts = true;
            })
            .OnComplete(() =>
            {
                Quit();
            });
    }

    private void Save()
    {
        AudioManager.Sound.Play("SE/button-3", E_SOUND.SE);
        DatabaseManager.Instance.DataSave();
    }

    private void Quit()
    {
        Application.Quit();
    }
}