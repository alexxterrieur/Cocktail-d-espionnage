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
    [SerializeField] S_LockPickingMenu lockpickingMenu;
    [SerializeField] GameObject computerObj;

    [SerializeField] string correctPassword;
    [SerializeField] TMP_InputField passwordInputField;

    [SerializeField] S_ClueData youtubeProof;
    [SerializeField] S_ClueData qrCodeClue;
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
            S_DialogueManager.Instance.StartDialogue("Ok j'ai supprim� le mail ... tiens ? C'est quoi ces onglets ?");
        }
        else
        {
            Debug.Log("Mot de passe incorrect !");
            wrongPasswordPanel.SetActive(true);
            remainingPasswordAttemps -= 1;
            if (remainingPasswordAttemps < 0)
            {
                remainingPasswordAttemps = 0;
            }
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
            S_DialogueManager.Instance.StartDialogue("Je n'ai pas le choix, je dois utiliser mon outil de deverrouillage sur le PC.");
            lockpickingMenu.OpenCloseMenu(true);
            S_DialogueManager.Instance.StartDialogue("Veuillez entrer le code.");
            S_TCP_Client._TCP_Instance.Interactable = computerObj.GetComponent<S_Interactable>();
            S_TCP_Client._TCP_Instance.LoadMegaMind(); //We launch the mastermind game
            Debug.Log("computer locked");
        }
    }

    //open yotube
    public void OpenLink(string url)
    {
        Application.OpenURL(url);
        JournalManager journalManager = GetComponent<JournalManager>();

        if (youtubeProof != null && !journalManager.CheckProofInJournal(youtubeProof))
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
        JournalManager journalManager = GetComponent<JournalManager>();

        if (qrCodeClue != null && !journalManager.CheckClueInJournal(qrCodeClue))
        {
            S_DialogueManager.Instance.StartDialogue(qrCodeClue.clueFinding);
            journalManager.AddClue(qrCodeClue);
        }

        qrCodePanel.SetActive(true);
        youtubePanel.SetActive(false);
    }


    //boite mail
}