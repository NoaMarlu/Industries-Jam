//
//  https://kitty-pool.com/ss015/
//  Inspector の説明を日本語で表示する
//  （リストの場合は名前の後にインデックス数を表示するようにする）
// 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class NamedArrayAttribute : PropertyAttribute
{
    public readonly string Label;

    public NamedArrayAttribute(string names) { this.Label = names; }
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(NamedArrayAttribute))]
public class NamedArrayDrawerEditor : PropertyDrawer
{
    public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
    {
        try
        {
            int pos = int.Parse(property.propertyPath.Split('[', ']')[1]) + 1;
            EditorGUI.PropertyField(rect, property, new GUIContent(((NamedArrayAttribute)attribute).Label + pos));
        }
        catch
        {
            EditorGUI.PropertyField(rect, property, label);
        }
    }
}
#endif