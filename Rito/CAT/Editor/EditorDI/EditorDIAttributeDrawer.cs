#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System;

namespace Rito.CAT.EditorDrawer
{
    /// <summary>
    /// <para/> 2020-05-18 PM 9:15:30
    /// <para/> Component 상속 타입 변수들에 자동으로 초기화
    /// </summary>
    [CustomPropertyDrawer(typeof(EditorDIAttribute), true)]
    public class EditorDIAttributeDrawer : PropertyDrawer
    {
        #region Props

        EditorDIAttribute Atr => attribute as EditorDIAttribute;

        private float Height { get; set; } =
#if UNITY_2019_3_OR_NEWER
            18;
#else
            16f;
#endif

        private bool IsPlayMode => EditorApplication.isPlaying;

        private static bool IsFullDeco =>
            EditorDIPrefs.OnOff.Value &&
            EditorDIPrefs.ShowDeco.Value &&
            EditorDIPrefs.FullDeco.Value;
        private static bool IsMiniDeco =>
            EditorDIPrefs.OnOff.Value &&
            EditorDIPrefs.ShowDeco.Value &&
            EditorDIPrefs.FullDeco.Value == false;
        private static bool IsPreviewDeco =>
            EditorDIPrefs.OnOff.Value == false &&
            EditorDIPrefs.ShowDeco.Value;

        #endregion

        #region Icons
        private static GUIContent _iconGreen;
        private static GUIContent IconGreen
        {
            get
            {
                if (_iconGreen == null)
                {
                    //_iconGreen = EditorGUIUtility.IconContent("winbtn_mac_max");
                    _iconGreen = EditorGUIUtility.IconContent("sv_icon_dot3_sml");
                    //_iconGreen = EditorGUIUtility.IconContent("d_greenLight");
                }
                return _iconGreen;
            }
        }
        private static GUIContent _iconCyan;
        private static GUIContent IconCyan
        {
            get
            {
                if (_iconCyan == null)
                {
                    //_iconCyan = EditorGUIUtility.IconContent("winbtn_mac_max_h");
                    _iconCyan = EditorGUIUtility.IconContent("sv_icon_dot2_sml");
                    //_iconCyan = EditorGUIUtility.IconContent("sv_icon_dot10_sml");
                }
                return _iconCyan;
            }
        }

        private static GUIContent _iconYellow;
        private static GUIContent IconYellow
        {
            get
            {
                if (_iconYellow == null)
                {
                    //_iconYellow = EditorGUIUtility.IconContent("winbtn_mac_min");
                    _iconYellow = EditorGUIUtility.IconContent("sv_icon_dot4_sml");
                    //_iconYellow = EditorGUIUtility.IconContent("d_orangeLight");
                }
                return _iconYellow;
            }
        }

        private static GUIContent _iconRed;
        private static GUIContent IconRed
        {
            get
            {
                if (_iconRed == null)
                {
                    //_iconRed = EditorGUIUtility.IconContent("winbtn_mac_close");
                    _iconRed = EditorGUIUtility.IconContent("sv_icon_dot6_sml");
                    //_iconRed = EditorGUIUtility.IconContent("d_redLight");
                }
                return _iconRed;
            }
        }

        private static GUIContent _iconPreview1;
        private static GUIContent IconPreviewWhite
        {
            get
            {
                if (_iconPreview1 == null)
                    _iconPreview1 = EditorGUIUtility.IconContent("sv_icon_dot0_sml");
                return _iconPreview1;
            }
        }

        private static GUIContent _iconPreview2;
        private static GUIContent IconPreviewCyan
        {
            get
            {
                if (_iconPreview2 == null)
                    _iconPreview2 = EditorGUIUtility.IconContent("sv_icon_dot2_sml");
                return _iconPreview2;
            }
        }

        private static GUIContent _iconPreviewRed;
        private static GUIContent IconPreviewRed
        {
            get
            {
                if (_iconPreviewRed == null)
                    _iconPreviewRed = EditorGUIUtility.IconContent("sv_icon_dot6_sml");
                return _iconPreviewRed;
            }
        }

        #endregion

        #region Editor GUI

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (IsPlayMode) return Height;
            if (IsFullDeco) return Height * 2.5f;
            return Height;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // 1. 동작하지 않음
            if (IsPlayMode || EditorDIPrefs.OnOff.Value == false)
            {
                if (IsPreviewDeco)
                {
                    DrawPreviewGUI(position, property, label);
                }
                else
                {
                    EditorGUI.PropertyField(position, property, label, true);
                }
            }
            // 2. DI 동작
            else
            {
                DrawInjectionGUI(position, property, label);
            }
        }

        #endregion

