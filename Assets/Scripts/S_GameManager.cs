using UnityEngine;
using UnityEngine.SceneManagement;

public class S_GameManager : MonoBehaviour
{
    public static S_GameManager Instance { get; private set; }
    public int maxProof = 0;

    [SerializeField] private string sceneToLoad;

    private bool canChangeScene = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Update()
    {
        if (canChangeScene && !S_DialogueManager.Instance.GetIsDialogueActive())
        {
            SceneManager.LoadScene(sceneToLoad);
            canChangeScene = false;
        }
    }

    public bool CheckIfEnoughProof(JournalManager journalManager)
    {
        if (journalManager.GetNbOfProof() >= maxProof)
        {
            S_DialogueManager.Instance.StartDialogue("J'ai tout ce qu'il me faut, je peux sortir d'ici.");
            return true;
        }

        return false;
    }

    public void ExitLevel()
    {
        canChangeScene = true;
    }
}
