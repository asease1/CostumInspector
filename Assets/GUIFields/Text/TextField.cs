using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

public class TextField : StringGUI
{
    public TextField(EditorWindow renderWindow, List<FieldInfo> Fields) : base(renderWindow, Fields)
    {
        
    }

    public override void EmitCode(StreamWriter file)
    {
        if (index > 0)
        {
            file.WriteLine("rectPosition = new Rect(" + rect.x + "," + rect.y + "+height, " + rect.width + "," + rect.height + "); ");
            file.WriteLine("{0}Prop.stringValue = EditorGUI.TextField(rectPosition, {0}Prop.stringValue);", fieldsname[index]);
        }
    }

    public override void RenderCall()
    {
        GUIStyle style = new GUIStyle("TextField");
        style.active = style.normal;
        EditorGUI.BeginDisabledGroup(false);
        EditorGUI.TextField(rect, " ", style);
        EditorGUI.EndDisabledGroup();
    }

    
}
