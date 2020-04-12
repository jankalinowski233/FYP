using System;
using System.CodeDom.Compiler;
using System.Reflection;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Binds the code in the runtime
public class CompileCode : MonoBehaviour
{
    public static CompileCode instance; // Instance

    public SyntaxTheme codeTheme; // Theme
    public TextContainer container; // Code text container

    public Text errorText; // In-game console error texts

    List<MethodInfo> methods; // List of runtime methods

    [Header("Custom text input")] // Custom text input properties
    [Space(5f)]
    int charCount; // Code text
    int lineCount;
    public string codeString;
    public Text codeToDisplay;

    public float caretBlinkRate; // Caret
    float caretBlinkTimer;
    public GameObject caret;
    RectTransform caretRect;
    public Vector3 caretOffset;

    public float timeBtwInputs; // Input timer
    float remainingTimeBtwInputs;

    private void Awake()
    {
        // Init
        if (instance == null)
            instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Init
        methods = new List<MethodInfo>();

        codeString = ""; // Init code string
        codeString = container.visibleText;     
        errorText.text = "";

        charCount = codeString.Length; // Calculate number of lines
        string[] lines = codeString.Split('\n');
        lineCount = lines.Length-1;

        caretRect = caret.GetComponent<RectTransform>(); // Init caret
        codeToDisplay.supportRichText = true;
        codeToDisplay.text = codeString;
    }

    private void Update()
    {
        if(codeToDisplay.IsActive() == true) // If input console is active
        {
            if(remainingTimeBtwInputs <= 0) // If players are allowed to input
            {
                TextInput();
                SpecialKeysInput();               
            }
            else
            {
                remainingTimeBtwInputs -= Time.deltaTime;
            }

            HandleCaret(codeString); // Blink caret
        }
        
        codeToDisplay.text = SyntaxHighlighter.HighlightCode(codeString, codeTheme); // Highlight code
    }

    public void Run() // Compiles code
    {
        Assembly assembly = Compile(container.hiddenText + codeString); // Compile code from text
        MethodInfo function = assembly.GetType("TestClass").GetMethod("TestFunction"); // Process a class and a function

        // Add methods to the list
        methods.Add(function);

        // Make sure the list doesn't grow like crazy if users mash COMPILE button
        if (methods.Count > 1)
        {
            methods.RemoveAt(0);
        }

        // Create a delegate and invoke it
        // Action is a special delegate, that can invoke a void function or in general take up to 16 parameters of different types
        // While in a classic delegate users have to provide a parameter they're going to pass, Action overcomes that issue
        foreach (MethodInfo method in methods)
        {
            Action codeAction = (Action)Delegate.CreateDelegate(typeof(Action), method);

            //invoke delegate (execute code)
            codeAction.Invoke();
        }
    }

    public Assembly Compile(string source)
    {
        //clear console
        errorText.text = "";

        //set up compiler options
        CompilerParameters compilerParameters = new CompilerParameters();
        compilerParameters.GenerateExecutable = false;
        compilerParameters.GenerateInMemory = true;

        //add assemblies
        foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
        {           
            if (!assembly.IsDynamic)
            {
                compilerParameters.ReferencedAssemblies.Add(assembly.Location);
            }
        }

        //compiler code and get results
        CSharpCompiler.CodeCompiler virtualCompiler = new CSharpCompiler.CodeCompiler();
        CompilerResults compilationResults = virtualCompiler.CompileAssemblyFromSource(compilerParameters, source);

        //do results have any errors?
        foreach (var err in compilationResults.Errors)
        {
            CompilerError e = (CompilerError)err; //cast a generic object to CompilerError

            //log errors to the console
            Debug.Log(e.ErrorText);

            //display errors to the console on the screen
            errorText.text += " " + e.ErrorText;          
        }

        //return compiled (or not) code
        return compilationResults.CompiledAssembly;
    }

    public void ResetText()
    {
        charCount = container.defaultText.Length; // Reset caret's anchor point
        codeString = container.defaultText; // Reset text
        container.visibleText = container.defaultText;
    }

    public void TextInput() // Input text
    {
        string inputString = Input.inputString; // If pressed key is contained in allowed input characters
        foreach(char character in inputString)
        {
            if(Keywords.inputCharacters.Contains(character.ToString()))
            {
                if (codeString == "") // If code string is not empty...
                    codeString += character; // ...add a character...
                else
                    codeString = codeString.Insert(charCount, character.ToString()); // ...ekse simply insert a character
               
                charCount++; // Increment character count
            }        
        }
    }

