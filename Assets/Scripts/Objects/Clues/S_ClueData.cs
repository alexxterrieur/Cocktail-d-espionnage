using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Clues")]
public class S_ClueData : ScriptableObject
{
    [field:TextArea]
    public string[] clueFinding;

    [field: TextArea]
    public string clueDescription;
}
