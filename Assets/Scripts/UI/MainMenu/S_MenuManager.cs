using UnityEngine;
using UnityEngine.SceneManagement;

public class S_MenuManager : MonoBehaviour
{
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
}