        private void DrawPreviewGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            const float MiniW = 20f;
            Rect miniRect = new Rect(position.x - MiniW, position.y, MiniW, position.height);

            EditorGUI.PropertyField(position, property, label, true);

            //Component foundNamedTarget = Atr.NameIncludes == null ? null : 
            //    ComponentHelper.FindComponentInScene_NC(fieldInfo.FieldType, Atr.NameIncludes, Atr.IncludeDisabledObject);

            Color bg = GUI.backgroundColor;
            GUI.backgroundColor = Atr.IncludeInactive ? new Color(0.3f, 0.3f, 0.3f, 0.5f) : new Color(1f, 1f, 1f, 0.3f);
            GUI.Button(miniRect, Atr.NameIncludes == null ? IconPreviewWhite : IconPreviewCyan);
            GUI.backgroundColor = bg;

            DrawPreviewTooltip(miniRect);
        }

        private void DrawInjectionGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            float propH  = IsFullDeco ? Height * 1.5f : 0f;
            float errorH = IsFullDeco ? Height * 2f   : Height;
            float errorY = IsFullDeco ? Height * 0.5f : 0f;

            // -
            Rect infoRect  = new Rect(position.x, position.y + Height * 0.5f, position.width, Height);
            Rect propRect  = new Rect(position.x, position.y + propH        , position.width, Height);
            Rect errorRect = new Rect(position.x, position.y + errorY       , position.width, errorH);
            Type fieldType = fieldInfo.FieldType;

            const float ResetBtnW = 20f;
            Rect infoRectL = new Rect(position.x, position.y + Height * 0.5f, position.width - ResetBtnW, Height);
            Rect infoRectR = new Rect(position.x + position.width - ResetBtnW, position.y + Height * 0.5f, ResetBtnW, Height);

            bool isTargetGameObject = false;

            // 2023. 10. 14. GameObject 타입 타겟 추가
            if (fieldType.Equals(typeof(GameObject)))
            {
                // ComponentType을 설정하지 않은 경우: Transform 타입으로 자동 설정
                if (Atr.ComponentType == null)
                {
                    isTargetGameObject = true;
                    Atr.ComponentType = typeof(Transform);
                    fieldType = Atr.ComponentType;
                }
                // 게임오브젝트를 대상으로 하려면 올바른 ComponentType 설정 반드시 필요
                else if (Atr.ComponentType.IsSubclassOf(typeof(Component)))
                {
                    isTargetGameObject = true;
                    fieldType = Atr.ComponentType;
                }
                else
                {
                    EditorHelper.ColorErrorBox(errorRect,
                        $"[{fieldType}] {fieldInfo.Name}\n" +
                        $"Error : ComponentTypeOfGameObject 옵션에 Component를 상속하는 타입을 지정하세요."
                    );
                    return;
                }

                //// ComponentType 프로퍼티를 설정하지 않은 경우 -> 자동으로 typeof(Component) 초기화
                //if (Atr.ComponentType == null ||
                //    !Atr.ComponentType.IsSubclassOf(typeof(Component)))
                //{
                //    Atr.ComponentType = typeof(Component);
                //    fieldType = Atr.ComponentType;
                //}
                //isTargetGameObject = true;
            }

            // 컴포넌트 상속 타입이 아닌 타입 - 에러박스
            if (!isTargetGameObject && fieldType.IsSubclassOf(typeof(Component)) == false)
            {
                // 배열, 리스트면 크기를 0으로 고정하고 콘솔 에러 메시지
                if (fieldType.Ex_IsArrayOrListType())
                {
                    fieldInfo.SetValue(property.serializedObject.targetObject, null);
                    Debug.LogError("[AutoInject] 배열 또는 리스트에는 사용할 수 없습니다.");
                }

                if (IsFullDeco)
                {
                    // 그 외의 경우, 인스펙터에 큼지막한 에러 박스
                    EditorHelper.ColorErrorBox(errorRect,
                        $"[{fieldType}] {fieldInfo.Name}\n" +
                        $"Error : Component를 상속하는 타입에만 사용할 수 있습니다"
                    );
                }
                else
                {
                    EditorHelper.ColorErrorBox(errorRect,
                        $"[{fieldType}] {fieldInfo.Name}"
                    );
                }
                return;
            }

            // 컴포넌트 타입 - 올바르게 동작 ===========================================================
            bool isRefDirty;

            // 업데이트 필요 대상(기존 레퍼런스가 null이거나 미스매치)
            bool isUpdateRequired =
                property.objectReferenceValue == null ||
                property.objectReferenceValue.GetType().Ex_IsChildOrEqualsTo(fieldType) == false;

            UnityEngine.Object foundTarget;
            string includeDisabled = Atr.IncludeInactive ? " (+ Disabled)" : "";

            // 인젝션
            {
                Component @this = property.serializedObject.targetObject as Component;

                // Inject 수행
                if(Atr.NameIncludes == null && Atr.NameEquals == null)
                    EditorDIHelper.Inject(@this, Atr, fieldType, out foundTarget);
                else
                    EditorDIHelper.Inject_NC(
                        @this, Atr, fieldType, Atr.NameIncludes, Atr.NameEquals, Atr.IgnoreCase, out foundTarget);

                // 게임오브젝트 타겟인 경우, 찾은 컴포넌트의 게임오브젝트 추출
                if (isTargetGameObject && foundTarget != null)
                {
                    foundTarget = (foundTarget as Component).gameObject;
                }

                // 업데이트 필수 대상
                if (isUpdateRequired)
                {
                    property.objectReferenceValue = foundTarget;
                    isRefDirty = false;
                }
                // 기존 <-> 새로 찾은 대상 일치 판단
                else
                {
                    isRefDirty = property.objectReferenceValue != foundTarget;
                }
            }

            // 인젝션 실패함
            bool isInjectionFailed = property.objectReferenceValue == null;

            // 풀 데코레이터 표시 여부
            if (IsFullDeco)
            {
                // 실행 결과 - 할당 성공
                if (property.objectReferenceValue != null)
                {
                    string foundObjName = foundTarget == null ? "NULL" : foundTarget.name;
                    string dirtyText = isRefDirty ? $"[* {foundObjName}]" : "";

                    EditorHelper.ColorInfoBox(infoRectL, Color.green, $"{Atr.Method}{includeDisabled} {dirtyText}");
                    if (GUI.Button(infoRectR, "")) // 강제 리셋 버튼
                    {
                        property.objectReferenceValue = null;
                    };
                }
                // 실행 결과 - 실패(대상이 없음)
                else
                {
                    EditorHelper.ColorWarningBox(infoRect, $"{Atr.Method}{includeDisabled} - Failed : 대상을 찾지 못했습니다");
                }
            }

            Rect miniRect = default;
            // 동작은 하지만 데코는 안보여주는 경우
            if (IsMiniDeco)
            {
                const float MiniW = 20f;
                miniRect = new Rect(position.x - MiniW, position.y, MiniW, position.height);

                GUIContent icon =
                    isInjectionFailed ? IconRed : 
                    isRefDirty ? IconYellow : 
                    Atr.NameIncludes != null ? IconCyan :
                    IconGreen;

                Color oldCol = GUI.backgroundColor;
                if (Atr.IncludeInactive) // Disabled 포함이면 검정 버튼
                {
                    GUI.backgroundColor = new Color(0.3f, 0.3f, 0.3f);
                }
                if (GUI.Button(miniRect, icon)) // 상태 표시 + 강제 리셋 버튼
                {
                    property.objectReferenceValue = null;
                };
                GUI.backgroundColor = oldCol;
            }
            EditorGUI.PropertyField(propRect, property, label, true);

            if (IsMiniDeco)
            {
                DrawTooltip(
                    miniRect, 
                    foundTarget == null ? "=> NULL" : $"=> {foundTarget.name}",
                    //(foundTarget != null && (foundTarget as Component).gameObject == (property.serializedObject.targetObject as Component).gameObject) ?
                    //    $"{Atr.NameIncludes}(this)" : Atr.NameIncludes,
                    isRefDirty,
                    foundTarget as Component
                );
            }
        }

        private void DrawPreviewTooltip(in Rect eventRect)
        {
            static Rect GetBgRect(Rect rect)
            {
                rect.x -= 1f; rect.width += 2f; rect.y -= 1f; rect.height += 2f; return rect;
            }

            // 툴팁 글자 색상
            Color cMethod = Atr.NameIncludes != null ? Color.cyan : Color.white;
            Color cNameContains = Color.cyan;

            Vector2 mPos =  Event.current.mousePosition;

            string method = $"{Atr.Method}{(Atr.IncludeInactive ? " [D]" : "")}";
            string nameIncludes = Atr.NameIncludes;

            // 스트링으로 너비 계산
            const float WPad = 4f;
            float rWidth1 = GUI.skin.label.CalcSize(new GUIContent(method)).x + WPad;

            if (eventRect.Contains(mPos))
            {
                Rect tooltipRect = new Rect(
                    eventRect.x + eventRect.width + 2f,
                    eventRect.y - 4f,
                    rWidth1,
                    eventRect.height + 4f
                );

                EditorGUI.DrawRect(GetBgRect(tooltipRect), Color.white);
                EditorGUI.DrawRect(tooltipRect, Color.black);

                var aln = GUI.skin.box.alignment;
                GUI.skin.box.alignment = TextAnchor.MiddleLeft;

                Color c = GUI.color;
                GUI.color = cMethod;
                GUI.Box(tooltipRect, method); // 메소드 툴팁
                GUI.color = c;

                // 포함 게임오브젝트명
                if (nameIncludes != null)
                {
                    float rWidth = GUI.skin.label.CalcSize(new GUIContent(nameIncludes)).x + WPad;
                    Rect nameRect =
                        new Rect(
                        tooltipRect.x + tooltipRect.width + 4f,
                        tooltipRect.y,
                        rWidth,
                        tooltipRect.height
                    );

                    EditorGUI.DrawRect(GetBgRect(nameRect), Color.white);
                    EditorGUI.DrawRect(nameRect, Color.black);

                    Color c2 = GUI.color;
                    GUI.color = cNameContains;
                    GUI.Box(nameRect, nameIncludes); // 포함 게임오브젝트명 툴팁
                    GUI.color = c2;
                }

                GUI.skin.box.alignment = aln;
            }
        }

        private void DrawTooltip(in Rect eventRect, string nextName, bool isDirty, Component foundComponent)
        {
            static Rect GetBgRect(Rect rect)
            {
                rect.x -= 1f; rect.width += 2f; rect.y -= 1f; rect.height += 2f; return rect;
            }

            string method = $"{Atr.Method}{(Atr.IncludeInactive ? " [D]" : "")}";
            string nameIncludes = 
                (string.IsNullOrEmpty(Atr.NameEquals)) ? 
                    Atr.NameIncludes : $"{Atr.NameEquals} [EQ]"
            ;

            // 툴팁 글자 색상
            Color cMethod = nameIncludes != null ? Color.cyan : Color.green;
            Color cNameContains = (nameIncludes != null && foundComponent != null) ? 
                (foundComponent.gameObject.activeInHierarchy ? Color.white : Color.gray) : Color.red;

            Vector2 mPos =  Event.current.mousePosition;

            // 스트링으로 너비 계산
            const float WPad = 4f;
            float rWidth1 = GUI.skin.label.CalcSize(new GUIContent(method)).x + WPad;

            if (eventRect.Contains(mPos))
            {
                Rect tooltipRect = new Rect(
                    eventRect.x + eventRect.width + 2f,
                    eventRect.y - 4f,
                    rWidth1,
                    eventRect.height + 4f
                );

                EditorGUI.DrawRect(GetBgRect(tooltipRect), Color.white);
                EditorGUI.DrawRect(tooltipRect, Color.black);

                var aln = GUI.skin.box.alignment;
                GUI.skin.box.alignment = TextAnchor.MiddleLeft;

                Color c = GUI.color;
                GUI.color = cMethod;
                GUI.Box(tooltipRect, method); // 메소드 툴팁
                GUI.color = c;

                // 포함 게임오브젝트명
                if (nameIncludes != null)
                {
                    float rWidth = GUI.skin.label.CalcSize(new GUIContent(nameIncludes)).x + WPad;
                    Rect nameRect =
                        new Rect(
                        tooltipRect.x + tooltipRect.width + 4f,
                        tooltipRect.y,
                        rWidth,
                        tooltipRect.height
                    );

                    EditorGUI.DrawRect(GetBgRect(nameRect), Color.white);
                    EditorGUI.DrawRect(nameRect, Color.black);

                    // 더블 클릭 시 대상으로 포커스
                    //if (Event.current.clickCount > 1 && Event.current.type == EventType.Used)
                    //{
                    //    if(foundNamedTarget != null)
                    //        Selection.activeObject = foundNamedTarget;
                    //}

                    Color c2 = GUI.color;
                    GUI.color = cNameContains;
                    GUI.Box(nameRect, nameIncludes); // 포함 게임오브젝트명 툴팁
                    GUI.color = c2;
                }

                // 변경될 참조명
                if (isDirty)
                {
                    float rWidth = GUI.skin.label.CalcSize(new GUIContent(nextName)).x + WPad;
                    Rect nextNameRect = tooltipRect;
                    nextNameRect.y = nextNameRect.y - nextNameRect.height + 2f;
                    nextNameRect.width = rWidth;

                    EditorGUI.DrawRect(GetBgRect(nextNameRect), Color.white);
                    EditorGUI.DrawRect(nextNameRect, Color.black);

                    Color c2 = GUI.color;
                    GUI.color = Color.yellow;
                    GUI.Box(nextNameRect, nextName);
                    GUI.color = c2;
                }

                GUI.skin.box.alignment = aln;
            }
        }
    }
}
#endif