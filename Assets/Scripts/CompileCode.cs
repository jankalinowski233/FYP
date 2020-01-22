using System;
using System.CodeDom.Compiler;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Binds the code in the runtime
/// </summary>
public class CompileCode : MonoBehaviour
{
    public SyntaxTheme codeTheme;
    public TextContainer container;

    public Text errorText;

    List<MethodInfo> methods;

    [Header("Custom text input")]
    [Space(5f)]
    string inputCharacters = "zxcvbnmasdfghjklqwertyuiop,./<>?;': 1234567890\"-=_+\\ZXCVBNMASDFGHJKLQWERTYUIOP!@#$%^&*()~\t[]{}|";
    int charCount;
    public string codeString;
    public Text codeToDisplay;
    public Image textCaret;
    public float caretBlinkRate;
    float caretBlinkTimer;

    // Start is called before the first frame update
    void Start()
    {
        methods = new List<MethodInfo>();

        codeString = "";
        codeString = container.visibleText;     
        errorText.text = "";

        codeToDisplay.supportRichText = true;
        codeToDisplay.text = codeString;
    }

    private void Update()
    {
        if(codeToDisplay.IsActive() == true)
        {
            HandleCaret();
            TextInput();
            SpecialKeysInput();
        }
        
        codeToDisplay.text = codeString;
        codeToDisplay.text = SyntaxHighlighter.HighlightCode(codeString, codeTheme);
    }

    public void Run()
    {
        Assembly assembly = Compile(container.hiddenText + codeString); //compile code from text
        MethodInfo function = assembly.GetType("TestClass").GetMethod("TestFunction"); //process a class and a function

        container.visibleText = codeString;

        //add methods to the list
        methods.Add(function);

        //make sure the list doesn't grow like crazy if users mash COMPILE button
        if (methods.Count > 1)
        {
            methods.RemoveAt(0);
        }

        //create a delegate and invoke it
        //Action is a special delegate, that can invoke a void function or in general take up to 16 parameters of different types
        //While in a classic delegate users have to provide a parameter they're going to pass, Action overcomes that issue
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
        container.visibleText = container.defaultText;
        codeString = container.visibleText;
    }

    public void TextInput()
    {
        string inputString = Input.inputString;
        foreach(char character in inputString)
        {
            if(inputCharacters.Contains(character.ToString()))
            {
                if (codeString == "")
                    codeString += character;
                else
                    codeString = codeString.Insert(charCount, character.ToString());

                charCount++;
            }        
        }
    }

    public void SpecialKeysInput()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            if (codeString == "" || charCount == codeString.Length)
                codeString += "\n";
            else
                codeString = codeString.Insert(charCount, "\n");

            charCount++;
        }

        if(Input.GetKeyDown(KeyCode.Backspace))
        {
            if (charCount > 0)
            {
                char deleted = codeString[charCount - 1];
                string codeStart = codeString.Substring(0, charCount - 1);
                string codeEnd = codeString.Substring(charCount, codeString.Length - charCount);
                codeString = codeStart + codeEnd;
                charCount--;
            }
        }

        if(Input.GetKeyDown(KeyCode.Tab))
        {
            codeString = codeString.Insert(charCount, "    ");
            charCount += 4;
        }

        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            charCount = Mathf.Max(0, charCount - 1);
        }

        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            charCount = Mathf.Min(codeString.Length, charCount + 1);
        }

        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            
        }

        if(Input.GetKeyDown(KeyCode.DownArrow))
        {

        }
    }

    void HandleCaret()
    {
        caretBlinkTimer += Time.deltaTime;
        if(caretBlinkTimer > (1.0f / caretBlinkRate))
        {
            textCaret.enabled = true;
            caretBlinkTimer = 0f;
        }
        else
        {
            textCaret.enabled = false;
        }
        // TODO caret
    }
}