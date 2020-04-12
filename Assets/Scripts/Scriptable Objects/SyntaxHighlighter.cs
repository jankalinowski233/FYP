using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Highlights written code in the similar way as it is highlighted in dedicated compilators
public class SyntaxHighlighter
{
    // Highlights in-game written code with a given theme
    public static string HighlightCode(string code, SyntaxTheme theme)
    {
        string highlightedCode = ""; // Init string

        string[] codeLines = code.Split('\n'); // Split by new line
        
        // Iterate over each line, adding a highlighted line (Unity's rich text) to a string
        for (int i = 0; i < codeLines.Length; i++)
        {
            highlightedCode += Highlight(codeLines[i], theme);

            if (i != codeLines.Length - 1) // if i does not exceed array length, add a new line
            {
                highlightedCode += '\n';
            }
        }

        return highlightedCode; // Return highlighted code
    }

    // Highlights a line
    static string Highlight(string line, SyntaxTheme theme)
    {
        List<Color> colors = new List<Color>(); // Init list of colors
        List<Vector2Int> wordStartEnd = new List<Vector2Int>(); // Init list of integers - indices of where words start & end
        int notSpace = 0; // Where does a word start?

        // Divide a code by breakChars (e.g. stop characters, commas, parentheses, brackets)
        // This allows me to highlight code even if it is a single word
        // E.g. by default, a word "Debug" would be highlighted, but not in a word "Debug.Log()"
        // This is because a program had seen it as a single word
        // What I'm essentially doing here is converting "Debug.Log()" to something like "Debug . Log ( )"
        // By putting spaces in between characters, a program started picking it up correctly
        // The code still compiles
        string subLine = "";
        subLine = line;

        for (int i = 0; i < subLine.Length; i++)
        {
            char currentChar = subLine[i];

            for(int j = 0; j < Keywords.breakChars.Length; j++)
            {
                string current = Keywords.breakChars[j];

                if (currentChar.ToString() == current)
                {
                    if (subLine.Length > 0)
                    {
                        if ((i - 1 != -1) && subLine[i - 1].ToString() != " ")
                        {
                            subLine = subLine.Insert(i, " ");
                        }

                        if (i + 1 < subLine.Length && subLine[i + 1].ToString() != " ")
                        {
                            subLine = subLine.Insert(i + 1, " ");
                        }
                    }

                }
            }
        }

        // Split words by spaces
        string[] words = subLine.Split(' ');

        // Highlight words
        for (int i = 0; i < words.Length; i++)
        {
            Color c = Color.clear;

            // Highlight comment lines
            if (words[i].StartsWith(Keywords.commentMark) == true)
            {
                line = line.TrimEnd(' ');
                int beginning = line.IndexOf('/');
                int end = line.Length;
                wordStartEnd.Add(new Vector2Int(beginning, end));

                c = theme.commentColor;
                colors.Add(c);

                break;
            }

            // Highlight strings & single characters (words between '' and "");
            if (words[i].StartsWith(Keywords.charMark) || words[i].StartsWith(Keywords.quotationMark))
            {
                c = theme.stringColor;
            }
           
            // Highlight C# keywords (e.g. int)
            for (int j = 0; j < Keywords.blueWords.Length; j++)
            {
                string word = Keywords.blueWords[j];
                if (words[i] == word)
                {
                    c = theme.variableColor;
                }
            }

            // Highlight Unity keywords (e.g. "Update")
            for (int j = 0; j < Keywords.unityKeywords.Length; j++)
            {
                string word = Keywords.unityKeywords[j];
                if (words[i] == word)
                {
                    c = theme.unityKeyword;
                }
            }

            // Highlight Unity classes names (e.g. "Debug")
            for (int j = 0; j < Keywords.unityClasses.Length; j++)
            {
                string word = Keywords.unityClasses[j];
                if (words[i] == word)
                {
                    c = theme.unityClass;
                }
            }

            // Highlight custom classes made by me (e.g. "EventTrigger")
            for(int j = 0; j < Keywords.customClasses.Length; j++)
            {
                string word = Keywords.customClasses[j];
                if(words[i] == word)
                {
                    c = theme.unityClass;
                }
            }

            // If a word has been highlighted, add its indices and colors to the list
            if (c != Color.clear)
            {
                colors.Add(c);
                int end = notSpace + words[i].Length;
                wordStartEnd.Add(new Vector2Int(notSpace, end));
            }
            notSpace += words[i].Length; // Increment notSpace by word's length
        }

        if (colors.Count > 0) // If a line is to be highlighted
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
                else if (wordStartEnd[colorIndex].y == notSpace) // If there is an end tag (</color>) in the middle of the word
                {
                    wordStartEnd[colorIndex] = new Vector2Int(actualStartIndex, i);
                    colorIndex++;

                    if (colorIndex >= colors.Count) // if colorIndex exceeded number of colors, break
                    {
                        break;
                    }

                    i--; // decrement i
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

        for (int i = colors.Count - 1; i >= 0; i--) // Insert rich text in a string
        {
            var color = colors[i];
            var startEnd = wordStartEnd[i];
            string colorString = ColorUtility.ToHtmlStringRGB(color); // Convert a color to a string

            line = line.Insert(startEnd.y, "</color>");
            line = line.Insert(startEnd.x, $"<color=#{colorString}>");
        }

        return line;
    }
}