using UnityEngine;

public class S_Door : S_Interactable
{
    [SerializeField] private GameObject lockpickingMenu;
    public bool isLocked;

    protected override void Start()
    {
        interactableName = interactableData.interactableName;

        interactableStruct.isLocked = isLocked;

        //Initialization of the boolean at every scene change
        interactableStruct = S_SaveDataExternal.LoadData(interactableName, interactableStruct);

        popUpPos = transform.position + (Vector3.up * GetComponent<SpriteRenderer>().bounds.size.y);
    }

    public override void Interact(JournalManager journalManager)
    {
        
    }

    public override void Unlock(JournalManager journalManager, S_ItemData key)
    {
        
    }

    public void CloseLockpickingMenu()
    {
        lockpickingMenu.SetActive(false);
    }
}
