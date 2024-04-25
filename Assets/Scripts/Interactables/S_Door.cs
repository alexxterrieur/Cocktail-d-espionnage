// Doors can be destroyed to show they are open
using UnityEngine;

public class S_Door : S_Interactable
{
    private bool waitForDialogue = false;
    private bool isOpen;

    protected override void Start()
    {
        base.Start();

        isOpen = interactableStruct.isLocked;
    }

    private void Update()
    {
        if (waitForDialogue) //brute force wait before door open
        {
            if (!S_DialogueManager.Instance.GetIsDialogueActive())
            {
                OpenDoor();
            }
        }

        if (S_TCP_Client._TCP_Instance.MegamindWin)
        {
            S_TCP_Client._TCP_Instance.MegamindWin = false;
            UnlockWithDigicode();
        }
    }

    public override void Interact(JournalManager journalManager)
    {
        if (isOpen)
        {
            OpenDoor();
        }
        else
        {
            Unlock(journalManager, interactableData.key);
        }
    }

    public override void Unlock(JournalManager journalManager, S_ItemData key)
    {
        S_DialogueManager.Instance.StartDialogue(interactableData.lockedInteractableDescription);
        if (journalManager.SearchKey(key)) //player have the key
        {
            if (key.itemName == "Unlocking Tool") //If it has to be opened with a digicode
            {
                lockpickingMenu.OpenCloseMenu(true);
                S_DialogueManager.Instance.StartDialogue("Veuillez entrer le code.");
                S_TCP_Client._TCP_Instance.LoadMegaMind(); //We launch the mastermind game
            }
            else //If it's just a regular key
            {
                S_DialogueManager.Instance.StartDialogue("Vous déverrouillez la porte avec : " + key.itemName);
                S_DialogueManager.Instance.StartDialogue(interactableData.unlockedInteractableDescription);
                interactableStruct.isLocked = false;
                S_SaveDataExternal.SaveData(interactableName, interactableStruct);

                waitForDialogue = true;
            }
        }
        else //player doesnt have key
        {
            S_DialogueManager.Instance.StartDialogue("Je n'ai pas l'objet nécessaire pour déverouiller.");
        }
    }

    public override void UnlockWithDigicode()
    {
        base.UnlockWithDigicode();

        waitForDialogue = true;
    }

    public void OpenDoor()
    {
        Destroy(gameObject);
    }
}
