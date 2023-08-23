#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace Rito.CAT.Drawer
{
    // 2023. 08. 23. TODO: PropertyDrawer로 변경하여 기존 프로퍼티를 감추거나 표시할 수 있도록 수정
    
    /// <summary>
    /// 2020. 05. 15.
    /// <para/> 헤더 한 줄 작성 + 여러 필드를 하나의 컬러 박스로 묶어주기
    /// </summary>
    [CustomPropertyDrawer(typeof(MemoBoxAttribute))]
    public class MemoBoxAttributeDrawer : DecoratorDrawer
    {
        MemoBoxAttribute Atr => attribute as MemoBoxAttribute;

        float LineHeight => Atr.FontSize + Atr.LineSpacing ;

        public override float GetHeight()
        {
            float contentHeight = Atr.Contents.Length * LineHeight;
            float finalHeight = 
                contentHeight + 
                Atr.MarginTop + Atr.MarginBottom + Atr.PaddingTop + Atr.PaddingBottom
            ;
            return finalHeight;
        }
        
        public override void OnGUI(Rect position)
        {
            float textHeight = Atr.Contents.Length * LineHeight;
            float boxHeight = textHeight + Atr.PaddingTop + Atr.PaddingBottom;
        
            float boxWidth = position.width;
            float textWidth = boxWidth - Atr.PaddingLeft;
        
            float boxX = position.x;
            float textX = boxX + Atr.PaddingLeft;
        
            float boxY = position.y + Atr.MarginTop;
            float textY = boxY + Atr.PaddingTop;
        
            Rect boxRect = new Rect(boxX, boxY, boxWidth, boxHeight);
        
            Color textColor = EColorConverter.Convert(Atr.TextColor);
            Color boxColor = EColorConverter.Convert(Atr.BoxColor);
        
            EditorGUI.DrawRect(boxRect, boxColor);
        
            GUIStyle labelStyle = Atr.BoldText ? EditorStyles.boldLabel : EditorStyles.label;
        
            // Remember Olds
            Color oldStyleTextColor = labelStyle.normal.textColor;
            int oldTextSize = labelStyle.fontSize;
        
            // Custom Text Color
            labelStyle.normal.textColor = textColor;
            labelStyle.fontSize = Atr.FontSize;
        
            // Header Label
            float curPosY = textY;
            foreach (var text in Atr.Contents)
            {
                Rect curRect = new Rect(textX, curPosY, textWidth, LineHeight);
        
                EditorGUI.LabelField(curRect, text, labelStyle);
                curPosY += LineHeight;
            }
        
            // Restore Olds
            labelStyle.normal.textColor = oldStyleTextColor;
            labelStyle.fontSize = oldTextSize;
        }

        // public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        // {
        //     float propHeight = EditorGUI.GetPropertyHeight(property, label, true);
        //     float contentHeight = 
        //         Atr.Contents.Length * LineHeight +
        //         Atr.MarginTop + Atr.MarginBottom + Atr.PaddingTop + Atr.PaddingBottom
        //     ;
        //     float finalHeight = 
        //         contentHeight + (Atr.hideProperty ? 0 : propHeight)
        //     ;
        //     return finalHeight;
        // }
        //
        // public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        // {
        //     float textHeight = Atr.Contents.Length * LineHeight;
        //     float boxHeight = textHeight + Atr.PaddingTop + Atr.PaddingBottom;
        //
        //     float boxWidth = position.width;
        //     float textWidth = boxWidth - Atr.PaddingLeft;
        //
        //     float boxX = position.x;
        //     float textX = boxX + Atr.PaddingLeft;
        //
        //     float boxY = position.y + Atr.MarginTop;
        //     float textY = boxY + Atr.PaddingTop;
        //
        //     if (!Atr.hideProperty)
        //         boxHeight -= EditorGUI.GetPropertyHeight(property, label, true);
        //     Rect boxRect = new Rect(boxX, boxY, boxWidth, boxHeight);
        //
        //     Color textColor = EColorConverter.Convert(Atr.TextColor);
        //     Color boxColor = EColorConverter.Convert(Atr.BoxColor);
        //
        //     EditorGUI.DrawRect(boxRect, boxColor);
        //
        //     GUIStyle labelStyle = Atr.BoldText ? EditorStyles.boldLabel : EditorStyles.label;
        //
        //     // Remember Olds
        //     Color oldStyleTextColor = labelStyle.normal.textColor;
        //     int oldTextSize = labelStyle.fontSize;
        //
        //     // Custom Text Color
        //     labelStyle.normal.textColor = textColor;
        //     labelStyle.fontSize = Atr.FontSize;
        //
        //     // Header Label
        //     float curPosY = textY;
        //     foreach (var text in Atr.Contents)
        //     {
        //         Rect curRect = new Rect(textX, curPosY, textWidth, LineHeight);
        //
        //         EditorGUI.LabelField(curRect, text, labelStyle);
        //         curPosY += LineHeight;
        //     }
        //
        //     // Restore Olds
        //     labelStyle.normal.textColor = oldStyleTextColor;
        //     labelStyle.fontSize = oldTextSize;
        //
        //     if (!Atr.hideProperty)
        //     {
        //         float propHeight = EditorGUI.GetPropertyHeight(property, label, true);
        //         Rect propRect = boxRect;
        //         propRect.y = propRect.y + (propRect.height - propHeight);
        //         propRect.height = propHeight;
        //         EditorGUI.PropertyField(propRect, property, label, includeChildren: true);
        //     }
        // }
    }
}
#endif