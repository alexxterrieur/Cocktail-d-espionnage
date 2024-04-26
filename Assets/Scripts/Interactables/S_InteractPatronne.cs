using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.InputSystem;

public class S_InteractPatronne : S_Interactable
{
    protected override void Start()
    {
        base.Start();
    }

    public override void Interact(JournalManager journalManager)
    {
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
