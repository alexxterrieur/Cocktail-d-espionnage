using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class S_DialogueManager : MonoBehaviour
{
    public static S_DialogueManager Instance { get; private set; }

    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TextMeshProUGUI dialogueTxt;
    [SerializeField] private GameObject nextTxtDisplay;

    [Header("Text speed")]
    public float characterPerSeconds = 5f;
    public float maxCharPerSec = 100f;
    private float currentCharPerSec = 0f;

    private Queue<string[]> dialogueQueue = new Queue<string[]>();
    private bool isDialogueActive = false;

    private InputAction skipAction;
    public InputActionReference skipReference;

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

        currentCharPerSec = characterPerSeconds;

        skipAction = skipReference.action;
        skipAction.Enable();
    }

    private void OnDestroy()
    {
        skipAction.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        dialogueBox.SetActive(false);
        nextTxtDisplay.SetActive(false);
        dialogueTxt.text = null;
    }

    public void StartDialogue(string[] dialogue)
    {
        dialogueBox.SetActive(true);

        //Since we call this method multiple time we use a buffer to be sure every dialogue is not displayed at the same time
        dialogueQueue.Enqueue(dialogue);

        if (!isDialogueActive)
        {
            StartCoroutine(TypeText(dialogueQueue.Dequeue()));
        }
    }

    public void StartDialogue(string dialogue)
    {
        StartDialogue(new string[] { dialogue });
    }

    public void EndDialogue()
    {
        isDialogueActive = false;
        dialogueTxt.text = null;
        nextTxtDisplay.SetActive(false);
        dialogueBox.SetActive(false);
        GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().SetCanMove(true);
    }

    private IEnumerator TypeText(string[] dialogue)
    {
        isDialogueActive = true;
        string textBuffer = null;

        foreach (string s in dialogue)
        {
            foreach (char c in s)
            {
                textBuffer += c;
                dialogueTxt.text = textBuffer;
                S_SoundManager.Instance.PlaySoundEffect("Dialogue_SFX");

                yield return new WaitForSeconds(1 / currentCharPerSec);
            }

            if(skipAction == null)
            {
                yield return null;
            }

            // We wait for the player to press a key to display the next string
            while (!skipAction.triggered)
            {
                nextTxtDisplay.SetActive(true);
                yield return null;
            }

            nextTxtDisplay.SetActive(false);
            textBuffer = null;
        }
        // The coroutine will start again while there is dialogues in the queue
        if (dialogueQueue.Count > 0) 
        {
            StartCoroutine(TypeText(dialogueQueue.Dequeue()));
        }
        else
        {
            EndDialogue();
        }
    }

    public bool GetIsDialogueActive()
    {
        return isDialogueActive;
    }

    //Use this with the new input system to change text speed
    public void ChangeTextSpeed(bool isSped)
    {
        if (isSped)
        {
            currentCharPerSec = maxCharPerSec;
        }
        else
        {
            currentCharPerSec = characterPerSeconds;
        }
    }
}
