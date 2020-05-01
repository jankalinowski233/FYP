using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class UnitTests
{
    [Test]
    public void KeywordsTest()
    {
        Assert.AreNotEqual(string.Empty, Keywords.inputCharacters);
        Assert.AreNotEqual(string.Empty, Keywords.commentMark);
        Assert.AreNotEqual(string.Empty, Keywords.quotationMark);
        Assert.AreNotEqual(string.Empty, Keywords.charMark);

        int stringLength = Keywords.blueWords.Length;
        Assert.AreNotEqual(0, stringLength);

        stringLength = Keywords.breakChars.Length;
        Assert.AreNotEqual(0, stringLength);

        stringLength = Keywords.unityClasses.Length;
        Assert.AreNotEqual(0, stringLength);

        stringLength = Keywords.unityKeywords.Length;
        Assert.AreNotEqual(0, stringLength);

        stringLength = Keywords.customClasses.Length;
        Assert.AreNotEqual(0, stringLength);
    }

    [Test]
    public void HighlightTest()
    {
        string initString = "// This is highlighted string";
        int prevLength = initString.Length;

        SyntaxTheme theme = ScriptableObject.CreateInstance<SyntaxTheme>();

        string testString = SyntaxHighlighter.HighlightCode(initString, theme);
        int finalLength = testString.Length;

        Assert.AreNotEqual(string.Empty, testString);
        Assert.AreNotEqual(prevLength, finalLength);
        Assert.AreNotEqual(initString, testString);
    }

    [Test]
    public void CompileTest()
    {
        GameObject go = new GameObject("GO");
        CompileCode cc = go.AddComponent<CompileCode>();
        UnityEngine.UI.Text t = go.AddComponent<UnityEngine.UI.Text>();
        cc.errorText = t;

        string source = @"using UnityEngine;
                        using System.Collections.Generic;

                        public class TestClass
                        {
                             public static void TestFunction()
                             {
                             }
                        }";

        System.Reflection.Assembly testAssembly = cc.Compile(source);

        Assert.AreNotEqual(null, testAssembly);
    }

    [Test]
    public void ResetCodeTest()
    {
        string initCode = "Some code";

        TextContainer tc = ScriptableObject.CreateInstance<TextContainer>();

        tc.defaultText = "Default";
        tc.visibleText = initCode;

        initCode = tc.defaultText;
        tc.visibleText = tc.defaultText;

        Assert.AreEqual(initCode, tc.defaultText);
        Assert.AreEqual(tc.visibleText, tc.defaultText);
    }

    [Test]
    public void LoadCodeTest()
    {
        GameObject go = new GameObject("GO");
        SceneController sc = go.AddComponent<SceneController>();

        string code = "";
        string loadedCode = sc.LoadCode(1);

        Assert.AreNotEqual(code, loadedCode);
    }

    [Test]
    public void SaveCodeTest()
    {
        GameObject go = new GameObject("GO");
        CompileCode cc = go.AddComponent<CompileCode>();
        SceneController sc = go.AddComponent<SceneController>();
        TextContainer tc = ScriptableObject.CreateInstance<TextContainer>();

        cc.container = tc;
        cc.codeString = "This is code";
        sc.c = cc;

        string code = "";
        sc.SaveCode();

        string loadedCode = sc.LoadCode(-1);

        Assert.AreNotEqual(code, loadedCode);
    }

    [Test]
    public void PanelManagerInitTest()
    {
        GameObject panel1 = new GameObject();
        GameObject panel2 = new GameObject();
        GameObject panel3 = new GameObject();

        GameObject panelManager = new GameObject("Manager");
        PanelsManager pm = panelManager.AddComponent<PanelsManager>();
        pm.current = 0;

        pm.panels = new GameObject[3];
        pm.panels[0] = panel1;
        pm.panels[1] = panel2;
        pm.panels[2] = panel3;

        pm.panels[pm.current].SetActive(true);

        Assert.AreEqual(true, pm.panels[pm.current].activeSelf);
    }

    [Test]
    public void PanelManagerShowNextTest()
    {
        GameObject panel1 = new GameObject();
        GameObject panel2 = new GameObject();
        GameObject panel3 = new GameObject();

        GameObject panelManager = new GameObject("Manager");
        PanelsManager pm = panelManager.AddComponent<PanelsManager>();
        pm.current = 0;

        pm.panels = new GameObject[3];
        pm.panels[0] = panel1;
        pm.panels[1] = panel2;
        pm.panels[2] = panel3;

        pm.ShowNext();
        Assert.AreNotEqual(0, pm.current);
    }
    
    [Test]
    public void PanelManagerShowPreviousTest()
    {
        GameObject panel1 = new GameObject();
        GameObject panel2 = new GameObject();
        GameObject panel3 = new GameObject();

        GameObject panelManager = new GameObject("Manager");
        PanelsManager pm = panelManager.AddComponent<PanelsManager>();
        pm.current = 0;

        pm.panels = new GameObject[3];
        pm.panels[0] = panel1;
        pm.panels[1] = panel2;
        pm.panels[2] = panel3;

        pm.ShowPrevious();
        Assert.AreNotEqual(0, pm.current);
    }

    [Test]
    public void PanelManagerWrapNextTest()
    {
        GameObject panel1 = new GameObject();
        GameObject panel2 = new GameObject();
        GameObject panel3 = new GameObject();

        GameObject panelManager = new GameObject("Manager");
        PanelsManager pm = panelManager.AddComponent<PanelsManager>();
        pm.current = 0;

        pm.panels = new GameObject[3];
        pm.panels[0] = panel1;
        pm.panels[1] = panel2;
        pm.panels[2] = panel3;

        pm.ShowNext();
        pm.ShowNext();
        pm.ShowNext();
        Assert.AreEqual(0, pm.current);
    }

    [Test]
    public void PanelManagerWrapPreviousTest()
    {
        GameObject panel1 = new GameObject();
        GameObject panel2 = new GameObject();
        GameObject panel3 = new GameObject();

        GameObject panelManager = new GameObject("Manager");
        PanelsManager pm = panelManager.AddComponent<PanelsManager>();
        pm.current = 0;

        pm.panels = new GameObject[3];
        pm.panels[0] = panel1;
        pm.panels[1] = panel2;
        pm.panels[2] = panel3;

        pm.ShowPrevious();
        pm.ShowPrevious();
        pm.ShowPrevious();
        Assert.AreEqual(0, pm.current);
    }

    [Test]
    public void ShowDropdownTest()
    {
        GameObject go = new GameObject("Temp");
        CustomDropdownController cdc = go.AddComponent<CustomDropdownController>();

        GameObject tempDropdown = new GameObject("Temp dropdown");
        cdc.buttonHolder = tempDropdown;
        cdc.ShowButtons();

        Assert.AreEqual(true, cdc.buttonHolder.activeSelf);
    }

    [Test]
    public void HideDropdownTest()
    {
        GameObject go = new GameObject("Temp");
        CustomDropdownController cdc = go.AddComponent<CustomDropdownController>();

        GameObject tempDropdown = new GameObject("Temp dropdown");
        cdc.buttonHolder = tempDropdown;
        cdc.ShowButtons();
        cdc.ShowButtons();

        Assert.AreNotEqual(true, cdc.buttonHolder.activeSelf);
    }

    [Test]
    public void ChangeTimeScaleTest()
    {
        GameObject go = new GameObject("Temp");
        LevelController lc = go.AddComponent<LevelController>();

        lc.SetTimescale(0.2f);

        Assert.AreNotEqual(1.0f, Time.timeScale);

        lc.SetTimescale(1.0f);
    }
}
