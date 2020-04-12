using UnityEngine;

// A container for an in-game code
// Scriptable object, so it's easily created in an inspector
[CreateAssetMenu]
public class TextContainer : ScriptableObject
{
    [TextArea(1, 20)]
    public string hiddenText;

    [TextArea(1, 50)]
    public string visibleText;

    [TextArea(1, 50)]
    public string defaultText;

    [TextArea(1, 50)]
    public string loadText;
}
