using UnityEngine;
using UnityEditor;
using System.Reflection;
using System.Collections.Generic;
using System.IO;

public class StringGUI : GUIElements {

    public StringGUI(EditorWindow renderWindow, List<FieldInfo> Fields) : base(renderWindow, Fields)
    {
        fieldsname.Clear();
        fieldsname.Add("  ");
        foreach (FieldInfo elm in fields)
        {
            if (elm.FieldType == typeof(string))
            {
                fieldsname.Add(elm.Name);
            }
        }
    }

    public override Rect RightClickMenu()
    {
        Rect MenuPosition = base.RightClickMenu();
        MenuPosition = selectThisVariable(MenuPosition);
        MyGUIExtension.CustomToolTip("Select the variable that i comblet to this field", MenuPosition, rWindow);
        return MenuPosition;
    }

    public override void UpdateInfo(List<FieldInfo> Fields, List<PropertyInfo> properties, List<MethodInfo> methods)
    {
        base.UpdateInfo(Fields, properties, methods);
        fieldsname.Clear();
        fieldsname.Add("  ");
        foreach (FieldInfo elm in fields)
        {
            if (elm.FieldType == typeof(string))
            {
                fieldsname.Add(elm.Name);
            }
        }
    }

    private Rect selectThisVariable(Rect lastRect)
    {
        lastRect = new Rect(rWindow.position.width - 65, lastRect.y + 20, 65, 18);
        index = EditorGUI.Popup(lastRect, index, fieldsname.ToArray());
        return lastRect;
    }
}
