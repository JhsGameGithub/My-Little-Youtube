using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSetSlider : MonoBehaviour
{
    public E_SOUND type;
    private Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        LoadData();

        slider.onValueChanged.AddListener(delegate
        {
            OnChangeValue();
        });

    }

    public void OnChangeValue()
    {
        AudioManager.Sound.SetVolume(type, slider.value);

        if (type == E_SOUND.SE)
            AudioManager.Sound.Play("SE/IconBtn");

        SaveData();
    }

    private void LoadData()
    {
        slider.value = type == E_SOUND.BGM ? PlayerPrefs.GetFloat("VOLUME_BGM", 0.8f) : PlayerPrefs.GetFloat("VOLUME_SE", 0.8f);
        AudioManager.Sound.SetVolume(type, slider.value);
    }

    private void SaveData()
    {
        switch (type)
        {
            case E_SOUND.BGM:
                PlayerPrefs.SetFloat("VOLUME_BGM", slider.value);
                break;
            case E_SOUND.SE:
                PlayerPrefs.SetFloat("VOLUME_SE", slider.value);
                break;
        }
    }
}
