using UnityEngine;

namespace Rito.CAT
{
    // 2023-10-21 오후 7:43 
    /// <summary
    /// 지정한 필드 값이 지정 값과 일치하는 경우에만 프로퍼티 표시
    /// </summary>
    public class ShowIfAttribute : IfAttributeBase
    {
        public ShowIfAttribute(string targetField, object value) : base(targetField, value) { }
    }
}