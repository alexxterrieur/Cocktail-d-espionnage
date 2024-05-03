using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class S_Computer : S_Interactable
{
    [SerializeField] List<GameObject> activatedObjects;
    [SerializeField] GameObject computerPanel;
    private PlayerMovement playerMovement;
    private S_PlayerAction playerAction;

    protected override void Start()
    {
        base.Start();
        playerMovement = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
        playerAction = GameObject.FindWithTag("Player").GetComponent<S_PlayerAction>();
    }

    public override void Interact(JournalManager journalManager)
    {
        if (S_SoundManager.Instance != null)
        {
            if (interactableData.interactableSound != null)
            {
                S_SoundManager.Instance.PlaySoundEffect(interactableData.interactableSound.name);
                print(interactableData.interactableSound.name);
            }
        }

        if (interactableData.interactableDescription[0] != string.Empty)
        {
            S_DialogueManager.Instance.StartDialogue(interactableData.interactableDescription);
        }

        OpenComputerPanel();
    }

    public void OpenComputerPanel()
    {
        foreach (GameObject obj in activatedObjects)
        {
            obj.SetActive(false);
        }

        playerMovement.SetCanMovePanel(false);
        playerAction.OnPanel = true;

        computerPanel.SetActive(true);
    }
}
