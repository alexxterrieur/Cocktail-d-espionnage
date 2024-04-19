using UnityEngine;
using UnityEngine.UI;

public class JournalManager : MonoBehaviour
{
    [SerializeField] Button openButton;
    [SerializeField] GameObject cluesPanel;
    [SerializeField] GameObject objectsPanel;
    [SerializeField] GameObject historyPanel;
    [SerializeField] Image[] itemIcones;

    private Sprite lockedIcon;

    //private List<S_ItemData> items;
    private S_ItemData[] items;
    private int itemIndex = 0;

    private void Start()
    {
        lockedIcon = itemIcones[0].GetComponent<Image>().sprite; //keep in memory the locked sprite
        items = new S_ItemData[itemIcones.Length];
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.C))
        {
            RemoveItem(items[0]);
        }
    }

    public void OpenJournal()
    {
        openButton.gameObject.SetActive(false);
        cluesPanel.SetActive(true);
    }

    public void CloseJournal()
    {
        cluesPanel.SetActive(false);
        objectsPanel.SetActive(false);
        historyPanel.SetActive(false);
        openButton.gameObject.SetActive(true);
    }

    public void OpenCluesPanel()
    {
        objectsPanel.SetActive(false);
        historyPanel.SetActive(false);
        cluesPanel.SetActive(true);
    }

    public void OpenObjectsPanel()
    {
        cluesPanel.SetActive(false);
        historyPanel.SetActive(false);
        objectsPanel.SetActive(true);
    }

    public void OpenHistoryPanel()
    {
        cluesPanel.SetActive(false);
        objectsPanel.SetActive(false);
        historyPanel.SetActive(true);
    }

    //Update icones sprite in objectPanel
    public void UpdateItemsIcones(int cluesIndex)
    {
        //call the function when a new item is find
        itemIcones[cluesIndex].sprite = items[cluesIndex].itemSprite;
    }

    //Add item to inventory
    public void AddItem(S_ItemData item)
    {
        if (itemIndex < itemIcones.Length)
        {
            items[itemIndex] = item;
            UpdateItemsIcones(itemIndex);
            itemIndex++;
        }
    }

    //Remove item from inventory
    public void RemoveItem(S_ItemData item)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == item)
            {
                items[i] = null;
                itemIcones[i].sprite = lockedIcon;
            }
        }
    }
}
