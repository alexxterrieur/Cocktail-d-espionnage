using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Items")]
public class S_ItemData : ScriptableObject
{
    public string itemName;
    public Sprite itemSprite;

    [field: TextArea]
    public string[] itemFinding;

    [field: TextArea]
    public string itemDescription;
}
