using UnityEngine;

public class S_LockPickingMenu : MonoBehaviour
{
    private PlayerMovement playerMovement;

    private void Start()
    {
        playerMovement = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (gameObject.activeSelf)
        {
            playerMovement.SetCanMove(false);
        }
    }

    private void OnDisable()
    {
        if (!S_DialogueManager.Instance.GetIsDialogueActive())
        {
            playerMovement.SetCanMove(true);
        }
    }

    public void OpenCloseMenu(bool isOpen)
    {
        gameObject.SetActive(isOpen);
    }
}
