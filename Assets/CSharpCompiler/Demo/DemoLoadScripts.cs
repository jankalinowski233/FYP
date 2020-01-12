using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Loads the code from file and executes it
/// </summary>
namespace CSharpCompiler
{ 
    public class DemoLoadScripts : MonoBehaviour
    {
        public bool loadInBackground = true;
        public bool doStream = false;

        List<string> loaded = new List<string>();

        DeferredSynchronizeInvoke synchronizedInvoke;
        CSharpCompiler.ScriptBundleLoader loader;

        public InputField codeInputField;

        void Start()
        {           
            synchronizedInvoke = new DeferredSynchronizeInvoke();

            loader = new CSharpCompiler.ScriptBundleLoader(synchronizedInvoke);
            loader.logWriter = new UnityLogTextWriter();

            loader.createInstance = (Type t) =>
            {
                if (typeof(Component).IsAssignableFrom(t)) return this.gameObject.AddComponent(t);
                else return System.Activator.CreateInstance(t);
            };

            loader.destroyInstance = (object instance) =>
            {
                if (instance is Component) Destroy(instance as Component);
            };
        }

        void Update()
        {
            synchronizedInvoke.ProcessQueue();
        }

        public void Compile()
        {
            var sourceFolder = Application.streamingAssetsPath;
            int num = 0;
            var files = Directory.GetFiles(sourceFolder, "*", SearchOption.AllDirectories);

            foreach (var file in files)
            {
                if (!file.EndsWith(".meta"))
                {
                    if (num > 20)
                        break;

                    num++;
                    var shortPath = file.Substring(sourceFolder.Length);

                    if (loaded.Contains(file))
                    {
                        //already loaded
                        print("file already loaded");
                    }
                    else
                    {
                        //load the file and compile
                        loader.LoadAndWatchScriptsBundle(new[] { file });
                        loaded.Add(file);
                    }
                }
            }
        }

        //void OnGUI()
        //{
        //    var sourceFolder = Application.streamingAssetsPath;
        //    int num = 0;
        //    var files = Directory.GetFiles(sourceFolder, "*", SearchOption.AllDirectories);
        //    foreach (var file in files)
        //    {
        //        if (!file.EndsWith(".meta"))
        //        {
        //            if (num > 20) break;
        //            num++;
        //            var shortPath = file.Substring(sourceFolder.Length);
        //            if (loaded.Contains(file))
        //            {
        //                GUILayout.Label("Loaded: " + shortPath);
        //            }
        //            else
        //            {
        //                if (GUILayout.Button("Load: " + shortPath))
        //                {
        //                    loader.LoadAndWatchScriptsBundle(new[] { file });
        //                    loaded.Add(file);
        //                }
        //            }
        //        }
        //    }
        //}

    }
}

