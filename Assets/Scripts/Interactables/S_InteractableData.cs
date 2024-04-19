using System;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Interactables")]
public class S_InteractableData : ScriptableObject
{
    public string interactableName;

    [field: TextArea]
    public string[] interactableDescription;

    public S_ItemData item;
    public S_ClueData clue;
}
