using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    [SerializeField] private GameObject _options;
    [SerializeField] private GameObject _credits;

    public void StartGame()
    {
        SceneManager.LoadScene("LukaTestScene");
    }

    public void OpenOptions()
    {
        _options.SetActive(true);
    }

    public void CloseOptions()
    {
        _options.SetActive(false);
    }

    public void OpenCredits()
    {
        _credits.SetActive(true);
    }

    public void CloseCredits()
    {
        _credits.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
