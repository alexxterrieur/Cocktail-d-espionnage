using UnityEngine;

public class S_Interactable : MonoBehaviour
{
    [SerializeField] private S_InteractableData interactableData;

    public void Interact(JournalManager journalManager)
    {
        foreach (string description in interactableData.interactableDescription)
        {
            Debug.Log(description);
        }

        if (interactableData.hasItem)
        {
            foreach (string description in interactableData.itemFoundDescription)
            {
                Debug.Log(description);
            }

            Debug.Log("Vous récupérez : " + interactableData.item.itemName);

            journalManager.AddItem(interactableData.item);

            interactableData.hasItem = false;
        }
        else
        {
            Debug.Log("Vous ne trouvez rien.");
        }
    }

    public S_InteractableData GetInteractableData()
    {
        return interactableData;
    }
}
