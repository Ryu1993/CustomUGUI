using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;


[CustomPropertyDrawer(typeof(TransitionTarget))]
public class TransitionTargetDrawer : PropertyDrawer
{
    private static readonly GUIContent sGraphicLabel = EditorGUIUtility.TrTextContent("TargetGraphic");
    private static readonly GUIContent sTransitionLabel = EditorGUIUtility.TrTextContent("Transition");

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position,label,property);
        SerializedObject transitionObject = null;
        var graphicProperty = property.FindPropertyRelative("m_TargetGraphic");
        var transitionProperty = property.FindPropertyRelative("m_Transition");
        var lineHeight = EditorGUIUtility.singleLineHeight;
        var spacing = EditorGUIUtility.standardVerticalSpacing;
        var fieldRect = new Rect(position.x, position.y, position.width, lineHeight);
        property.isExpanded = EditorGUI.Foldout(fieldRect, property.isExpanded, label);
        
        if (property.isExpanded)
        {
            fieldRect.y += lineHeight + spacing;
            EditorGUI.PropertyField(fieldRect, graphicProperty, sGraphicLabel);
            fieldRect.y += lineHeight + spacing;
            EditorGUI.PropertyField(fieldRect, transitionProperty, sTransitionLabel);
            fieldRect.y += lineHeight + spacing;
            switch (transitionProperty.objectReferenceValue)
            {
                case ColorTransition colorTransition :
                    transitionObject = new SerializedObject(colorTransition);
                    var colorsProperty = transitionObject.FindProperty("Colors");
                    EditorGUI.PropertyField(fieldRect, colorsProperty);
                    fieldRect.y += EditorGUI.GetPropertyHeight(colorsProperty) + spacing;
                    break;
                case SpriteTransition spriteTransition :
                    transitionObject = new SerializedObject(spriteTransition);
                    var spriteProperty = transitionObject.FindProperty("Sprites");
                    EditorGUI.PropertyField(fieldRect, spriteProperty);
                    fieldRect.y += EditorGUI.GetPropertyHeight(spriteProperty) + spacing;
                    break;
                case AnimationTransition animationTriggers :
                    transitionObject = new SerializedObject(animationTriggers);
                    var animationProperty = transitionObject.FindProperty("Triggers");
                    EditorGUI.PropertyField(fieldRect, animationProperty);
                    fieldRect.y += EditorGUI.GetPropertyHeight(animationProperty) + spacing;
                    break;
            }
        }

        if (transitionObject != null)
        {
            transitionObject.ApplyModifiedProperties();
            transitionObject.Dispose();
        }
        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        var lineHeight = EditorGUIUtility.singleLineHeight;
        var spacing = EditorGUIUtility.standardVerticalSpacing;
        var height = lineHeight + spacing;
        if (property.isExpanded)
        {
            var transitionProperty = property.FindPropertyRelative("m_Transition");
            switch (transitionProperty.objectReferenceValue)
            {
                case ColorTransition colorTransition:
                    height += lineHeight*10 + spacing;
                    break;
                case SpriteTransition spriteTransition:
                    height += lineHeight + spacing;
                    height += lineHeight*5.5f + spacing;
                    break;
                case AnimationTransition animationTriggers:
                    height += lineHeight + spacing;
                    height += lineHeight*6.5f + spacing;
                    break;
                default:
                    height += lineHeight*2 + spacing;
                    break;
            }
        }
        return height;
    }

}
