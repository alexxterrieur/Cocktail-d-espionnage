using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class S_Computer : S_Interactable
{
    [SerializeField] List<GameObject> activatedObjects;
    [SerializeField] GameObject computerPanel;

    protected override void Start()
    {
        base.Start();
    }

    public override void Interact(JournalManager journalManager)
    {
        OpenComputerPanel();
    }

    public void OpenComputerPanel()
    {
        GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().enabled = false;

        foreach (GameObject obj in activatedObjects)
        {
            obj.SetActive(false);
        }

        computerPanel.SetActive(true);
    }
}
