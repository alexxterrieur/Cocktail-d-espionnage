using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerMovement))]
public class S_PlayerAction : MonoBehaviour
{
    public LayerMask layer; //interactable layer
    public float range = 0.5f;
    private S_Interactable lastHitInteractable;
    private CircleCollider2D playerCollider;
    private bool _onPanel = false;

    [SerializeField] private JournalManager journalManager;
    [SerializeField] private S_MenuManager menuManager;

    private PlayerMovement playerMovement;

    public bool OnPanel
    {
        get { return _onPanel; }
        set { _onPanel = value; }
    }

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerCollider = GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        LookAtInteractable();
    }

    public void Interact(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Vector2 direction = playerMovement.GetDirection();

            RaycastHit2D hit = Physics2D.Raycast(playerCollider.bounds.center, direction, range, layer);

            if (hit.collider != null)
            {
                if (hit.collider.TryGetComponent(out S_Interactable interactable))
                {
                    if (!S_DialogueManager.Instance.GetIsDialogueActive() && !_onPanel)
                    {
                        playerMovement.SetCanMove(false);
                        interactable.Interact(journalManager);
                    }
                }
            }
        }
    }

    public void SkipDialogue(InputAction.CallbackContext context)
    {
        if (context.started && S_DialogueManager.Instance.GetIsDialogueActive())
        {
            S_DialogueManager.Instance.ChangeTextSpeed(true);
        }
        else if (context.canceled && S_DialogueManager.Instance.GetIsDialogueActive())
        {
            S_DialogueManager.Instance.ChangeTextSpeed(false);
        }
    }

    public void OpenPauseMenu(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (!menuManager.GetOnPause())
            {
                if (!journalManager.GetJournalObj().activeSelf)
                    menuManager.OpenClosePause(true);
            }
            else
            {
                menuManager.OpenClosePause(false);
            }
        }
    }

    public void OpenJournal(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (!journalManager.GetJournalObj().activeSelf)
            {
                if (!menuManager.GetOnPause())
                    journalManager.OpenJournal();
            }
            else
            {
                journalManager.CloseJournal();
            }
        }
    }

    public void LookAtInteractable()
    {
        Vector2 direction = playerMovement.GetDirection();
        RaycastHit2D hit = Physics2D.Raycast(playerCollider.bounds.center, direction, range, layer);

        if (hit.collider != null)
        {
            if (hit.collider.TryGetComponent(out S_Interactable interactable))
            {
                lastHitInteractable = interactable;
                interactable.DisplayPopup(true);
            }
        }
        else
        {
            if (lastHitInteractable != null)
            {
                lastHitInteractable.DisplayPopup(false);
                lastHitInteractable = null;
            }
        }
    }
}
