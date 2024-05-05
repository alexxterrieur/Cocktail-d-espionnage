using UnityEngine;
using UnityEngine.SceneManagement;

public class S_InteractPatronne : S_Interactable
{
    private bool isInteractible = false;
    private bool journalHaveKey;

    protected override void Update()
    {
        if (isInteractible && !S_DialogueManager.Instance.GetIsDialogueActive())
        {
            if (interactableData.interactableDescription[0] != string.Empty)
            {
                S_DialogueManager.Instance.StartDialogue(interactableData.interactableDescription);
            }

            if (journalHaveKey)
            {
                Debug.Log("load Combat Scene");
                S_TCP_Client._TCP_Instance.LoadShaker();
                SceneManager.LoadScene("FinalFight");
            }
            else
            {
                S_GameOverManager.Instance.GameOverType = S_GameOverManager.GameOver.BossHouse;
                SceneManager.LoadScene("GameOver");

                Debug.Log("load defeat Scene");
            }
        }
    }

    public override void Interact(JournalManager journalManager)
    {
        journalHaveKey = journalManager.SearchKey(interactableData.key);

        isInteractible = true;

        if (!journalHaveKey)
        {
            S_DialogueManager.Instance.StartDialogue(interactableData.lockedInteractableDescription);
        }
        else
        {
            S_DialogueManager.Instance.StartDialogue(interactableData.unlockedInteractableDescription);
        }
    }
}
