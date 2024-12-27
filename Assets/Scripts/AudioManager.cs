using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    public const string MUSIC_KEY = "musicVolume";
    public static AudioManager instance;
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        LoadVolume();
    }
    public void LoadVolume() //Volume saved in VolumeSettings.cs
    {
        float musicVolume = PlayerPrefs.GetFloat(MUSIC_KEY, 1f);
        mixer.SetFloat(VolumeSettings.MIXER_MUSIC, Mathf.Log10(musicVolume)*20);
    }
}
