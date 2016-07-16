using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class TestWindow : EditorWindow
{
    public CreateInspector mainWindow;
    private GUIElements currentMoving, rightClicked;
    
    public TestWindow(CreateInspector window)
    {
        mainWindow = window;
    }

    void OnGUI()
    {
        
        if (mainWindow == null)
            this.Close();


        foreach (GUIElements elm in mainWindow.rootWindow.GUIelements)
        {
            if (currentMoving == null)
                MyGUIExtension.DragAndDrop(elm, ref currentMoving, this);
            if(rightClicked == null)
                MyGUIExtension.SelectRect(elm, ref rightClicked);
        }

        EditorGUI.BeginDisabledGroup(true);
        Color guiColor = GUI.color;
        guiColor.a = 2;
        GUI.color = guiColor;
        EditorGUILayout.InspectorTitlebar(true, new UnityEngine.Object(), false);
        mainWindow.rootWindow.RenderCall();
        EditorGUI.EndDisabledGroup();

        if (currentMoving != null)
            MyGUIExtension.DragAndDrop(currentMoving, ref currentMoving, this);

        if (rightClicked != null)
        {     
            rightClicked.RightClickMenu();
            if (rightClicked.deleteThis)
            {
                mainWindow.rootWindow.Remove(rightClicked);
                rightClicked = null;
            }
            else
                MyGUIExtension.SelectRect(rightClicked, ref rightClicked);
            Repaint();
        }
    }
}

