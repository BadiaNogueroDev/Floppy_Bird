using System;
using UnityEngine;
using UnityEngine.Events;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }
    public AudioSource _audioSource;
    public AudioClip [] _clips;
    public bool _mute;
    
    public enum AUDIOS
    {
        FLY,
        HIT,
        DIE,
        COIN,
        SWOOSH,
    };

    private void Awake()
    {
        if (instance != null && instance != this) 
            Destroy(this);
        else 
            instance = this;
        
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        _mute = PlayerPrefs.GetInt("Mute") == 0;
        SwitchMute(_mute);
    }

    public void SwitchMute(bool value)
    {
        _mute = value;
        _audioSource.mute = _mute;

        //0 = true -- 1 = false
        PlayerPrefs.SetInt("Mute", _mute ? 0 : 1);
    }

    public void PlayAudio(AUDIOS audio)
    {
        _audioSource.PlayOneShot(_clips[(int)audio]);
    }
}