using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Interactables")]
public class S_InteractableData : ScriptableObject
{
    public string interactableName;

    [field: TextArea]
    public string[] interactableDescription;

    [field: TextArea]
    public string[] lockedInteractableDescription;

    [field: TextArea]
    public string[] unlockedInteractableDescription;

    public S_ItemData item;
    public S_ClueData clue;
    public S_ClueData proof;
    public S_ItemData key;
}
