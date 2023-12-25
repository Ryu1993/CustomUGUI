using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UI;
using UnityEngine;

[CustomEditor(typeof(MaskedImage))]
public class MaskedImageEditor : ImageEditor
{
    private SerializedProperty m_MaskGraphic;
    protected override void OnEnable()
    {
        base.OnEnable();
        m_MaskGraphic = serializedObject.FindProperty("m_MaskGraphic");
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        serializedObject.Update();
        EditorGUILayout.PropertyField(m_MaskGraphic);
        serializedObject.ApplyModifiedProperties();
    }
}
