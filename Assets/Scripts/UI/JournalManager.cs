using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JournalManager : MonoBehaviour
{
    [SerializeField] Button openButton;
    [SerializeField] GameObject cluesPanel;
    [SerializeField] GameObject objectsPanel;
    [SerializeField] GameObject historyPanel;
    [SerializeField] Image[] cluesIcones;
    [SerializeField] Sprite[] cluesSprites;

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
    public void UpdateCluesIcones(int cluesIndex)
    {
        //call the function when a new clue is find
        cluesIcones[cluesIndex].sprite = cluesSprites[cluesIndex];
    }

    public void Update()
    {
        //test
        if(Input.GetKeyDown(KeyCode.A)) { UpdateCluesIcones(0); print("A"); }
        if(Input.GetKeyDown(KeyCode.Z)) { UpdateCluesIcones(1); print("Z"); }
        if(Input.GetKeyDown(KeyCode.E)) { UpdateCluesIcones(2); print("E"); }
        if(Input.GetKeyDown(KeyCode.R)) { UpdateCluesIcones(3); print("R"); }
    }
}
