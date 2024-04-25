using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class S_Cinematic : MonoBehaviour
{
    [SerializeField] private List<Sprite> images = new List<Sprite>();
    [SerializeField] private GameObject imageObject;
    [SerializeField] private Clock clock;

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
        //Remove this if we don't need to start the cinematic at the start of the gameobject's life
        if (images.Count > 0)
        {
            StartTimerTransition();
        }
        else
        {
            Debug.LogWarning("No images available for cinematic !");
        }
    }

    //If needed we can change the list with this
    public void SetImages(List<Sprite> pictures)
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
            currentImage = images[0];
            UpdateImage(currentImage);
            imageObject.SetActive(true);

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
                    currentImage = images[frameIndex];
                    UpdateImage(currentImage);
                }
            }
            if (skipAction.triggered)
            {
                frameTimer = frameDuration;
            }
            yield return null;
        }
        imageObject.SetActive(false);
        transitionCoroutine = null; //Set the coroutine variable to null because it's not running anymore
        clock.SetPause(false);
    }
}
