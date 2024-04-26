using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.InputSystem;

public class S_InteractPatronne : S_Interactable
{
    public override void Interact(JournalManager journalManager)
    {
        Debug.Log("Prout");
        if (journalManager.SearchKey(interactableData.key))
        {
            Debug.Log("load Combat Scene");
        }
        else
        {
            Debug.Log("load defeat Scene");
        }
    }
}
