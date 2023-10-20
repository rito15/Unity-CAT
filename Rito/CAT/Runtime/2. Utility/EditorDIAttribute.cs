using UnityEngine;

namespace Rito.CAT
{
    /// <summary>
    /// <para/> 2020-05-18 PM 9:12:43
    /// <para/> Component 상속 타입 변수들에 참조를 자동으로 초기화 해주며
    /// <para/> 초기화 이후 애트리뷰트를 지워도 참조를 손실하지 않습니다.
    /// <para/> 자신의 게임오브젝트 또는 컴포넌트가 비활성화 상태여도 기본적으로 동작합니다.
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public class EditorDIAttribute : PropertyAttribute
    {
        public DiMethod Method { get; private set; }

        /// <summary>
        /// 비활성화된 오브젝트를 모두 탐색 (기본 값: true)
        /// </summary>
        public bool IncludeInactive { get; set; }

        /// <summary>
        /// 게임오브젝트 이름에 해당 문자열이 포함되는 경우 탐색
        /// </summary>
        public string NameIncludes { get; set; }

        ////////////////////////////////////// OPTIONAL ///////////////////////////////////
        /// <summary>
        /// NameIncludes, NameEquals 대소문자 구분하지 않음
        /// </summary>
        public bool IgnoreCase { get; set; } = true;

        /// <summary>
        /// 게임오브젝트 이름과 완전히 일치하는 경우 탐색 (NameInclues보다 우선 적용)
        /// </summary>
        public string NameEquals { get; set; } = null;

        /// <summary> 게임오브젝트를 대상으로 한 경우에만 사용: 대상 타입의 컴포넌트를 찾아 게임오브젝트 주입 </summary>
        public System.Type ComponentType { get; set; } = null;

        ////////////////////////////////////// -------- ///////////////////////////////////


        public EditorDIAttribute(DiMethod method, bool includeInactive = true, string nameIncludes = null)
        {
            this.Method = method;
            this.IncludeInactive = includeInactive;
            this.NameIncludes = nameIncludes;
        }

        public EditorDIAttribute(DiMethod method, string nameIncludes) : this(method, true, nameIncludes) { }

        // 전역 사용을 위한 생성자
        public EditorDIAttribute(string nameIncludes) : this(DiMethod.FindObjectOfType, true, nameIncludes) { }

        // 싱글톤 참조를 위한 생성자
        public EditorDIAttribute() : this(DiMethod.FindObjectOfType, true, null) { }
    }

    // 애트리뷰트에서 선택할 옵션
    public enum DiMethod
    {
        GetComponent,

        /// <summary> 자기 게임오브젝트 및 자식 게임오브젝트들을 대상으로 수행합니다. </summary>
        GetComponentInChildren,
        /// <summary> 자기 게임오브젝트를 제외하고 자식 게임오브젝트들을 대상으로 수행합니다. </summary>
        GetComponentInChildrenOnly,

        /// <summary> 자기 게임오브젝트 및 부모 게임오브젝트들을 대상으로 수행합니다. </summary>
        GetComponentInParents,
        /// <summary> 자기 게임오브젝트를 제외하고 부모 게임오브젝트들을 대상으로 수행합니다. </summary>
        GetComponentInParentsOnly,

        FindObjectOfType
    }
}