using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class S_MenuManager : MonoBehaviour
{
    [SerializeField] private  Slider volumeSlider;
    [SerializeField] private TextMeshProUGUI volumeSliderValue;

    private void Start()
    {
        volumeSlider.value = S_SoundManager.Instance.MasterVolume;
        volumeSliderValue.SetText("Volume : " + Mathf.Round(S_SoundManager.Instance.MasterVolume * 100));
    }

    public void StartGame()
    {
        SceneManager.LoadScene("LukaTestScene");
    }

    public void OpenMenu(GameObject menu)
    {
        menu.SetActive(true);
    }

    public void CloseMenu(GameObject menu)
    {
        menu.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OnVolumeChange(float volume)
    {
        S_SoundManager.Instance.MasterVolume = volume;
        volumeSliderValue.SetText("Volume : " + Mathf.Round(S_SoundManager.Instance.MasterVolume * 100));
    }
}
