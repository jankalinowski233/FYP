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

    static string Format(string line)
    {
        line = line.Trim();

        string formatted = "";

        return formatted;
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

            if(words[i].StartsWith(Keywords.commentMark) == true)
            {
                line = line.TrimEnd(' ');
                int beginning = line.IndexOf('/');
                int end = line.Length;
                wordStartEnd.Add(new Vector2Int(beginning, end));

                c = theme.commentColor;
                colors.Add(c);

                break;
            }

            for (int j = 0; j < Keywords.blueWords.Length; j++)
            {
                string word = Keywords.blueWords[j];              
                if (words[i].Contains(word))
                {
                    c = theme.variableColor;
                }
            }

            for (int j = 0; j < Keywords.unityKeywords.Length; j++)
            {
                string word = Keywords.unityKeywords[j];
                if (words[i].Contains(word))
                {
                    c = theme.unityKeyword;
                }
            }

            for (int j = 0; j < Keywords.unityClasses.Length; j++)
            {
                string word = Keywords.unityClasses[j];
                if (words[i].Contains(word))
                {
                    c = theme.unityClass;
                }
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
            int colorIndex = 0;
            int actualStartIndex = -1;

            for (int i = 0; i <= line.Length; i++)
            {
                if (wordStartEnd[colorIndex].x == notSpace) // check for actual start and end position
                {
                    actualStartIndex = i;
                }
                else if (wordStartEnd[colorIndex].y == notSpace)
                {
                    wordStartEnd[colorIndex] = new Vector2Int(actualStartIndex, i);
                    colorIndex++;

                    if (colorIndex >= colors.Count)
                    {
                        break;
                    }

                    i--;
                    continue;
                }

                if (i < line.Length) // look for spaces in a line
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

