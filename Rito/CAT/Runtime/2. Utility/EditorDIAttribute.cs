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
        //public EModeOption ModeOption { get; private set; } = EModeOption.Always;

        /// <summary>
        /// 비활성화된 오브젝트를 모두 탐색
        /// </summary>
        public bool IncludeDisabledObject { get; private set; }

        /// <summary>
        /// 게임오브젝트 이름에 해당 문자열이 포함되는 경우 탐색
        /// </summary>
        public string NameIncludes { get; private set; }

        public EditorDIAttribute(DiMethod method, bool evenDisabled = true, string nameIncludes = null)
        {
            this.Method = method;
            this.IncludeDisabledObject = evenDisabled;
            this.NameIncludes = nameIncludes;
        }

        public EditorDIAttribute(DiMethod method, string nameIncludes) : this(method, true, nameIncludes) { }
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

    // 실행할 타이밍
    //public enum EModeOption
    //{
    //    EditModeOnly,
    //    //Always // TODO: 리플렉션 활용한 DI (GetComponentAttributes 라이브러리 사용)
    //}

    //public enum DiNameMethod
    //{
    //    None = 0,
    //    Contains = 1,
    //    Equals = 2,
    //}

    //public readonly struct NameContains
    //{
    //    public readonly string name;
    //    public NameContains(string name) => this.name = name;
    //}
    //public readonly struct NameEquals
    //{
    //    public readonly string name;
    //    public NameEquals(string name) => this.name = name;
    //}
}