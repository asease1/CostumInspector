using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;
using System.Reflection;

public class GUIElements  {

    public bool deleteThis = false, scaling = false;
    public Rect rect = new Rect(0,0,70,20);
    private static Texture scaleHandel, closeButton;
    protected bool procenctPos, procentSize;
    protected static EditorWindow rWindow;
    protected static List<FieldInfo> fields;
    protected int index;
    protected List<string> fieldsname = new List<string>();

    public float width
    {
        set
        {     
            rect.width = value;
            if (value < 5)
                rect.width = 5;
        }
    }
    public float height
    {

        set
        {
            rect.height = value;
            if (value < 5)
                rect.height = 5;
        }
    
}
    public void setPosition(Vector2 pos)
    {
        if (pos.x < 0)
            rect.x = 0;
        else
            rect.x = pos.x;

        if (pos.y < 20)
            rect.y = 20;
        else
            rect.y = pos.y;
    }
    
    public GUIElements(EditorWindow renderWindow, List<FieldInfo> Fields){
        rWindow = renderWindow;
        fields = Fields;
        if(scaleHandel == null | closeButton == null)
        {
            
            scaleHandel = (Texture)AssetDatabase.LoadAssetAtPath("Assets/Editor/Grafics/ScaleHandel.png", typeof(Texture));
            if (scaleHandel == null)
                Debug.Log("Missing asset Assets/Editor/Grafics/ScaleHandel.png");
            closeButton = (Texture)AssetDatabase.LoadAssetAtPath("Assets/Editor/Grafics/Close.png", typeof(Texture));
            if (closeButton == null)
                Debug.Log("Missing asset Assets/Editor/Grafics/Close.png");
        }
    }

    public virtual void RenderCall()
    {

    }

    public virtual void EmitCode(System.IO.StreamWriter file)
    {

    }

    public void OnEnable(System.IO.StreamWriter file)
    {
        if (index > 0)
        {
            file.WriteLine("{0}Prop = serializedObject.FindProperty (\"{0}\");", fieldsname[index]);
        }
    }

    public virtual Rect RightClickMenu()
    {
        Rect MenuPosition = new Rect(rWindow.position.width - 145, 20, 30, 18);
        MenuPosition = SizeMenu(MenuPosition);
        ScaleHandels();
        CloseButton();
        MenuPosition.y += 20;
        procenctPos = FieldType<Boolean>(MenuPosition, procenctPos);
        MyGUIExtension.CustomToolTip("Make the Field procent position", MenuPosition, rWindow);
        return MenuPosition;
    }

    public virtual void UpdateInfo(List<FieldInfo> Fields, List<PropertyInfo> properties, List<MethodInfo> methods)
    {
        fields = Fields;
    }

    private Rect SizeMenu(Rect lastRect)
    {
        GUIStyle style = new GUIStyle("TextField");
        style.contentOffset = new Vector2(1, 1);
        Rect MenuPosition = lastRect;
        MenuPosition.x += 30;
        EditorGUI.LabelField(MenuPosition, "Size:");
        MenuPosition.x += 30;
        EditorGUI.LabelField(MenuPosition, "X");
        MenuPosition.x += 10;
        width = EditorGUI.FloatField(MenuPosition, rect.width);
        MenuPosition.x += 30;
        EditorGUI.LabelField(MenuPosition, "Y");
        MenuPosition.x += 10;
        height = EditorGUI.FloatField(MenuPosition, rect.height);
        return MenuPosition;
    }

    private void ScaleHandels()
    {
        Rect scaleHandelRect = new Rect(rect.x + rect.width -2, rect.y + rect.height -2, 10, 10);
        EditorGUI.DrawTextureTransparent(scaleHandelRect, scaleHandel, ScaleMode.ScaleToFit);
        if(MyGUIExtension.ClickRect(scaleHandelRect, 0))
        {
            scaling = true;
        }
        else if(Event.current.type == EventType.MouseUp && Event.current.button == 0)
        {
            scaling = false;
        }
        if (scaling)
        {
            if (Event.current.shift)
            {
                Vector2 mousePosition = Event.current.mousePosition;
                float temp = mousePosition.x - rect.x;
                rect.width = temp -(temp%10);
                temp = mousePosition.y - rect.y;
                rect.height = temp - (temp % 10);
            }
            else {
                Vector2 mousePosition = Event.current.mousePosition;
                rect.width = mousePosition.x - rect.x;
                rect.height = mousePosition.y - rect.y;
            }
        }
    }

    private void CloseButton()
    {
        Rect closeButtonRect = new Rect(rect.x + rect.width + 5, rect.y - 15, 10, 10);
        EditorGUI.DrawTextureTransparent(closeButtonRect, closeButton, ScaleMode.ScaleToFit);
        if (MyGUIExtension.ClickRect(closeButtonRect, 0))
        {
            deleteThis = true;
        }
    }

    public string GetVariableName()
    {
        if (index > 0)
        {
            for(int i = 0; i < fields.Count; i++)
            {
                if(fields[i].Name == fieldsname[index])
                {
                    if (fields[i].IsPrivate && !fields[i].IsNotSerialized)
                        Debug.Log("The variable " + fields[i].Name + " need to be serialized");
                    return fieldsname[index];
                }
            }

            Debug.Log("Could not find the Field");
            return "Error";
        }

        else
            return "None";

    }

    protected T FieldType<T>(Rect rect, T value)
    {
        switch (Type.GetTypeCode(typeof(T)))
        {
            case TypeCode.Int32:
                return value;
            case TypeCode.String:
                return value = (T)(object)EditorGUI.TextField(rect, Convert.ToString((object)value)); ;
            case TypeCode.Boolean:
                value = (T)(object)EditorGUI.Toggle(rect, Convert.ToBoolean((object)value));
                return value;
            default:
                Debug.Log("Not Support Type");
                return value;

        }
    }
}
