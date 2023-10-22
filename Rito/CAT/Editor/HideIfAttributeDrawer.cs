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
    [CustomPropertyDrawer(typeof(HideIfAttribute), true)]
    public class HideIfAttributeDrawer : PropertyDrawer
    {
        HideIfAttribute Atr
        {
            get
            {
                if (_atr == null)
                    _atr = attribute as HideIfAttribute;
                return _atr;
            }
        }
        HideIfAttribute _atr;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return Predicate(property) ? EditorGUI.GetPropertyHeight(property, label, true) : 0f;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (Predicate(property))
                EditorGUI.PropertyField(position, property, label);
        }

        private bool Predicate(SerializedProperty property)
        {
            object targetValue = EditorSerializedObjectHelper.GetObjectValue (
                property.serializedObject.FindProperty(Atr.TargetField));
            if (targetValue != null)
            {
                if (Atr.Value != null)
                {
                    return !targetValue.Equals(Atr.Value);
                }
            }
            return true;
        }

    }
}
#endif