    public void SpecialKeysInput() // Check for special keys input
    {
        if(Input.GetKeyDown(KeyCode.Return)) // add new line upon ENTER press
        {
            if (codeString == "" || charCount == codeString.Length)
                codeString += "\n";
            else
                codeString = codeString.Insert(charCount, "\n");

            charCount++;
            lineCount++;
            caretBlinkTimer = 0.0f;
            caret.SetActive(true);
        }

        if(Input.GetKey(KeyCode.Backspace))
        {
            if (charCount > 0) // delete a character when backspace is pressed
            {
                char deleted = codeString[charCount - 1];
                string codeStart = codeString.Substring(0, charCount - 1);
                string codeEnd = codeString.Substring(charCount, codeString.Length - charCount);
                codeString = codeStart + codeEnd;
                charCount--;

                if (deleted == '\n') // decrement line count if \n is deleted
                    lineCount--;
            }
            remainingTimeBtwInputs = timeBtwInputs;
            caretBlinkTimer = 0.0f;
            caret.SetActive(true);
        }

        if(Input.GetKeyDown(KeyCode.Tab))
        {
            codeString = codeString.Insert(charCount, "    "); // add 4 spaces when tab is pressed
            charCount += 4;
            caretBlinkTimer = 0.0f;
            caret.SetActive(true);
        }

        if(Input.GetKey(KeyCode.LeftArrow)) // Move caret left
        {
            if(charCount != 0) // move to previous line
            {
                if (codeString[charCount - 1] == '\n')
                    lineCount--;
            }          

            charCount = Mathf.Max(0, charCount - 1);
            remainingTimeBtwInputs = timeBtwInputs;
            caretBlinkTimer = 0.0f;
            caret.SetActive(true);
        }

        if(Input.GetKey(KeyCode.RightArrow)) // Move caret right
        {
            if(codeString.Length > charCount) // move to next line
            {
                if (codeString[charCount] == '\n')
                    lineCount++;
            }
           
            charCount = Mathf.Min(codeString.Length, charCount + 1);
            remainingTimeBtwInputs = timeBtwInputs;
            caretBlinkTimer = 0.0f;
            caret.SetActive(true);
        }

        if(Input.GetKeyDown(KeyCode.UpArrow)) // Move caret up
        {          
            if(lineCount > 0) // if line count is greater than 0 (not at the very first line
            {
                string[] lines = codeString.Split('\n'); // split the code
                int length = 0;

                for(int i = 0; i < lineCount; i++) // calculate length
                {
                    length += lines[i].Length + 1;
                }         
                
                charCount = length-1; // set char count
                lineCount--; // decrement line count
                caretBlinkTimer = 0.0f;
                caret.SetActive(true);
            }           
        }

        if(Input.GetKeyDown(KeyCode.DownArrow)) // Move caret down
        {
            string[] lines = codeString.Split('\n'); // get all lines in the code
            if (lineCount < lines.Length - 1) // if current line count is smaller than total count - 1
            {
                int length = lines[0].Length; //begin at line[0] length
                for(int i = 1; i <= lineCount + 1; i++) // iterate over each line
                {
                    length += lines[i].Length + 1; //increment length
                }

                charCount = length; // set char count to calculated length
                lineCount++; //increment line count
                caretBlinkTimer = 0.0f;
                caret.SetActive(true);
            }
        }
        
    }

    void HandleCaret(string t) // Blink caret
    {
        string stopChar = ".";
        codeToDisplay.text = stopChar;

        float lineWidth = codeToDisplay.preferredWidth; // get width and height of a single character
        float lineHeight = codeToDisplay.preferredHeight;

        string upToCharIndex = t.Substring(0, charCount); // determine where the current char index is
        codeToDisplay.text = upToCharIndex;

        float height = codeToDisplay.preferredHeight - lineHeight; // first calculate y position of a caret

        string textUpToCaretCurrentLine = ""; // determine where the caret should be
        for (int i = charCount - 1; i >= 0; i--)
        {
            if (codeString[i] == '\n' || i == 0)
            {
                textUpToCaretCurrentLine = t.Substring(i, charCount - i);
                break;
            }
        }
        codeToDisplay.text = textUpToCaretCurrentLine + stopChar;

        float width = codeToDisplay.preferredWidth - lineWidth; // calculate x position of a caret

        caretRect.position = codeToDisplay.rectTransform.position; // place caret in a right place
        caretRect.localPosition += Vector3.right * (width - caretOffset.x);
        caretRect.localPosition += Vector3.down * (height - (codeToDisplay.rectTransform.rect.height / 2) + caretOffset.y);

        // blink
        caretBlinkTimer += Time.deltaTime;
        if (caretBlinkTimer > caretBlinkRate)
        {
            caret.SetActive(!caret.activeSelf);
            caretBlinkTimer = 0f;
        }
    }
}