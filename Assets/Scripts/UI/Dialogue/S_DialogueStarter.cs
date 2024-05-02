using UnityEngine;

public class S_DialogueStarter : MonoBehaviour
{
    [TextArea]
    public string[] monologue;

    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("Player").GetComponent<PlayerMovement>().SetCanMove(false);
        S_DialogueManager.Instance.StartDialogue(monologue);
    }
}
