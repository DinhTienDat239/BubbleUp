using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField]
    List<AudioSource> _BGPlayers = new List<AudioSource>();
    [SerializeField]
    List<AudioSource> _effectPlayers = new List<AudioSource> ();

    public float _BGVolume;
    public float _effectVolume;

    private void Start()
    {
        DontDestroyOnLoad(this);
        Init();
    }

    public void Init()
    {
        AdjustBGVolume(PlayerPrefs.GetFloat(CONSTANTS.BG_VOLUME, 1));
        AdjustEffectVolume(PlayerPrefs.GetFloat(CONSTANTS.EFX_VOLUME, 1));
        foreach(AudioSource source in _BGPlayers)
        {
            source.loop = true;
            source.volume = _BGVolume;
        }
        foreach (AudioSource source in _effectPlayers)
        {
            source.loop = false;
            source.volume = _effectVolume;
        }
    }
    public void PlayBG(AudioClip clip,bool turnOffOthers)
    {
        if (turnOffOthers)
        {
            foreach (AudioSource source in _BGPlayers)
            {
                source.Stop();
            }
        }
        foreach(AudioSource source in _BGPlayers)
        {
            if (!source.isPlaying)
            {
                source.clip = clip;
                source.Play();
                return;
            }
        }
    }
    public void PlayEffect(AudioClip clip, bool turnOffOthers)
    {
        if (turnOffOthers)
        {
            foreach (AudioSource source in _effectPlayers)
            {
                source.Stop();
            }
        }
        foreach (AudioSource source in _effectPlayers)
        {
            if (!source.isPlaying)
            {
                source.clip = clip;
                source.Play();
                return;
            }
        }
    }
    public void StopBG()
    {
        foreach (AudioSource source in _BGPlayers)
        {
            source.Stop();
        }
    }
    public void StopEffect()
    {
        foreach (AudioSource source in _effectPlayers)
        {
            source.Stop();
        }
    }
    public void AdjustBGVolume(float volume)
    {
        _BGVolume = volume;
        foreach(AudioSource source in _BGPlayers)
        {
            source.volume = volume;
        }
        PlayerPrefs.SetFloat(CONSTANTS.BG_VOLUME, _BGVolume);
    }
    public void AdjustEffectVolume(float volume)
    {
        _effectVolume = volume;
        foreach (AudioSource source in _effectPlayers)
        {
            source.volume = volume;
        }
        PlayerPrefs.SetFloat(CONSTANTS.EFX_VOLUME, _effectVolume);
    }
}
