using System;
using System.Collections.Generic;

/* This static class is used to save the state of 
 * an Interactable item so it's not reinitialized at every scene change */
public static class S_InteractableSaveData
{
    [Serializable]
    public struct Interactable
    {
        public bool HasItem;
        public bool HasClue;
    }

    public static Dictionary<string, Interactable> InteractableMap = new Dictionary<string, Interactable>();

    //Use this method everytime a change has been made inside an Interactable
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
}
