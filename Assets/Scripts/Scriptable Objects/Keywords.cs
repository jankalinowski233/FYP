using System.Collections;
using System.Collections.Generic;

public static class Keywords
{
    public static readonly string commentMark = "//";

    public static readonly string[] blueWords =
    {
        "int",
        "double",
        "short",
        "float",
        "long",
        "bool",
        "string",
        "public",
        "private",
        "protected",
        "static",
        "const",
        "readonly",
        "get",
        "set",
        "class",
        "void",
        "new",
        "in",
        "as",
        "this",
        "if",
        "else",
        "false",
        "continue",
        "break",
        "return",
        "foreach",
        "for",
        "true",
        "null",
        "using",
        "switch",
        "case"
    };

    public static readonly string breakChars = "<>,.;[]{}()";

    public static readonly string[] unityKeywords =
    {
        "Awake",
        "Start",
        "FixedUpdate",
        "Update",
        "LateUpdate",
        "OnGUI",
        "OnEnable",
        "OnDisable",
        "OnTriggerEnter",
        "OnTriggerEnter2D",
        "OnTriggerExit",
        "OnTriggerExit2D",
        "OnTriggerStay",
        "OnTriggerStay2D",
        "Reset",
        "OnMouseDown",
        "OnMouseEnter",
        "OnMouseDrag",
        "OnMouseExit",
        "OnMouseOver",
        "OnMouseUp",
        "OnGUI",
        "OnDrawGizmos"
    };

    public static readonly string[] unityClasses =
    {
        "Object",
        "MonoBehaviour",
        "Transform",
        "Rigidbody",
        "Debug",
        "Rigidbody2D",
        "Collider",
        "Collider2D",
        "Collision",
        "Collision2D",
        "GameObject",
        "Time"
    };
}
