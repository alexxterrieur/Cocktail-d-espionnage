using UnityEngine;

public class S_Interactable : MonoBehaviour
{
    [SerializeField] private S_InteractableData interactableData;

    public void Interact()
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
        }
    }
}
