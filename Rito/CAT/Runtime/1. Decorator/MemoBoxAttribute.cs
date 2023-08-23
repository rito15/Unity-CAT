using UnityEngine;

namespace Rito.CAT
{
    // 2023. 08. 23. TODO: PropertyDrawer로 변경하여 기존 프로퍼티를 감추거나 표시할 수 있도록 수정
    
    /// <summary>
    /// 2021. 01. 15.
    /// <para/> 프로퍼티 상단에 박스 메모 작성
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.Field, AllowMultiple = true, Inherited = false)]
    public class MemoBoxAttribute : PropertyAttribute
    {
        public string[] Contents { get; private set; }

        // /// <summary> 프로퍼티(필드)를 인스펙터에서 표시/제외할지 여부 </summary>
        // public bool hideProperty { get; set; } = false;

        public int FontSize { get; set; } = 12;
        public int LineSpacing { get; set; } = 2;

        public EColor TextColor { get; set; } = EColor.White;
        public EColor BoxColor { get; set; } = EColor.Black;

        public float MarginTop { get; set; } = 5f;
        public float MarginBottom { get; set; } = 5f;

        public float PaddingTop { get; set; } = 5f;
        public float PaddingBottom { get; set; } = 5f;
        public float PaddingLeft { get; set; } = 5f;

        public bool BoldText { get; set; } = false;

        public MemoBoxAttribute(params string[] contents)
            => Contents = contents;
    }
}
