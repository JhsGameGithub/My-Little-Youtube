using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance = null;

    public static AudioManager Instance
    {
        get
        {
            Init();

            return instance;
        }
    }

    private SoundManager sound = new SoundManager();

    public static SoundManager Sound
    {
        get
        {
            return Instance.sound;
        }
    }

    static void Init()
    {
        if (instance == null)
        {
            GameObject go = GameObject.Find("AudioManager");

            if (go == null)
            {
                go = new GameObject { name = "AudioManager" };
                go.AddComponent<AudioManager>();
            }
            DontDestroyOnLoad(go);

            instance = go.GetComponent<AudioManager>();
            instance.sound.Init();
        }
    }

    public void FadeInSound(float _speed, CallbackEvent callback = null)
    {
        StartCoroutine(sound.CoFadeInSound(_speed, callback));
    }

    public void FadeOutSound(float _speed, bool isStop = false, CallbackEvent callback = null)
    {
        StartCoroutine(sound.CoFadeOutSound(_speed, isStop, callback));
    }

    public void Clear()
    {
        sound.Clear();
    }

}
