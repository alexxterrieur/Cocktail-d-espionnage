using Unity.VisualScripting;
using UnityEngine;

public class S_Interactable : MonoBehaviour
{
    [SerializeField] protected S_InteractableData interactableData;
    [SerializeField] private GameObject popUp;
    [SerializeField] protected S_LockPickingMenu lockpickingMenu;

    protected Vector2 popUpPos;

    protected string interactableName; //Do not give an already existing name of an Interactible (or the SaveData won't work !)

    public S_SaveDataExternal.Interactable interactableStruct; //The two booleans became a structure to simplify the data saving

    protected virtual void Start()
    {
        interactableName = interactableData.interactableName;

        if (interactableData.item != null )
        {
            interactableStruct.HasItem = true;
        }
        if ( interactableData.clue != null )
        {
            interactableStruct.HasClue = true;
        }
        if ( interactableData.proof != null )
        {
            interactableStruct.HasProof = true;
        }
        if ( interactableData.key != null)
        {
            interactableStruct.isLocked = true;
        }

        //Initialization of the boolean at every scene change
        interactableStruct = S_SaveDataExternal.LoadData(interactableName, interactableStruct);

        popUpPos = transform.position + (Vector3.up * GetComponent<SpriteRenderer>().bounds.size.y);
    }

    public virtual void Interact(JournalManager journalManager)
    {
        if (interactableData.interactableDescription[0] != string.Empty)
        {
            S_DialogueManager.Instance.StartDialogue(interactableData.interactableDescription);
        }

        if (!interactableStruct.isLocked)
        {
            if (interactableStruct.HasItem)
            {
                if (interactableData.item != null)
                    FoundItem(journalManager, interactableData.item);
                else
                    Debug.LogWarning("No item has been referenced in this interactable !"); //error warning
            }

            if (interactableStruct.HasClue)
            {
                if (interactableData.clue != null)
                    FoundClue(journalManager, interactableData.clue);
                else
                    Debug.LogWarning("No clue has been referenced in this interactable !");
            }

            if (interactableStruct.HasProof)
            {
                if (interactableData.proof != null)
                    FoundProof(journalManager, interactableData.proof);
                else
                    Debug.LogWarning("No proof has been referenced in this interactable !");
            }

            if (interactableData.interactableDescription[0] == string.Empty && !interactableStruct.HasItem &&
                !interactableStruct.HasClue && !interactableStruct.HasProof)
            {
                GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().SetCanMove(true);
            }
        }
        else
        {
            Unlock(journalManager, interactableData.key);
        }
    }


    public S_InteractableData GetInteractableData()
    {
        return interactableData;
    }

    public void FoundItem(JournalManager journalManager, S_ItemData item)
    {
        S_DialogueManager.Instance.StartDialogue(item.itemFinding);
        S_DialogueManager.Instance.StartDialogue("Vous avez ramassé : " + item.itemName);

        journalManager.AddItem(item);

        interactableStruct.HasItem = false;

        //We save the boolean at every change
        S_SaveDataExternal.SaveData(interactableName, interactableStruct);
    }

    public void FoundClue(JournalManager journalManager, S_ClueData clue)
    {
        S_DialogueManager.Instance.StartDialogue(clue.clueFinding);
        S_DialogueManager.Instance.StartDialogue("Un indice à été ajouté au journal.");
        journalManager.AddClue(clue);

        interactableStruct.HasClue = false;

        //We save the boolean at every change
        S_SaveDataExternal.SaveData(interactableName, interactableStruct);
    }

    public void FoundProof(JournalManager journalManager, S_ClueData proof)
    {
        S_DialogueManager.Instance.StartDialogue(proof.clueFinding);
        S_DialogueManager.Instance.StartDialogue("Vous avez trouvé une preuve !");
        journalManager.AddProof(proof);

        interactableStruct.HasProof = false;

        //We save the boolean at every change
        S_SaveDataExternal.SaveData(interactableName, interactableStruct);
    }

    //Unlock the interactable if the key is found
    public virtual void Unlock(JournalManager journalManager, S_ItemData key)
    {
        S_DialogueManager.Instance.StartDialogue(interactableData.lockedInteractableDescription);

        if (journalManager.SearchKey(key)) //player have the key
        {
            if (key.itemName == "Unlocking Tool") //If it has to be opened with a digicode
            {
                lockpickingMenu.OpenCloseMenu(true);
                S_DialogueManager.Instance.StartDialogue("Veuillez entrer le code.");
                S_TCP_Client._TCP_Instance.Interactable = this;
                S_TCP_Client._TCP_Instance.LoadMegaMind(); //We launch the mastermind game
            }
            else //If it's just a regular key
            {
                S_DialogueManager.Instance.StartDialogue("Vous déverrouillez " + interactableName + " avec : " + key.itemName);
                S_DialogueManager.Instance.StartDialogue(interactableData.unlockedInteractableDescription);
                interactableStruct.isLocked = false;
                S_SaveDataExternal.SaveData(interactableName, interactableStruct);
            }
        }
        else //player doesnt have key
        {
            S_DialogueManager.Instance.StartDialogue("Je n'ai pas l'objet nécessaire pour déverouiller.");
        }
    }

    // If the interactable has to be opened with the digicode
    public virtual void UnlockWithDigicode()
    {
        S_DialogueManager.Instance.StartDialogue(interactableData.unlockedInteractableDescription);

        interactableStruct.isLocked = false;
        S_SaveDataExternal.SaveData(interactableName, interactableStruct);

        lockpickingMenu.OpenCloseMenu(false);
    }

    public void DisplayPopup(bool isDisplayed)
    {
        popUp.SetActive(isDisplayed);
        popUp.transform.position = popUpPos;
    }
}
