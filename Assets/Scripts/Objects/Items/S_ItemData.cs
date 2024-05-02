using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Items")]
public class S_ItemData : ScriptableObject
{
    public string itemName;
    public Sprite itemSprite;
    public AudioClip itemSound;

    [field: TextArea]
    public string[] itemFinding;

    [field: TextArea]
    public string itemDescription;
}
