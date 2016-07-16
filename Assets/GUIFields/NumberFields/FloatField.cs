using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Reflection;

public class FloatField : GUIElements
{
    public float myValue;

    public FloatField(EditorWindow renderWindow, List<FieldInfo> Fields) : base(renderWindow, Fields)
    {

    }

    public override void RenderCall()
    {
        GUIStyle style = new GUIStyle("TextField");
        style.active = style.normal;
        EditorGUI.BeginDisabledGroup(true);
        EditorGUI.FloatField(rect, myValue,style);
        EditorGUI.EndDisabledGroup();
    }

    public override Rect RightClickMenu()
    {
        Rect lastRect = base.RightClickMenu();
        return lastRect;
    }
}
