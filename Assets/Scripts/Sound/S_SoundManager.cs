using UnityEngine;

public class S_SoundManager : MonoBehaviour
{
    public static S_SoundManager Instance { get; private set; }
    [SerializeField] private AudioSource musicSource;

    public float MasterVolume = 1f;

    //private List<AudioClip> soundsEffects = new List<AudioClip>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
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
            source.volume = volume * MasterVolume;
            source.PlayOneShot(clip);
            Destroy(source, clip.length);
        }
        else
        {
            Debug.LogError("Sound clip '" + clipName + "' not found !");
        }
    }

    public void PlayMusic(AudioClip musicClip, float volume = 1, bool loop = true)
    {
        musicSource.clip = musicClip;
        musicSource.volume = volume;
        musicSource.loop = loop;
        musicSource.Play();
    }
}
