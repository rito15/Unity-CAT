using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

// 날짜 : 2023-10-21 오후 6:03
// 작성자 : Rito15

namespace Rito.CAT
{
    [System.Serializable]
    public class MethodButton
    {

        public string methodName;
        public string buttonText;
        public int buttonHeight;

        public MethodButton(
            string methodName,
            string buttonText = null,
            int buttonHeight = 20
        )
        {
            this.methodName = methodName;
            this.buttonText = buttonText ?? ConvertPascalCaseToWords(methodName);
            this.buttonHeight = buttonHeight;
        }

        private static string ConvertPascalCaseToWords(string input)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < input.Length; i++)
            {
                char currentChar = input[i];
                if (i > 0 && char.IsUpper(currentChar))
                    sb.Append(' ');
                sb.Append(currentChar);
            }
            return sb.ToString();
        }

        public static implicit operator MethodButton(string str)
        {
            return new MethodButton(str);
        }

        public static implicit operator MethodButton((string methodName, string buttonText) args)
        {
            return new MethodButton(args.methodName, args.buttonText);
        }

        public static implicit operator MethodButton((string methodName, int height) args)
        {
            return new MethodButton(args.methodName, null, args.height);
        }

        public static implicit operator MethodButton((string methodName, string buttonText, int height) args)
        {
            return new MethodButton(args.methodName, args.buttonText, args.height);
        }
    }
}

#if UNITY_EDITOR
namespace Rito.CAT.EditorDrawer
{
    using UnityEditor;
    using System.Reflection;

    [CustomPropertyDrawer(typeof(MethodButton))]
    public class MethodButtonFieldDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return property.FindPropertyRelative("buttonHeight").intValue;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            string methodName = property.FindPropertyRelative("methodName").stringValue;
            string buttonText = property.FindPropertyRelative("buttonText").stringValue;
            float buttonHeight = property.FindPropertyRelative("buttonHeight").intValue;
            
            var target = property.serializedObject.targetObject;

            var method = fieldInfo.DeclaringType.GetMethod(
                methodName,
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static,
                null,
                new Type[] { },
                null
            );

            position.height = buttonHeight;

            if (GUI.Button(position, buttonText))
            {
                if (method != null)
                    method.Invoke(target, null);
                else
                    Debug.LogError($"{methodName} 메소드가 존재하지 않습니다");
            }
        }
    }
}
#endif