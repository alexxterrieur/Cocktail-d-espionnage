using UnityEngine;

public class S_Door : S_Interactable
{
    public bool isClosed;

    public override void Interact(JournalManager journalManager)
    {
        if (isClosed)
        {
            Debug.Log("La porte est ferm�e.");
        }
        else
        {
            //open door animation ?
        }
    }

    public void UnlockDoor()
    {
        isClosed = true;
    }
}
