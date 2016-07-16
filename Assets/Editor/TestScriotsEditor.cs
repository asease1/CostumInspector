using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
[CustomEditor(typeof(TestScriots))]
[CanEditMultipleObjects]
public class TestScriotsEditor : Editor{
private Rect rectPosition;
private float height;
SerializedProperty test1Prop;
void OnEnable () {
test1Prop = serializedObject.FindProperty ("test1");
}
public override void OnInspectorGUI(){
serializedObject.Update ();
TestScriots myTarget = (TestScriots)target;
height = EditorGUILayout.GetControlRect(true, GUILayout.Height(94)).y;
rectPosition = new Rect(21,69+height, 70,20); 
test1Prop.stringValue = EditorGUI.TextField(rectPosition, test1Prop.stringValue);
serializedObject.ApplyModifiedProperties ();
}
}
