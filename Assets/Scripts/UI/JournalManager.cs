using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class JournalManager : MonoBehaviour
{
    [SerializeField] Button openButton;
    [SerializeField] GameObject cluesPanel;
    [SerializeField] GameObject objectsPanel;
    [SerializeField] GameObject historyPanel;
    [SerializeField] Image[] itemIcones;
    [SerializeField] TextMeshProUGUI descriptionTMP;

    public int clueNb = 10;

    private Sprite lockedIcon;

    //private List<S_ItemData> items;
    private S_ItemData[] items;
    private int itemIndex = 0;

    private S_ClueData[] clues;
    private int clueIndex = 0;

    private string description;

    private void Start()
    {
        lockedIcon = itemIcones[0].GetComponent<Image>().sprite; //keep in memory the locked sprite

        items = new S_ItemData[itemIcones.Length];
        clues = new S_ClueData[clueNb];

        descriptionTMP.SetText("Vous n'avez pas encore trouvé d'indice.");
        description = "";
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.C)) //Test the item removing
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

    public void UpdateClues(string clueDescription)
    {
        description = description + clueDescription + "\r\n";
        descriptionTMP.text = description;
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

    //Add the clue as a text in the description
    public void AddClue(S_ClueData clue)
    {
        if (clueIndex < clueNb)
        {
            clues[clueIndex] = clue;
            UpdateClues(clue.clueDescription);
            clueIndex++;
        }
    }

    //No clue removal needed ?
}
