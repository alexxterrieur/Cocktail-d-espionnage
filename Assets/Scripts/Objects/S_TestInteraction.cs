using UnityEngine;

/* THIS SCRIPT WAS MADE TO TEST THE Interact() METHOD OF THE Interactable SCRIPT.
 ONCE THE PLAYER IS CREATED AND READY THIS SCRIPT HAVE TO BE REMOVED */
public class S_TestInteraction : MonoBehaviour
{
    public LayerMask layer; //interactable layer

    [SerializeField] private JournalManager journalManager;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, 1.5f, layer);

            if (hit.collider != null)
            {
                if (hit.collider.TryGetComponent(out S_Interactable interactable))
                {
                    if (!S_DialogueManager.Instance.GetIsDialogueActive())
                    {
                        interactable.Interact(journalManager);
                    }
                }
            }
        }
    }
}
