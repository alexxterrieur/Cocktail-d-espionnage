using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Items")]
public class S_ItemData : ScriptableObject
{
    public string itemName;

    [field: TextArea]
    public string itemDescription;
}
