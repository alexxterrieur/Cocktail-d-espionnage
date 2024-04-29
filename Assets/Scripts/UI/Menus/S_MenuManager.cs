using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class S_MenuManager : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private TextMeshProUGUI volumeSliderValue;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject HUD;

    private bool onPause = false;

    private void Start()
    {
        if (volumeSlider != null)
        {
            if (S_SoundManager.Instance != null)
            {
                volumeSlider.value = S_SoundManager.Instance.masterVolume;
                volumeSliderValue.SetText("Volume : " + Mathf.Round(S_SoundManager.Instance.masterVolume * 100));
            }
        }
    }

    private void OnEnable()
    {
        if (volumeSlider != null)
        {
            if (S_SoundManager.Instance != null)
            {
                volumeSlider.value = S_SoundManager.Instance.masterVolume;
                volumeSliderValue.SetText("Volume : " + Mathf.Round(S_SoundManager.Instance.masterVolume * 100));
            }
            else
            {
                Debug.LogError("No instance of SoundManager has been found !");
            }
        }
        else
        {
            Debug.LogError("No slider has been referenced !");
        }
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
        if (volumeSliderValue != null)
        {
            if (S_SoundManager.Instance != null)
            {
                S_SoundManager.Instance.masterVolume = volume;
                volumeSliderValue.SetText("Volume : " + Mathf.Round(S_SoundManager.Instance.masterVolume * 100));
            }
            else
            {
                Debug.LogError("No instance of SoundManager has been found !");
            }
        }
        else
        {
            Debug.LogError("No slider has been referenced !");
        }
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void SetGamePaused(bool paused)
    {
        Time.timeScale = paused ? 0f : 1f;
    }

    public void OpenClosePause(bool isOpen)
    {
        if (pauseMenu != null|| HUD != null)
        {
            pauseMenu.SetActive(isOpen);
            HUD.SetActive(!isOpen);
            onPause = isOpen;

            if (isOpen)
            {
                GameObject.Find("Player").GetComponent<PlayerMovement>().SetCanMove(false);
                pauseMenu.transform.GetChild(0).gameObject.SetActive(true);
            }
            else
            {
                GameObject.Find("Player").GetComponent<PlayerMovement>().SetCanMove(true);
                foreach (Transform child in pauseMenu.transform)
                {
                    child.gameObject.SetActive(false);
                }
            }
        }
        else
        {
            Debug.LogError("No references for pauseMenu or HUD !");
        }
    }

    public bool GetOnPause()
    {
        return onPause;
    }
}
