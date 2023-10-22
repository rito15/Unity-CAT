using UnityEngine;

namespace Rito.CAT
{
    // 2023-10-21 오후 7:43 
    /// <summary
    /// 지정한 필드 값이 지정 값과 일치하는 경우에만 프로퍼티 표시
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.Field, AllowMultiple = true, Inherited = true)]
    public class ShowIfAttribute : PropertyAttribute
    {
        public string TargetField { get; set; }
        public object Value { get; set; }

        public ShowIfAttribute(string targetField, object value)
        {
            TargetField = targetField;
            Value = value;
        }
    }
}