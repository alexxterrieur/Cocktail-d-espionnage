using System.Collections.Generic;

/* This static class is used to save the state of 
 * an Interactable item so it's not reinitialized at every scene change */
public static class S_SaveDataExternal
{
    public struct Interactable
    {//Why not just use the item and clue objects and check if they're null?
        public bool HasItem;
        public bool HasClue;
        public bool HasProof;
        public bool isLocked;
    }

    public struct Journal
    {
        public S_ItemData[] Items;
        public int ItemIndex;

        public S_ClueData[] Clues;
        public int ClueIndex;

        public S_ClueData[] Proofs;
        public int ProofIndex;

        public string ClueDescription;
        public string ProofDescription;
    }

    public static Dictionary<string, Interactable> InteractableMap = new Dictionary<string, Interactable>(); //Store the state of every interactable in the game

    public static Journal JournalData;
    public static bool IsJournalDataInit = false;

    //Use this method everytime a change has been made inside an Interactable or the Journal.
    public static void SaveData(string key, Interactable value)
    {
        if (!InteractableMap.ContainsKey(key))
        {
            InteractableMap.Add(key, value);
        }
        else
        {
            InteractableMap[key] = value;
        }
    }

    //Use this method at the Start for every Interactables
    public static Interactable LoadData(string key, Interactable value)
    {
        if (!InteractableMap.ContainsKey(key))
        {
            SaveData(key, value);
        }
        else
        {
            return InteractableMap[key];
        }

        return value;
    }

    public static void SaveJournalData(Journal journal)
    {
        JournalData = journal;
    }

    public static Journal LoadJournalData(Journal journal)
    {
        if (!IsJournalDataInit)
        {
            SaveJournalData(journal);
            IsJournalDataInit = true;

            return journal;
        }

        return JournalData;
    }

    public static void Reset()
    {
        InteractableMap.Clear();
        JournalData = new Journal();
        IsJournalDataInit = false;
    }

}
