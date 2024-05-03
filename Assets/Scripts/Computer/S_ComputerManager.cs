using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class S_ComputerManager : MonoBehaviour
{
    [SerializeField] List<GameObject> activatedObjects;
    [SerializeField] GameObject computerPanel;
    [SerializeField] GameObject lockedPanel;
    [SerializeField] GameObject wrongPasswordPanel;
    [SerializeField] GameObject webPanel;
    [SerializeField] GameObject youtubePanel;
    [SerializeField] GameObject qrCodePanel;

    [SerializeField] string correctPassword;
    [SerializeField] TMP_InputField passwordInputField;

    [SerializeField] S_ClueData youtubeProof;
    private int remainingPasswordAttemps = 3;
    public TMP_Text remainingAttemps;

    public void OpenComputer()
    {
        GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().SetCanMove(false);

        foreach (GameObject obj in activatedObjects) 
        {
            obj.SetActive(false);
        }

        computerPanel.SetActive(true);
    }

    public void CloseComputer()
    {
        GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().SetCanMove(true);

        foreach (GameObject obj in activatedObjects)
        {
            obj.SetActive(true);
        }

        computerPanel.SetActive(false);
    }

    public void CheckPassword()
    {
        if (passwordInputField.text == correctPassword)
        {
            Debug.Log("Mot de passe correct !");
            lockedPanel.SetActive(false);
            webPanel.SetActive(true);
            S_DialogueManager.Instance.StartDialogue("Ok j'ai supprimé le mail ... tiens ? C'est quoi ces onglets ?");
        }
        else
        {
            Debug.Log("Mot de passe incorrect !");
            wrongPasswordPanel.SetActive(true);
            remainingPasswordAttemps -= 1;
            CheckComputerLock();
        }
    }

    public void CloseWrongPasswordPanel()
    {
        wrongPasswordPanel.SetActive(false);
    }

    public void CheckComputerLock()
    {
        remainingAttemps.text = "Tentatives restantes: " + remainingPasswordAttemps;

        if (remainingPasswordAttemps <= 0)
        {
            Debug.Log("computer locked");
        }
    }

    //open yotube
    public void OpenLink(string url)
    {
        Application.OpenURL(url);
        JournalManager journalManager = GetComponent<JournalManager>();

        if (!journalManager.CheckProofInJournal(youtubeProof))
        {
            S_DialogueManager.Instance.StartDialogue(youtubeProof.clueFinding);
            journalManager.AddProof(youtubeProof);
        }
    }

    public void OpenYoutubePanel()
    {
        youtubePanel.SetActive(true);
        qrCodePanel.SetActive(false);
    }

    public void OpenQrCode()
    {
        qrCodePanel.SetActive(true);
        youtubePanel.SetActive(false);
    }


    //boite mail
}