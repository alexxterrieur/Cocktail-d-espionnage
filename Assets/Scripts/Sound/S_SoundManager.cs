using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S_SoundManager : MonoBehaviour
{
    public static S_SoundManager Instance { get; private set; }
    [SerializeField] private AudioSource musicSource;

    public float masterVolume = 1f;

    //private List<AudioClip> soundsEffects = new List<AudioClip>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }



    public void PlaySoundEffect(string clipName, float volume = 1)
    {
        AudioClip clip = Resources.Load<AudioClip>("Sounds/" + clipName);

        if (clip != null)
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            source.clip = clip;
            source.volume = volume * masterVolume;
            source.PlayOneShot(clip);
            Destroy(source, clip.length);
        }
        else
        {
            Debug.LogError("Sound clip '" + clipName + "' not found !");
        }
    }

    public void PlayMusic(string musicClipName, float volume = 1, bool loop = true)
    {
        musicSource.clip = Resources.Load<AudioClip>("Sounds/" + musicClipName);
        musicSource.volume = volume * masterVolume;
        musicSource.loop = loop;
        musicSource.Play();
    }

    public void ChangeVolume(float volume)
    {
        masterVolume = volume;

        foreach(AudioSource source in FindObjectsOfType<AudioSource>())
        {
            source.volume = masterVolume;
        }
    }

    public void StopAllSoundsEffects() //don't stop musics
    {
        foreach (AudioSource source in FindObjectsOfType<AudioSource>())
        {
            if (source.clip != null && source.isPlaying && source.gameObject.name != musicSource.name)
            {
                source.Stop();
            }
        }
    }

    public AudioClip GetSceneMusicClip(string sceneName)
    {
        switch(sceneName)
        {
            case "MainMenu":
                return Resources.Load<AudioClip>("Sounds/MainMenuSong");
            case "Office":
                return Resources.Load<AudioClip>("Sounds/OfficeSound");
            case "PlayerHouse":
                return Resources.Load<AudioClip>("Sounds/MainMenuSong");
            case "BossHouse":
                return Resources.Load<AudioClip>("Sounds/BossHouseSong");
            case "SecretBase":
                return Resources.Load<AudioClip>("Sounds/BossHideoutSong");
        }

        return null;
    }
}
