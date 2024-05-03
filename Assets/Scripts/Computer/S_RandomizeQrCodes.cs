using System.Collections.Generic;
using UnityEngine;

public class S_RandomizeQrCodes : MonoBehaviour
{
    [SerializeField] private GameObject qrCodePanel;
    [SerializeField] private GameObject repeatedImage;
    [SerializeField] private GameObject singleImage;
    private int image1Count = 15;
    private List<Vector3> takenPositions = new List<Vector3>();
    private float minDistance = 200f;

    void Start()
    {
        RectTransform qrCodePanelRectTransform = qrCodePanel.GetComponent<RectTransform>();
        for (int i = 0; i < image1Count; i++)
        {
            InstantiateImage(repeatedImage, qrCodePanelRectTransform);
        }

        InstantiateImage(singleImage, qrCodePanelRectTransform);
    }

    void InstantiateImage(GameObject imagePrefab, RectTransform parentRectTransform)
    {
        Vector3 randomPosition = GetRandomPosition(parentRectTransform.rect);

         //Keep trying new positions until a non-overlapping one is found
        while (IsOverlapping(randomPosition))
        {
            randomPosition = GetRandomPosition(parentRectTransform.rect);
        }

        // Instantiate the image
        GameObject newImage = Instantiate(imagePrefab, parentRectTransform);
        RectTransform newImageRectTransform = newImage.GetComponent<RectTransform>();
        newImageRectTransform.localPosition = randomPosition;
        takenPositions.Add(randomPosition);
    }

    Vector3 GetRandomPosition(Rect rect)
    {
        float randomX = Random.Range(-rect.width / 2, rect.width / 2);
        float randomY = Random.Range(-rect.height / 2, rect.height / 2);
        return new Vector3(randomX, randomY, 0f);
    }

    bool IsOverlapping(Vector3 position)
    {
        foreach (Vector3 takenPosition in takenPositions)
        {
            if (Vector3.Distance(position, takenPosition) < minDistance)
            {
                return true;
            }
        }
        return false;
    }
}