using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Reflection;
using System.Collections.Generic;

public class CreateInspector : EditorWindow
{
    //Enum that defines all the GUI elements that can be made
    private enum GUIElm { FloatField, TextField};
    private Object source = new Object(), oldSource = new Object();
    public List<FieldInfo> ScriptFields = new List<FieldInfo>();
    public List<PropertyInfo> ScriptPropeties = new List<PropertyInfo>();
    public List<MethodInfo> ScriptMethods = new List<MethodInfo>();
    private Vector2 scrollPos;
    private bool foldOutNumberFields, foldOutVectorFields, foldOutText, foldOutListFields, foldOutSliders, foldOutToggle;
    public RootWindow rootWindow = new RootWindow();
    private TestWindow testWindow;
    private bool delete;


    // Add menu named "My Window" to the Window menu
    [MenuItem("Window/Create Inspector")]
    static void Init()
    {
        
        // Get existing open window or if none, make a new one:
        CreateInspector window = (CreateInspector)EditorWindow.GetWindow(typeof(CreateInspector));
        window.Show();  
    }

    void OnGUI()
    {
        GUILayout.Label("Source Code", EditorStyles.boldLabel);
        source = EditorGUILayout.ObjectField(source, typeof(Object), true);
        if (source != null && !source.Equals(oldSource))
        {
            ScriptFields = ReadFile.GetFields(source.name);
            ScriptMethods = ReadFile.GetMethods(source.name);
            if (rootWindow.GUIelements.Count > 0)
                rootWindow.GUIelements[0].UpdateInfo(ScriptFields, ScriptPropeties, ScriptMethods);
        }
        oldSource = source;
        EditorGUILayout.BeginHorizontal();
        if(GUILayout.Button("Show Test Window"))
        {
            if(testWindow == null)
                testWindow = new TestWindow(this);

            testWindow.Show();
        }
        if (!delete)
        {
            if (GUILayout.Button("Delete"))
            {
                delete = true;
            }
        }
        else
        {
            if (GUILayout.Button("Are you sure!"))
            {
                rootWindow.DeleteAll();
                delete = false;
                testWindow.Repaint();
            }
        }
        EditorGUILayout.EndHorizontal();
        if (GUILayout.Button("Compile") && source != null)
        {
            rootWindow.Compline(source.name);
        }
        //the minus 40 is for the size of the last rect and the space to the scroll area
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Width(position.width), GUILayout.Height(position.height- GUILayoutUtility.GetLastRect().y-40));
        NumberField(ref foldOutNumberFields);
        VectorField(ref foldOutVectorFields);
        TextField(ref foldOutText);
        ListFields(ref foldOutListFields);
        Sliders(ref foldOutSliders);
        Toggle(ref foldOutToggle);
        EditorGUILayout.EndScrollView();


    }

    private void NumberField(ref bool foldOut)
    {
        EditorGUI.BeginChangeCheck();
        EditorGUILayout.GetControlRect(true, 16f, EditorStyles.foldout);
        Rect foldRect = GUILayoutUtility.GetLastRect();
        //make a check to see if you are clicken on the foldout
        if (Event.current.type == EventType.MouseUp && foldRect.Contains(Event.current.mousePosition))
        {
            foldOut = !foldOut;
            GUI.changed = true;

        }
        EditorGUI.Foldout(foldRect, foldOut, "Number Field");

        if (foldOut)
        {
            EditorGUI.indentLevel++;
            FieldButton("Float Field", "Make a text field for entering float values.", GUIElm.FloatField);
            //FieldButton("Int Field", "Make a text field for entering integers.", "IntField");
            //FieldButton("DelayedIntField", "Make a delayed text field for entering integers.", "DelayedIntField");
            //FieldButton("DelayedFloatField", "Make a delayed text field for entering floats.", "DelayedFloatField");
            //FieldButton("LongField", "Make a text field for entering long integers.", "LongField");
            //FieldButton("DoubleField", "Make a text field for entering double values.", "DoubleField");
            EditorGUI.indentLevel--;
        }
    }
    private void VectorField(ref bool foldOut)
    {
        EditorGUI.BeginChangeCheck();
        EditorGUILayout.GetControlRect(true, 16f, EditorStyles.foldout);
        Rect foldRect = GUILayoutUtility.GetLastRect();
        //make a check to see if you are clicken on the foldout
        if (Event.current.type == EventType.MouseUp && foldRect.Contains(Event.current.mousePosition))
        {
            foldOut = !foldOut;
            GUI.changed = true;

        }
        EditorGUI.Foldout(foldRect, foldOut, "Vector Field");

        if (foldOut)
        {
            EditorGUI.indentLevel++;
            //FieldButton("Vector2Field", "Make an X & Y field for entering a Vector2.", "Vector2Field");
            //FieldButton("Vector3Field", "Make an X, Y & Z field for entering a Vector3.", "Vector3Field");
            //FieldButton("Vector4Field", "Make an X, Y, Z & W field for entering a Vector4.", "Vector4Field");
            //FieldButton("RectField", "Make an X, Y, W & H field for entering a Rect.", "RectField");
            EditorGUI.indentLevel--;
        }
    }
    private void TextField(ref bool foldOut)
    {
        EditorGUI.BeginChangeCheck();
        EditorGUILayout.GetControlRect(true, 16f, EditorStyles.foldout);
        Rect foldRect = GUILayoutUtility.GetLastRect();
        //make a check to see if you are clicken on the foldout
        if (Event.current.type == EventType.MouseUp && foldRect.Contains(Event.current.mousePosition))
        {
            foldOut = !foldOut;
            GUI.changed = true;

        }
        EditorGUI.Foldout(foldRect, foldOut, "Text");

        if (foldOut)
        {
            EditorGUI.indentLevel++;
            //FieldButton("DelayedTextField", " Make a delayed text field.", "DelayedTextField");
            //FieldButton("PasswordField", "Make a text field where the user can enter a password.", "PasswordField");
            //FieldButton("TextArea", "Make a text area.", "TextArea");
            FieldButton("TextField", "Make a text field.", GUIElm.TextField);
            EditorGUI.indentLevel--;
        }
    }
    private void ListFields(ref bool foldOut)
    {
        EditorGUI.BeginChangeCheck();
        EditorGUILayout.GetControlRect(true, 16f, EditorStyles.foldout);
        Rect foldRect = GUILayoutUtility.GetLastRect();
        //make a check to see if you are clicken on the foldout
        if (Event.current.type == EventType.MouseUp && foldRect.Contains(Event.current.mousePosition))
        {
            foldOut = !foldOut;
            GUI.changed = true;

        }
        EditorGUI.Foldout(foldRect, foldOut, "List Fields");

        if (foldOut)
        {
            EditorGUI.indentLevel++;
            
            EditorGUI.indentLevel--;
        }
    }
    private void Sliders(ref bool foldOut)
    {
        EditorGUI.BeginChangeCheck();
        EditorGUILayout.GetControlRect(true, 16f, EditorStyles.foldout);
        Rect foldRect = GUILayoutUtility.GetLastRect();
        //make a check to see if you are clicken on the foldout
        if (Event.current.type == EventType.MouseUp && foldRect.Contains(Event.current.mousePosition))
        {
            foldOut = !foldOut;
            GUI.changed = true;

        }
        EditorGUI.Foldout(foldRect, foldOut, "Sliders");

        if (foldOut)
        {
            EditorGUI.indentLevel++;
            
            EditorGUI.indentLevel--;
        }
    }
    private void Toggle(ref bool foldOut)
    {
        EditorGUI.BeginChangeCheck();
        EditorGUILayout.GetControlRect(true, 16f, EditorStyles.foldout);
        Rect foldRect = GUILayoutUtility.GetLastRect();
        //make a check to see if you are clicken on the foldout
        if (Event.current.type == EventType.MouseUp && foldRect.Contains(Event.current.mousePosition))
        {
            foldOut = !foldOut;
            GUI.changed = true;

        }
        EditorGUI.Foldout(foldRect, foldOut, "Toggle");

        if (foldOut)
        {
            EditorGUI.indentLevel++;
           
            EditorGUI.indentLevel--;
        }
    }

    private void FieldButton(string name, string toolTip, GUIElm field)
    {
        if (testWindow == null)
            testWindow = new TestWindow(this);

        bool buttonDown = false;
        GUILayout.BeginHorizontal();
        GUILayout.Label(name, EditorStyles.boldLabel);
        MyGUIExtension.CustomToolTip(toolTip, GUILayoutUtility.GetLastRect(), this);
        if (GUILayout.Button("New", GUILayout.Width(75)))
        {
            switch (field)
            {
                case GUIElm.FloatField:
                    buttonDown = true;
                    rootWindow.AddGUI(new FloatField(testWindow, ScriptFields));
                    break;
                case GUIElm.TextField:
                    buttonDown = true;
                    rootWindow.AddGUI(new TextField(testWindow, ScriptFields));
                    break;
            }
        }
        GUILayout.EndHorizontal();
        if (buttonDown)
            testWindow.Repaint();
    }
}
