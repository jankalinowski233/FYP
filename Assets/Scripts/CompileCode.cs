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
    public TextContainer container;

    public InputField codeField;
    public Text errorText;

    List<MethodInfo> methods;
    
    // Start is called before the first frame update
    void Start()
    {
        methods = new List<MethodInfo>();

        codeField.text = container.visibleText;
        errorText.text = "";
    }

    public void Run()
    {
        Assembly assembly = Compile(container.hiddenText + codeField.text); //compile code from text
        MethodInfo function = assembly.GetType("TestClass").GetMethod("TestFunction"); //process a class and a function

        container.visibleText = codeField.text;

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
        foreach(MethodInfo method in methods)
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
        codeField.text = container.visibleText;
    }

    
}