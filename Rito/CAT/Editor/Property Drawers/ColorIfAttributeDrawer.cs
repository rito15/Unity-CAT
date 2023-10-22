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
    [CustomPropertyDrawer(typeof(ColorIfAttribute), true)]
    public class ColorIfAttributeDrawer : RitoPropertyDrawer<ColorIfAttribute>
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Color old = GUI.color;
            if(Predicate(property))
                GUI.color = new Color(Atr.R, Atr.G, Atr.B, 1f);
            EditorGUI.PropertyField(position, property, label);
            GUI.color = old;
        }

        private bool Predicate(SerializedProperty property)
        {
            object targetValue = EditorSerializedObjectHelper.GetObjectValue(
                property.serializedObject.FindProperty(Atr.TargetField));
            if (targetValue != null)
            {
                if (Atr.Value != null)
                {
                    return targetValue.Equals(Atr.Value);
                }
            }
            return false;
        }
    }
}
#endif