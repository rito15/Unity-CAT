#if UNITY_EDITOR
using System;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace Rito.CAT.Drawer
{
    /// <summary>
    /// <para/> 2023-08-23 PM 2:45:53
    /// <para/> 스트링 - 사용 가능한 레이어 드롭다운 표시
    /// </summary>
    [CustomPropertyDrawer(typeof(StringDropDownAttribute), true)]
    public class StringDropDownAttributeDrawer : PropertyDrawer
    {
        private StringDropDownAttribute Atr => attribute as StringDropDownAttribute;
        
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (EditorHelper.CheckDuplicatedAttribute<DropDownAttributeBase>(fieldInfo))
            {
                EditorGUI.HelpBox(position, "DropDownAttribute는 중복 사용될 수 없습니다", MessageType.Error);
                return;
            }

            if (!property.propertyType.Equals(SerializedPropertyType.String))
            {
                Local_ShowErrorBox("string 타입에만 적용할 수 있습니다.");
                return;
            }

            if (Atr.Options == null || Atr.Options.Length == 0)
            {
                Local_ShowErrorBox("지정한 스트링 배열의 크기가 0입니다.");
                return;
            }

            float ratio = 0.4f;
            float widthLeft = position.width * ratio;
            float widthRight = position.width * (1 - ratio);

            Rect rectLeft = new Rect(position.x, position.y, widthLeft, position.height);
            Rect rectRight = new Rect(position.x + widthLeft, position.y, widthRight, position.height);

            // 1. 좌측 레이블 그리기
            EditorGUI.LabelField(rectLeft, label);

            // 2. 우측 팝업 그리기 + 값 할당
            int curIdx = Mathf.Max(0, Array.IndexOf(Atr.Options, property.stringValue));
            int nextIdx = EditorGUI.Popup(rectRight, curIdx, Atr.Options);
            property.stringValue = Atr.Options[nextIdx];
            

            void Local_ShowErrorBox(string msg)
            {
                EditorGUI.HelpBox(position, msg, MessageType.Error);
                Debug.LogError($"[StringDropDownAttribute] {msg}");
            }
        }
    }
}
#endif