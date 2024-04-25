// Doors can be destroyed to show they are open
public class S_Door : S_Interactable
{
    private bool waitForDialogue = false;

    private void Update()
    {
        if (waitForDialogue) //brute force wait before door open
        {
            if (!S_DialogueManager.Instance.GetIsDialogueActive())
            {
                OpenDoor();
            }
        }
    }

    public override void Unlock(JournalManager journalManager, S_ItemData key)
    {
        S_DialogueManager.Instance.StartDialogue(interactableData.lockedInteractableDescription);
        if (journalManager.SearchKey(key))
        {
            if (key.itemName == "Unlocking Tool")
            {
                S_DialogueManager.Instance.StartDialogue("Veuillez entrer le code.");
                //S_TCP_Client._TCP_Instance.LoadMegaMind(/* Interactable this*/);
            }
            else
            {
                S_DialogueManager.Instance.StartDialogue("Vous déverrouillez la porte avec : " + key.itemName);
                S_DialogueManager.Instance.StartDialogue(interactableData.unlockedInteractableDescription);
                interactableStruct.isLocked = false;
                S_SaveDataExternal.SaveData(interactableName, interactableStruct);

                waitForDialogue = true;
            }
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
