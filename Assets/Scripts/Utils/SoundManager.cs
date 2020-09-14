using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField]
    private Sound[] _Sounds;

    private bool _On = true;

    public bool on
    {
        get
        {
            return _On;
        }
        set
        {
            _On = value;
            if (!_On)
            {
                Array.Find(_Sounds, s => s.name == "BGM").source.Pause();
            }
            else
            {
                Play("BGM");
            }
        }
    }

    protected override void Awake()
    {
        base.Awake();
        foreach (Sound sound in _Sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();

            sound.source.clip = sound.clip;

            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
        }
    }

    private void Start()
    {
        _On = true;
        EventHub.Instance.RegisterEvent(EventName.UPDATE_SOUND_SETTING, ChangeSoundSetting);
        on = !StorageUserInfo.Instance.IsMute;
    }

    public void Play(string name)
    {
        Sound sound = Array.Find(_Sounds, s => s.name == name);
        if (sound == null)
        {
            Debug.LogError($"SoundClip {sound.name} not found");
            return;
        }
        if (_On)
            sound.source.Play();
    }
    public void ChangeSoundSetting(object data)
    {
        bool isMute = (bool)data;
        on = !isMute;
    }
}

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    [Range(0f, 1f)]
    public float volume;
    [Range(0.1f, 3f)]
    public float pitch;
    public bool loop;
    [HideInInspector]
    public AudioSource source;
}
