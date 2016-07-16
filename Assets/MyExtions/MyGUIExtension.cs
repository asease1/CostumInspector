using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Collections;

public class MyGUIExtension  {

    private static Texture2D boxText;

    
    ///make a colorfield with a name bye the side
    public static void ColorField(string colorName, ref Color color)
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(colorName);
        color = EditorGUILayout.ColorField(color);
        EditorGUILayout.EndHorizontal();
    }

    public static void CustomToolTip(string text, Rect LastRect, EditorWindow edWindow)
    {
        if (boxText == null)
            boxText = new Texture2D(2, 2);

        Vector2 mousePosition = Event.current.mousePosition;
        //Check if you hover over the rect
        if (LastRect.Contains(mousePosition))
        {
            GUIStyle style = new GUIStyle();
            style.alignment = TextAnchor.MiddleCenter;
            style.font = EditorStyles.boldFont;
            style.fontSize = 12;
            Vector2 textSize = GUI.skin.label.CalcSize(new GUIContent(text));
            if (textSize.x * 1.2f / 2 > mousePosition.x)
            {
                GUI.Box(new Rect(mousePosition.x, mousePosition.y - 35, textSize.x * 1.2f, textSize.y + 2), boxText);
                GUI.Label(new Rect(mousePosition.x + (textSize.x * 1.2f - textSize.x) / 2, mousePosition.y - 35, textSize.x, textSize.y), text, style);
            }
            else if(textSize.x * 1.2f / 2 + mousePosition.x > edWindow.position.width)
            {
                GUI.Box(new Rect(edWindow.position.width - (textSize.x * 1.2f), mousePosition.y - 35, textSize.x * 1.2f, textSize.y + 2), boxText);
                GUI.Label(new Rect(edWindow.position.width - textSize.x *1.1f, mousePosition.y - 35, textSize.x, textSize.y), text, style);
            }
            else
            {
                GUI.Box(new Rect(mousePosition.x - (textSize.x * 1.2f) / 2, mousePosition.y - 35, textSize.x * 1.2f, textSize.y + 2), boxText);
                GUI.Label(new Rect(mousePosition.x - textSize.x / 2, mousePosition.y - 35, textSize.x, textSize.y), text, style);
            }
            edWindow.Repaint();
        }

    }

    public static void DragAndDrop(GUIElements Guielm, ref GUIElements currentMoving, EditorWindow edWin)
    {
        Vector2 mousePosition = Event.current.mousePosition;
        //make a check to see if you are clicken on the foldout
        if (Event.current.type == EventType.MouseDown && Guielm.rect.Contains(mousePosition) && Event.current.button == 0)
        {
            Event.current.Use();
            currentMoving = Guielm;
        }

        else if (Event.current.type == EventType.MouseUp && Event.current.button == 0)
        {
            currentMoving = null;
        }

        if (currentMoving != null)
        {
            Vector2 positionTemp = new Vector2(mousePosition.x - (currentMoving.rect.width / 2), mousePosition.y - (currentMoving.rect.height / 2));
            if (Event.current.shift) {
                positionTemp.x = positionTemp.x - (positionTemp.x % 10);
                positionTemp.y = positionTemp.y - (positionTemp.y % 10);
                Guielm.setPosition(positionTemp);
            }
            else
                Guielm.setPosition(positionTemp);
            edWin.Repaint();
        }
    }

    public static void SelectRect(GUIElements Guielm, ref GUIElements rightClicked)
    {
        Vector2 mousePosition = Event.current.mousePosition;
        //make a check to see if you are clicken on the foldout
        if (Event.current.type == EventType.MouseDown && Guielm.rect.Contains(mousePosition) && Event.current.button == 1)
        {
            Event.current.Use();
            rightClicked = Guielm;
        }
        else if (Event.current.type == EventType.MouseDown && Event.current.button == 1)
        {

            rightClicked = null;
        }
    }
    //Button is the button number
    public static bool ClickRect(Rect rect, int button)
    {
        Vector2 mousePosition = Event.current.mousePosition;
        if (Event.current.type == EventType.MouseDown && rect.Contains(mousePosition) && Event.current.button == button)
        {
            return true;
        }
        return false;
    }

    public static bool ClickRelaeseRect(Rect rect, int button)
    {
        Vector2 mousePosition = Event.current.mousePosition;
        if (Event.current.type == EventType.MouseUp && rect.Contains(mousePosition) && Event.current.button == button)
        {
            return true;
        }
        return false;
    }

    public static bool MouseRelaese(int button)
    {
        
        if (Event.current.type == EventType.MouseUp && Event.current.button == button)
        {
            Debug.Log("dsa");
            return true;
        }
        return false;
    }
}
