using UnityEngine;

namespace Rito.CAT
{
    /// <summary>
    /// <para/> 2023-08-23 PM 2:45:53
    /// <para/> 스트링 - 사용 가능한 드롭다운 목록 표시
    /// </summary>
    public class StringDropDownAttribute : DropDownAttributeBase
    {
        public string[] Options { get; private set; }

        public StringDropDownAttribute(params string[] options)
        {
            this.Options = options;
        }
    }
}