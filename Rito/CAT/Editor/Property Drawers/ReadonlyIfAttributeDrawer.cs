#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace Rito.CAT.EditorDrawer
{
    using Editor;

    /// <summary>
    /// <para/> 2020-05-18 PM 3:29:49
    /// <para/> 
    /// </summary>
    [CustomPropertyDrawer(typeof(ReadonlyIfAttribute), true)]
    public class ReadonlyIfAttributeDrawer : RitoPropertyDrawer<ReadonlyIfAttribute>
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginDisabledGroup(Predicate(property));
            EditorGUI.PropertyField(position, property, label);
            EditorGUI.EndDisabledGroup();
        }

        private bool Predicate(SerializedProperty property)
        {
            object targetValue = EditorSerializedObjectHelper.GetObjectValue(
                property.serializedObject.FindProperty(Atr.TargetField));
            if (targetValue != null)
            {
                if (Atr.Value != null)
                {
                    bool predicate = targetValue.Equals(Atr.Value);
                    //return Atr.EqualsOrNotEquals ? predicate : !predicate;
                    return Atr.EqualsOrNotEquals == predicate; // XNOR
                }
            }
            return false;
        }
    }
}
#endif