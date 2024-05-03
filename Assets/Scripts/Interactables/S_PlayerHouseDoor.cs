public class S_PlayerHouseDoor : S_ExitDoor
{
    public S_ClueData clue;
    public override void Interact(JournalManager journalManager)
    {
        if (interactableData.interactableDescription[0] != string.Empty || interactableData.interactableDescription != null)
        {
            S_DialogueManager.Instance.StartDialogue(interactableData.interactableDescription);
        }

        if (!journalManager.CheckClueInJournal(clue))
        {
            S_GameManager.Instance.ExitLevel();
        }
        else //can't exit
        {
            S_DialogueManager.Instance.StartDialogue(exitDialogue);
        }
    }
}
