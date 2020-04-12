using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Syntax theme
// It's a scriptable object, so it's super simple to create new themes

[CreateAssetMenu]
public class SyntaxTheme : ScriptableObject
{
    public Color variableColor;
    public Color stringColor;
    public Color unityClass;
    public Color unityKeyword;
    public Color commentColor;
}
