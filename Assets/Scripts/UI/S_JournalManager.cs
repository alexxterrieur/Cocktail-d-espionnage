using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JournalManager : MonoBehaviour
{
    [SerializeField] Button openButton;
    [SerializeField] GameObject cluesPanel;
    [SerializeField] GameObject objectsPanel;
    [SerializeField] GameObject proofPanel;
    [SerializeField] Image[] itemIcones;
    [SerializeField] TextMeshProUGUI clueDescriptionTMP;
    [SerializeField] TextMeshProUGUI proofDescriptionTMP;

    public int clueNb = 10;

    private Sprite lockedIcon;

    private S_SaveDataExternal.Journal journal;

    private void Start()
    {
        lockedIcon = itemIcones[0].GetComponent<Image>().sprite; //keep in memory the locked sprite

        journal.Items = new S_ItemData[itemIcones.Length];
        journal.Clues = new S_ClueData[clueNb];
        journal.Proofs = new S_ClueData[clueNb];
        journal.ItemIndex = 0;
        journal.ClueIndex = 0;
        journal.ClueDescription = string.Empty;
        journal.ProofDescription = string.Empty;

        journal = S_SaveDataExternal.LoadJournalData(journal);

        RefreshJournal(); //Refresh the visuals at every scene start
    }

    public void OpenJournal()
    {
        openButton.gameObject.SetActive(false);
        cluesPanel.SetActive(true);
        GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().SetCanMove(false);
    }

    public void CloseJournal()
    {
        cluesPanel.SetActive(false);
        objectsPanel.SetActive(false);
        proofPanel.SetActive(false);
        openButton.gameObject.SetActive(true);
        GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().SetCanMove(true);
    }

    public void OpenCluesPanel()
    {
        objectsPanel.SetActive(false);
        proofPanel.SetActive(false);
        cluesPanel.SetActive(true);
    }

    public void OpenObjectsPanel()
    {
        cluesPanel.SetActive(false);
        proofPanel.SetActive(false);
        objectsPanel.SetActive(true);
    }

    public void OpenHistoryPanel()
    {
        cluesPanel.SetActive(false);
        objectsPanel.SetActive(false);
        proofPanel.SetActive(true);
    }

    //Update icones sprite in objectPanel
    public void UpdateItemsIcones(int cluesIndex)
    {
         Debug.Log(itemIcones[cluesIndex].name);
         S_ItemDescription itemIcon = itemIcones[cluesIndex].GetComponent<S_ItemDescription>();

        //call the function when a new item is find
        itemIcones[cluesIndex].sprite = journal.Items[cluesIndex].itemSprite;
        itemIcon.SetIsEmpty(false);
        itemIcon.SetItemData(journal.Items[cluesIndex]);
    }

    public void RefreshJournal()
    {
        for (int i = 0; i < itemIcones.Length; i++)
        {
            if (journal.Items[i] != null)
            {
                itemIcones[i].sprite = journal.Items[i].itemSprite;
            }
        }

        clueDescriptionTMP.text = journal.ClueDescription;
        proofDescriptionTMP.text = journal.ProofDescription;
    }

    public void UpdateClues(TextMeshProUGUI tmp, string description)
    {
        tmp.text = description;
    }

    //Add item to inventory
    public void AddItem(S_ItemData item)
    {
        if (journal.ItemIndex < itemIcones.Length)
        {
            journal.Items[journal.ItemIndex] = item;
            UpdateItemsIcones(journal.ItemIndex);
            journal.ItemIndex++;

            S_SaveDataExternal.SaveJournalData(journal);
        }
    }

    //Remove item from inventory, maybe we don't need it
    public void RemoveItem(S_ItemData item) //or use index ?
    {
        for (int i = 0; i < journal.Items.Length; i++)
        {
            if (journal.Items[i] == item)
            {
                journal.Items[i] = null;
                itemIcones[i].sprite = lockedIcon;

                S_SaveDataExternal.SaveJournalData(journal);
                break;
            }
        }
    }

    //Add the clue as a text in the description
    public void AddClue(S_ClueData clue)
    {
        if (journal.ClueIndex < clueNb)
        {
            journal.Clues[journal.ClueIndex] = clue;
            journal.ClueDescription = journal.ClueDescription + clue.clueDescription + "\r\n";

            UpdateClues(clueDescriptionTMP, journal.ClueDescription);
            journal.ClueIndex++;

            S_SaveDataExternal.SaveJournalData(journal);
        }
    }

    //No clue removal needed ?

    public void AddProof(S_ClueData proof)
    {
        if (journal.ClueIndex < clueNb)
        {
            journal.Proofs[journal.ProofIndex] = proof;
            journal.ProofDescription = journal.ProofDescription + proof.clueDescription + "\r\n";

            UpdateClues(proofDescriptionTMP, journal.ProofDescription);
            journal.ProofIndex++;

            S_SaveDataExternal.SaveJournalData(journal);
        }
    }

    public bool SearchKey(S_ItemData key)
    {
        for(int i = 0; i < journal.Items.Length; i++)
        {
            if (journal.Items[i] == key)
            {
                return true;
            }
        }
        return false;
    }
}
