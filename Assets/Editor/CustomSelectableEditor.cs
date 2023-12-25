using UnityEditor;
using UnityEditor.UI;

[CustomEditor(typeof(CustomSelectable))]
public class CustomSelectableEditor : SelectableEditor
{
    private SerializedProperty m_SelectableTargets;

    protected override void OnEnable()
    {
        m_SelectableTargets = serializedObject.FindProperty("SelectableTargets");
        base.OnEnable();
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        serializedObject.Update();
        EditorGUILayout.PropertyField(m_SelectableTargets);
        serializedObject.ApplyModifiedProperties();
    }
}
