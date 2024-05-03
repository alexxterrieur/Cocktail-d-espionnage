using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class S_InteractPatronne : S_Interactable
{
    public override void Interact(JournalManager journalManager)
    {
        Debug.Log("Prout");
        if (journalManager.SearchKey(interactableData.key))
        {
            Debug.Log("load Combat Scene");

            S_TCP_Client._TCP_Instance.LoadShaker();
            SceneManager.LoadScene("FinalFight");
        }
        else
        {
            Debug.Log("load defeat Scene");
        }
    }
}
