using UnityEngine;

namespace Rito.CAT
{
    // 2023-10-21 오후 7:43 
    /// <summary
    /// 지정한 필드 값이 지정 값과 일치하는 경우 Readonly
    /// </summary>
    public class ReadonlyIfAttribute : IfAttributeBase
    {
        /// <summary> true: 조건이 부합되는 경우 Readonly 상태, false: 조건이 다른 경우 Readonly 상태 전환 </summary>
        public bool EqualsOrNotEquals { get; set; } = true;

        public ReadonlyIfAttribute(string targetField, object value, bool EQorNE=true) : base(targetField, value) 
        {
            EqualsOrNotEquals = EQorNE;
        }
    }
}