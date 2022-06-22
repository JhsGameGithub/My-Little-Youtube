using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_SOUND
{
    BGM,
    SE,
    SIZE,
}

public class SoundManager
{
    AudioSource[] audio_sources = new AudioSource[(int)E_SOUND.SIZE];
    Dictionary<string, AudioClip> audio_clips = new Dictionary<string, AudioClip>();

    private float bgm_volume;
    private float se_volume;

    public void Init()
    {
        GameObject root = GameObject.Find("Sound");
        bgm_volume = PlayerPrefs.GetFloat("VOLUME_BGM", 0.8f);
        se_volume = PlayerPrefs.GetFloat("VOLUME_SE", 0.8f);

        if (root == null)
        {
            root = new GameObject { name = "Sound" };
            Object.DontDestroyOnLoad(root);

            string[] sound_names = System.Enum.GetNames(typeof(E_SOUND));
            for (int i = 0; i < sound_names.Length - 1; i++)
            {
                GameObject go = new GameObject { name = sound_names[i] };
                audio_sources[i] = go.AddComponent<AudioSource>();
                go.transform.parent = root.transform;
            }

            // BGM은 무한 루프
            audio_sources[(int)E_SOUND.BGM].loop = true;
        }
    }

    public void Play(string _path, E_SOUND _type = E_SOUND.SE, float _pitch = 1.0f, bool isWhenever = false)
    {
        AudioClip audi_clip = GetAddAudioClip(_path, _type);
        Play(audi_clip, _type, _pitch, isWhenever);
    }
    public void OverridePlay(string _path, E_SOUND _type = E_SOUND.SE, float _pitch = 1.0f)
    {
        AudioClip audi_clip = GetAddAudioClip(_path, _type);
        OverridePlay(audi_clip, _type, _pitch);
    }
    public void BGMStop()
    {
        AudioSource audio_source = audio_sources[(int)E_SOUND.BGM];
        audio_source.Stop();
    }

    private void Play(AudioClip _audio_clip, E_SOUND _type = E_SOUND.SE, float _pitch = 1.0f, bool isWhenever = false)
    {
        if (_audio_clip == null)
            return;

        if (_type == E_SOUND.BGM)
        {
            AudioSource audio_source = audio_sources[(int)E_SOUND.BGM];

            if (audio_source.isPlaying)
                audio_source.Stop();

            audio_source.pitch = _pitch;
            audio_source.volume = bgm_volume;
            audio_source.clip = _audio_clip;
            audio_source.Play();

        }
        else
        {
            AudioSource audio_source = audio_sources[(int)E_SOUND.SE];
            audio_source.pitch = _pitch;
            audio_source.volume = se_volume;

            if (!audio_source.isPlaying || isWhenever)
                audio_source.PlayOneShot(_audio_clip);
        }
    }
        private void OverridePlay(AudioClip _audio_clip, E_SOUND _type = E_SOUND.SE, float _pitch = 1.0f)
    {
        if (_audio_clip == null)
            return;

        if (_type == E_SOUND.BGM)
        {
            AudioSource audio_source = audio_sources[(int)E_SOUND.BGM];

            if (audio_source.isPlaying)
                audio_source.Stop();

            audio_source.pitch = _pitch;
            audio_source.volume = bgm_volume;
            audio_source.clip = _audio_clip;
            audio_source.Play();

        }
        else
        {
            AudioSource audio_source = audio_sources[(int)E_SOUND.SE];
            audio_source.pitch = _pitch;
            audio_source.volume = se_volume;


            audio_source.PlayOneShot(_audio_clip);
        }
    }


    private AudioClip GetAddAudioClip(string _path, E_SOUND _type = E_SOUND.SE)
    {
        if (_path.Contains("Sounds/") == false)
            _path = $"Sounds/{_path}"; // 📂Sound 폴더 안에 저장될 수 있도록



        AudioClip audio_clip = null;

        if (_type == E_SOUND.BGM) // BGM 배경음악 클립 붙이기
        {
            audio_clip = Resources.Load<AudioClip>(_path);
        }
        else // Effect 효과음 클립 붙이기
        {
            if (audio_clips.TryGetValue(_path, out audio_clip) == false)
            {
                audio_clip = Resources.Load<AudioClip>(_path);
                audio_clips.Add(_path, audio_clip);
            }
        }

        if (audio_clip == null)
            Debug.Log($"AudioClip Missing ! {_path}");

        return audio_clip;
    }

    public void Clear()
    {
        foreach (AudioSource copy_source in audio_sources)
        {
            copy_source.clip = null;
            copy_source.Stop();
        }

        audio_clips.Clear();
    }

    public void SetVolume(E_SOUND _type, float _volume)
    {
        if (_type == E_SOUND.BGM)
        {
            bgm_volume = _volume;
            AudioSource audio_source = audio_sources[(int)E_SOUND.BGM];

            audio_source.volume = bgm_volume;
        }
        else
        {
            se_volume = _volume;
        }
    }

    public IEnumerator CoFadeInSound(float _speed, CallbackEvent callback = null)
    {
        AudioSource audio_source = audio_sources[(int)E_SOUND.BGM];
        audio_source.volume = 0;

        while (audio_source.volume < bgm_volume)
        {
            yield return null;
            audio_source.volume += _speed * Time.deltaTime;
        }

        if (callback != null)
        {
            callback();
        }
    }

    public IEnumerator CoFadeOutSound(float _speed, bool isStop = false, CallbackEvent callback = null)
    {
        AudioSource audio_source = audio_sources[(int)E_SOUND.BGM];

        while (audio_source.volume > 0.0f)
        {
            yield return null;
            audio_source.volume -= _speed * Time.deltaTime;
        }

        if (isStop)
        {
            audio_source.Stop();
        }

        if (callback != null)
        {
            callback();
        }
    }

    public float GetVolume(E_SOUND _type)
    {
        return _type == E_SOUND.BGM ? bgm_volume : se_volume;
    }
}
