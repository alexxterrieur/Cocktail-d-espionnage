using UnityEngine;

public class S_Interactable : MonoBehaviour
{
    [SerializeField] private S_InteractableData interactableData;
    public bool hasItem;
    public bool hasClue;

    private void Start()
    {
        //if (PlayerPrefs.HasKey(gameObject.name))
        //{
        //    PlayerPrefs.DeleteKey(gameObject.name);
        //}
        //else
        //{
        //    PlayerPrefs.
        //}
    }

    public void Interact(JournalManager journalManager)
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

        journalManager.AddItem(item);

        hasItem = false;
    }

    public void FoundClue(JournalManager journalManager, S_ClueData clue)
    {
        foreach (string description in clue.clueFinding)
        {
            Debug.Log(description);
        }

        journalManager.AddClue(clue);

        hasClue = false;
    }

    private int ToBool(bool value)
    {
        if (value)
            return 1;
        else
            return 0;
    }
}
