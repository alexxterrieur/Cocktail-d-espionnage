using UnityEngine;
using UnityEngine.SceneManagement;

public class S_Interactable : MonoBehaviour
{
    [SerializeField] private S_InteractableData interactableData;

    private string interactableName; //Do not give an already existing name of an Interactible (or the SaveData won't work !)

    public S_InteractableSaveData.Interactable interactableStruct; //The two booleans became a structure to simplify the data saving

    private void Start()
    {
        interactableName = interactableData.interactableName;

        //Initialization of the boolean at every scene change
        interactableStruct = S_InteractableSaveData.LoadData(interactableName, interactableStruct);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.X)) //for testing data saved
        {
            SceneManager.LoadScene("Anais 1");
        }
    }

    public virtual void Interact(JournalManager journalManager)
    {
        foreach (string description in interactableData.interactableDescription)
        {
            Debug.Log(description);
        }

        if (interactableStruct.HasItem)
        {
            if (interactableData.item != null)
                FoundItem(journalManager, interactableData.item);
            else
                Debug.LogWarning("No item has been referenced in this interactable !"); //error warning
        }
        else
        {
            Debug.Log("Vous ne trouvez rien.");
        }

        if (interactableStruct.HasClue)
        {
            if (interactableData.clue != null)
                FoundClue(journalManager, interactableData.clue);
            else
                Debug.LogWarning("No clue has been referenced in this interactable !");
        }
    }

    public S_InteractableData GetInteractableData()
    {
        return interactableData;
    }

    public void FoundItem(JournalManager journalManager, S_ItemData item)
    {
        foreach (string description in item.itemFinding)
        {
            Debug.Log(description);
        }

        Debug.Log("Vous avez ramassé : " + item.itemName);
        journalManager.AddItem(item);

        interactableStruct.HasItem = false;

        //We save the boolean at every change
        S_InteractableSaveData.SaveData(interactableName, interactableStruct);
    }

    public void FoundClue(JournalManager journalManager, S_ClueData clue)
    {
        foreach (string description in clue.clueFinding)
        {
            Debug.Log(description);
        }

        journalManager.AddClue(clue);

        interactableStruct.HasClue = false;

        //We save the boolean at every change
        S_InteractableSaveData.SaveData(interactableName, interactableStruct);
    }
}
