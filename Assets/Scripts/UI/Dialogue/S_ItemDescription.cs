using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class S_ItemDescription : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject itemDescriptionBox;
    [SerializeField] private RectTransform background;
    [SerializeField] private TextMeshProUGUI itemDescriptionTxt;

    private Vector2 boxPos;
    private bool isEmpty = true;
    private S_ItemData itemData;

    private void Start()
    {
        boxPos = transform.position; //new Vector2(transform.position.x, transform.position.y + GetComponent<RectTransform>().rect.height);
        itemDescriptionBox.SetActive(false);
    }

    private void OnEnable()
    {
        AdjustSize();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isEmpty && itemData != null)
        {
            itemDescriptionBox.SetActive(true);
            itemDescriptionTxt.SetText(itemData.itemDescription);

            AdjustSize();

            itemDescriptionBox.transform.position = boxPos;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isEmpty)
        {
            itemDescriptionBox.SetActive(false);
        }
    }

    public void SetIsEmpty(bool isEmpty)
    {
        this.isEmpty = isEmpty;
    }

    public void SetItemData(S_ItemData itemData)
    {
        this.itemData = itemData;
    }

    public void AdjustSize()
    {
        // Get the preferred width and height of the text
        float textWidth = itemDescriptionTxt.preferredWidth;
        float textHeight = itemDescriptionTxt.preferredHeight;

        // Adjust the size of the background panel to match the text size
        Vector2 newSize = new Vector2(textWidth/2, textHeight/2);
        background.sizeDelta = newSize;
    }
}
