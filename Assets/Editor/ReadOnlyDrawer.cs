using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReadOnlyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        bool previousEnabled = GUI.enabled;
        GUI.enabled = false;

        Color previousColor = GUI.color;
        GUI.color = new Color(0.7f, 0.7f, 0.7f);

        EditorGUI.PropertyField(position, property, label, true);

        GUI.color = previousColor;
        GUI.enabled = previousEnabled;
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property, label, true);
    }
}