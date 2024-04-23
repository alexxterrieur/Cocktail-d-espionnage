using UnityEngine;
using UnityEngine.SceneManagement;

public class S_Interactable : MonoBehaviour
{
    [SerializeField] private S_InteractableData interactableData;

    private string interactableName; //Do not give an already existing name of an Interactible (or the SaveData won't work !)

    public S_SaveDataExternal.Interactable interactableStruct; //The two booleans became a structure to simplify the data saving

    private void Start()
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

        //Initialization of the boolean at every scene change
        interactableStruct = S_SaveDataExternal.LoadData(interactableName, interactableStruct);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.X)) //for testing data saved
        {
            SceneManager.LoadScene("Anais");
        }
    }

    public virtual void Interact(JournalManager journalManager)
    {
        S_DialogueManager.Instance.StartDialogue(interactableData.interactableDescription);

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
}
