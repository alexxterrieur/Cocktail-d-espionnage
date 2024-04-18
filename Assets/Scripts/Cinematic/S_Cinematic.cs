using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_Cinematic : MonoBehaviour
{
    [SerializeField] private List<Sprite> images = new List<Sprite>();
    [SerializeField] private GameObject imageObject;
    private Sprite currentImage;

    private const float frameDuration = 4f;
    private float frameTimer = 0f;

    private Coroutine transitionCoroutine; //We store the coroutine inside a variable to check if it's running

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

    // Update is called once per frame
    void Update()
    {
        //Display the next image imediately
        if (Input.GetMouseButtonUp(0) && transitionCoroutine != null)
        {
            frameTimer = frameDuration;
        }
        //Test button to check if we can launch the coroutine again
        else if (Input.GetMouseButtonDown(1))
        {
            StartTimerTransition();
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

    //Use this outside the class to start the coroutine
    public void StartTimerTransition()
    {
        if (transitionCoroutine == null)
        {
            currentImage = images[0];
            UpdateImage(currentImage);
            imageObject.SetActive(true);
            transitionCoroutine = StartCoroutine(ImageTimerTransition());
        }
        else
        {
            Debug.LogWarning("You can't start the coroutine while it's running !");
        }
    }

    private IEnumerator ImageTimerTransition()
    {
        int frameIndex = 0;

        while (frameIndex < images.Count) //No idea why the coroutine never end even if frameIndex get higher
        {
            frameTimer += Time.deltaTime;

            if (frameTimer >= frameDuration)
            {
                frameIndex++;
                frameTimer = 0;

                if (frameIndex >= images.Count) //Because the coroutine never ended I checked again here the frameIndex
                {
                    imageObject.SetActive(false);
                    transitionCoroutine = null; //We reset the coroutine because it stopped running at this point
                    yield break;
                }
                else
                {
                    currentImage = images[frameIndex];
                    UpdateImage(currentImage);
                }
            }
            yield return null;
        }
        transitionCoroutine = null;
    }
}
