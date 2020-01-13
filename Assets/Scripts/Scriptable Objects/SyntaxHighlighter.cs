using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyntaxHighlighter
{
    public static string HighlightCode(string code, SyntaxTheme theme)
    {
        string highlightedCode = "";

        string[] codeLines = code.Split('\n');

        for (int i = 0; i < codeLines.Length; i++)
        {
            highlightedCode += Highlight(codeLines[i], theme);

            if (i != codeLines.Length - 1)
            {
                highlightedCode += '\n';
            }
        }

        return highlightedCode;
    }

    static string Highlight(string line, SyntaxTheme theme)
    {
        List<Color> colors = new List<Color>();
        List<Vector2Int> wordStartEnd = new List<Vector2Int>();

        string[] words = line.Split(' ');

        int notSpace = 0;

        for (int i = 0; i < words.Length; i++)
        {
            Color c = Color.clear;

            if (words[i] == "float" || words[i] == "class" || words[i] == "public")
            {
                c = theme.variableColor;
            }
            else if(words[i] == "MonoBehaviour")
            {
                c = theme.mbColor;
            }
            else if(words[i] == "if")
            {
                c = theme.conditionColor;
            }

            if (c != Color.clear)
            {
                colors.Add(c);
                int end = notSpace + words[i].Length;
                wordStartEnd.Add(new Vector2Int(notSpace, end));
            }
            notSpace += words[i].Length;
        }

        if (colors.Count > 0)
        {
            notSpace = 0;
            int colIndex = 0;
            int actualStartIndex = -1;

            for (int i = 0; i <= line.Length; i++)
            {
                if (wordStartEnd[colIndex].x == notSpace)
                {
                    actualStartIndex = i;
                }
                else if (wordStartEnd[colIndex].y == notSpace)
                {
                    wordStartEnd[colIndex] = new Vector2Int(actualStartIndex, i);
                    colIndex++;

                    if (colIndex >= colors.Count)
                    {
                        break;
                    }

                    i--;
                    continue;
                }

                if (i < line.Length)
                {
                    char character = line[i];

                    if (character != ' ')
                        notSpace++;
                }
            }
        }

        for (int i = colors.Count - 1; i >= 0; i--)
        {
            var color = colors[i];
            var startEnd = wordStartEnd[i];
            string colorString = ColorUtility.ToHtmlStringRGB(color);

            line = line.Insert(startEnd.y, "</color>");
            line = line.Insert(startEnd.x, $"<color=#{colorString}>");
        }

        return line;
    }
}

