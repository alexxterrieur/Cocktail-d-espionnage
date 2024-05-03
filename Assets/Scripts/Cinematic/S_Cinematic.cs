using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class S_Cinematic : MonoBehaviour
{
    [SerializeField] private List<S_FrameData> images = new List<S_FrameData>();
    [SerializeField] private GameObject imageObject;
    [SerializeField] private Clock clock;

    public bool playAtStart = false;

    public InputActionReference skipReference;
    private InputAction skipAction;

    private Sprite currentImage;

    private const float frameDuration = 4f;
    private float frameTimer = 0f;

    private Coroutine transitionCoroutine; //We store the coroutine inside a variable to check if it's running

    private void Awake()
    {
        skipAction = skipReference.action;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (images.Count > 0 && playAtStart)
        {
            StartTimerTransition();
        }
        else
        {
            Debug.LogWarning("No images available for cinematic !");
        }
    }

    //If needed we can change the list with this
    public void SetImages(List<S_FrameData> pictures)
    {
        pictures.Clear();
        images = pictures;
    }

    //Change the sprite of the gameObject in the canvas
    public void UpdateImage(Sprite picture)
    {
        if (imageObject.TryGetComponent(out Image image))
        {
            image.sprite = picture;
        }
    }

    //Use this to start the coroutine
    public void StartTimerTransition()
    {
        if (transitionCoroutine == null)
        {
            clock.SetPause(true);
            currentImage = images[0].frameSprite;
            UpdateImage(currentImage);
            imageObject.SetActive(true);

            //disable some player action for the duration of the cinematic
            GameObject player = GameObject.Find("Player");
            player.GetComponent<PlayerMovement>().SetCanMove(false);
            player.GetComponent<PlayerInput>().actions.FindAction("OpenJournal").Disable();
            player.GetComponent<PlayerInput>().actions.FindAction("Pause").Disable();

            if (images[0].frameClip != null)
                S_SoundManager.Instance.PlaySoundEffect(images[0].frameClip.name);

            transitionCoroutine = StartCoroutine(ImageTimerTransition()); //Set the coroutine variable
        }
        else
        {
            Debug.LogWarning("You can't start the coroutine while it's running !");
        }
    }

    private IEnumerator ImageTimerTransition()
    {
        int frameIndex = 0;

        while (frameIndex < images.Count)
        {
            frameTimer += Time.deltaTime;

            if (frameTimer >= frameDuration)
            {
                frameIndex++;
                frameTimer = 0;

                if (frameIndex < images.Count)
                {
                    currentImage = images[frameIndex].frameSprite;

                    if (images[frameIndex].frameClip != null)
                        S_SoundManager.Instance.PlaySoundEffect(images[frameIndex].frameClip.name);

                    UpdateImage(currentImage);
                }
            }
            if (skipAction.triggered)
            {
                S_SoundManager.Instance.StopAllSoundsEffects();
                frameTimer = frameDuration;
            }
            yield return null;
        }
        imageObject.SetActive(false);
        transitionCoroutine = null; //Set the coroutine variable to null because it's not running anymore
        clock.SetPause(false);

        //enable the actions again after the cinematic
        GameObject player = GameObject.Find("Player");
        player.GetComponent<PlayerMovement>().SetCanMove(true);
        player.GetComponent<PlayerInput>().actions.FindAction("OpenJournal").Enable();
        player.GetComponent<PlayerInput>().actions.FindAction("Pause").Enable();
    }
}
