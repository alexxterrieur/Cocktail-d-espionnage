using UnityEngine;

public class S_ExitDoor : S_Interactable
{
    [SerializeField] private string exitDialogue;

    public override void Interact(JournalManager journalManager)
    {
        if (interactableData.interactableDescription[0] != string.Empty || interactableData.interactableDescription != null)
        {
            S_DialogueManager.Instance.StartDialogue(interactableData.interactableDescription);
        }

        if (S_GameManager.Instance.CheckIfEnoughProof(journalManager))
        {
            S_GameManager.Instance.ExitLevel();
        }
        else
        {
            S_DialogueManager.Instance.StartDialogue(exitDialogue);
        }
    }
}
