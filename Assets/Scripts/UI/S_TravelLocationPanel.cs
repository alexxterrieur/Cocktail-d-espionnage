using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S_TravelLocationPanel : MonoBehaviour
{
    [SerializeField] GameObject travelPanel;
    [SerializeField] GameObject lockedPanel;
    [SerializeField] GameObject wrongLocationPanel;

    [SerializeField] string correctLocation;
    [SerializeField] TMP_InputField travelInputField;

    public void OpenTravelMenu()
    {
        GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().SetCanMove(false);
        travelPanel.SetActive(true);
    }

    public void CloseTravelMenu()
    {
        GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().SetCanMove(true);
        travelPanel.SetActive(false);
        GameObject.FindWithTag("Player").GetComponent<S_PlayerAction>().OnPanel = false;
    }

    public void CheckLocationInfo()
    {
        if (travelInputField.text == correctLocation)
        {
            SceneManager.LoadScene("BossHouse");
        }
        else
        {
            lockedPanel.SetActive(false);
            wrongLocationPanel.SetActive(true);
        }
    }

    public void CloseWrongLocationPanel()
    {
        lockedPanel.SetActive(true);
        wrongLocationPanel.SetActive(false);
        travelPanel.SetActive(false);
        GameObject.FindWithTag("Player").GetComponent<S_PlayerAction>().OnPanel = false;
    }
}
