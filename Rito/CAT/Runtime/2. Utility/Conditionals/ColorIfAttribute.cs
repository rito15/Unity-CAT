using UnityEngine;

namespace Rito.CAT
{
    // 2023-10-21 오후 7:43 
    /// <summary
    /// 지정한 필드 값이 지정 값과 일치하는 경우, 색상 변경
    /// </summary>
    public class ColorIfAttribute : IfAttributeBase
    {
        public float R { get; set; }
        public float G { get; set; }
        public float B { get; set; }

        public ColorIfAttribute(
            string targetField, object value, 
            float r, float g, float b,
            float intensity = 1f
        )
            : base(targetField, value)
        {
            R = r * intensity;
            G = g * intensity;
            B = b * intensity;
        }
    }
}