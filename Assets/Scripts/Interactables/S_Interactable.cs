using UnityEngine;
using UnityEngine.InputSystem;

public class S_Interactable : MonoBehaviour
{
    [SerializeField] private S_InteractableData interactableData;
    public bool hasItem;
    public bool hasClue;

    private void Start()
    {
        hasItem = BoolInitialization(hasItem, nameof(hasItem));
        hasClue = BoolInitialization(hasClue, nameof(hasClue));
    }

    private void OnDisable()
    {
        if (Application.isEditor) //Only when playing on the editor we delete the playerpref data for the booleans
        {
            DeleteSaveData();
        }
    }

    //Check if the boolean is already saved in the Playerpref, so we can keep data between scenes
    private bool BoolInitialization(bool value, string valueName)
    {
        string key = gameObject.name + valueName;

        if (PlayerPrefs.HasKey(key))
        {
            Debug.Log("haskey");
            value = PlayerPrefs.GetInt(key) == 1;
        }
        
        return value;
    }

    //Save the boolean into the PlayerPrefs
    private void BoolUpdate(bool value, string valueName /* put the name of the variable here */)
    {
        string key = gameObject.name + valueName;

        PlayerPrefs.SetInt(key, value ? 1 : 0);
        PlayerPrefs.Save();
    }

    public virtual void Interact(JournalManager journalManager)
    {
        foreach (string description in interactableData.interactableDescription)
        {
            Debug.Log(description);
        }

        if (hasItem)
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

        if (hasClue)
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

        hasItem = false;
        BoolUpdate(hasItem, nameof(hasItem));
    }

    public void FoundClue(JournalManager journalManager, S_ClueData clue)
    {
        foreach (string description in clue.clueFinding)
        {
            Debug.Log(description);
        }

        journalManager.AddClue(clue);

        hasClue = false;
        BoolUpdate(hasClue, nameof(hasClue));
    }

    public void DeleteSaveData() //We should call this everytime we want to delete the data : when returning on the main menu and exiting the game.
    {
        PlayerPrefs.DeleteKey(gameObject.name + nameof(hasItem));
        PlayerPrefs.DeleteKey(gameObject.name + nameof(hasClue));
    }

    private void OnApplicationQuit()
    {
        DeleteSaveData();
    }
}
