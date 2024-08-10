using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AmbienceArea))]
[CanEditMultipleObjects]
public class AmbienceAreaEditor : Editor
{
    SerializedProperty transitionTime;

    private void OnEnable()
    {
        // Link the serialized property
        transitionTime = serializedObject.FindProperty("TransitionTime");
    }

    public override void OnInspectorGUI()
    {
        // Update the serialized object
        serializedObject.Update();

        // Begin changes check
        EditorGUI.BeginChangeCheck();

        // Display only the TransitionTime field
        EditorGUILayout.PropertyField(transitionTime, new GUIContent("Transition Time"));

        // Apply changes
        if (EditorGUI.EndChangeCheck())
        {
            // Apply the property modifications to the target object(s)
            serializedObject.ApplyModifiedProperties();
        }
    }
}
