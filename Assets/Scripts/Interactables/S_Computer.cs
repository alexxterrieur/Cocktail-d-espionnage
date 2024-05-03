using System.Collections.Generic;
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
        GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().SetCanMove(false);

        foreach (GameObject obj in activatedObjects)
        {
            obj.SetActive(false);
        }

        computerPanel.SetActive(true);
    }
}
