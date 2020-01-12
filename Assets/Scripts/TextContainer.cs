using UnityEngine;

[CreateAssetMenu]
public class TextContainer : ScriptableObject
{
    [TextArea(1, 20)]
    public string hiddenText;

    [TextArea(1, 50)]
    public string visibleText;

    [TextArea(1, 50)]
    public string defaultText;
}
