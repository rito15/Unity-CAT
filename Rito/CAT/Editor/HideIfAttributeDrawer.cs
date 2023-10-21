#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace Rito.CAT.Drawer
{
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
            object targetValue = GetObjectValue(property.serializedObject.FindProperty(Atr.TargetField));
            if (targetValue != null)
            {
                if (Atr.Value != null)
                {
                    return !targetValue.Equals(Atr.Value);
                }
            }
            return true;
        }
        private static object GetObjectValue(SerializedProperty property)
        {
            if (property == null)
                return null;

            switch (property.propertyType)
            {
                case SerializedPropertyType.Integer:
                    return property.intValue;
                case SerializedPropertyType.Boolean:
                    return property.boolValue;
                case SerializedPropertyType.Float:
                    return property.floatValue;
                case SerializedPropertyType.String:
                    return property.stringValue;
                case SerializedPropertyType.Color:
                    return property.colorValue;
                case SerializedPropertyType.ObjectReference:
                    return property.objectReferenceValue;
                case SerializedPropertyType.LayerMask:
                    return property.intValue;
                case SerializedPropertyType.Enum:
                    return property.enumValueIndex;
                case SerializedPropertyType.Vector2:
                    return property.vector2Value;
                case SerializedPropertyType.Vector3:
                    return property.vector3Value;
                case SerializedPropertyType.Vector4:
                    return property.vector4Value;
                default:
                    return null;
            }
        }

    }
}
#endif