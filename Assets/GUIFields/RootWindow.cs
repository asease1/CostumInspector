using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class RootWindow
{
    public List<GUIElements> GUIelements = new List<GUIElements>();

    public void AddGUI(GUIElements GUI)
    {
        GUIelements.Add(GUI);
    }

    public void RenderCall()
    {
        foreach(GUIElements elm in GUIelements)
        {
            elm.RenderCall();
        }
    }

    public void Compline(string sourceFile)
    {
        System.IO.StreamWriter file = new System.IO.StreamWriter(Application.dataPath+"\\Editor\\"+sourceFile+"Editor.cs");
        SetUp(file, sourceFile);
        OnEnableCompile(file,sourceFile);
        file.WriteLine("public override void OnInspectorGUI(){");
        file.WriteLine("serializedObject.Update ();");
        file.WriteLine(sourceFile + " myTarget = (" + sourceFile + ")target;");
        file.WriteLine("height = EditorGUILayout.GetControlRect(true, GUILayout.Height("+ GetMaxHeight() + ")).y;");
        foreach (GUIElements elm in GUIelements)
        {
            elm.EmitCode(file);
        }
        file.WriteLine("serializedObject.ApplyModifiedProperties ();");
        file.WriteLine("}");
        file.WriteLine("}");
        file.Close();
        AssetDatabase.Refresh();
    }

    private void OnEnableCompile(System.IO.StreamWriter file, string sourceFile)
    {
        file.WriteLine("void OnEnable () {");
        serializedObjects(file);
        file.WriteLine("}");
    }

    private void SetUp(System.IO.StreamWriter file,string sourceFile)
    {
        file.WriteLine("using UnityEditor;");
        file.WriteLine("using UnityEngine;");
        file.WriteLine("using System.Collections.Generic;");
        file.WriteLine("[CustomEditor(typeof(" + sourceFile + "))]");
        file.WriteLine("[CanEditMultipleObjects]");
        file.WriteLine("public class " + sourceFile + "Editor : Editor{");
        file.WriteLine("private Rect rectPosition;");
        file.WriteLine("private float height;");
        SetupVariables(file);


    }

    public void DeleteAll()
    {
        GUIelements.Clear();
    }

    public void Remove(GUIElements element)
    {
        GUIelements.Remove(element);
    }

    private float GetMaxHeight()
    {
        float maxHeight = 0;
        foreach(GUIElements elm in GUIelements)
        {
            if(maxHeight < (elm.rect.height + elm.rect.y))
            {
                maxHeight = elm.rect.height + elm.rect.y + 5;
            }
        }
        return maxHeight;
    }

    private void SetupVariables(System.IO.StreamWriter file)
    {
        List<string> variablenName = new List<string>();
        foreach(GUIElements elm in GUIelements)
        {
            string temp = elm.GetVariableName();
            if(temp != "None")
            {
                if (!variablenName.Contains(temp))
                    variablenName.Add(temp);
            }

        }
        foreach(string elm in variablenName)
        {
            file.WriteLine("SerializedProperty {0}Prop;", elm);
        }
    }

    private void serializedObjects(System.IO.StreamWriter file)
    {
        List<string> variablenName = new List<string>();
        foreach (GUIElements elm in GUIelements)
        {
            string temp = elm.GetVariableName();
            if (temp != "None")
            {
                if (!variablenName.Contains(temp))
                    variablenName.Add(temp);
            }

        }
        foreach (string elm in variablenName)
        {
            file.WriteLine("{0}Prop = serializedObject.FindProperty (\"{0}\");", elm);
        }
    }
}

