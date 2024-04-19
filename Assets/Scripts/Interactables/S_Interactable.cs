using UnityEngine;

public class S_Interactable : MonoBehaviour
{
    [SerializeField] private S_InteractableData interactableData;
    public bool hasItem;

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
            foreach (string description in interactableData.itemFoundDescription)
            {
                Debug.Log(description);
            }

            Debug.Log("Vous récupérez : " + interactableData.item.itemName);

            journalManager.AddItem(interactableData.item);

            hasItem = false;
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

    private int ToBool(bool value)
    {
        if (value)
            return 1;
        else
            return 0;
    }
}
