using UnityEngine;

public class S_MusicStarter : MonoBehaviour
{
    public string songName;

    private void Start()
    {
        S_SoundManager.Instance.PlayMusic(songName);
    }
}
