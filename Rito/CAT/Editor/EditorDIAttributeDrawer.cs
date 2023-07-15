#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System;

namespace Rito.CAT.Drawer
{
    /// <summary>
    /// <para/> 2020-05-18 PM 9:15:30
    /// <para/> Component 상속 타입 변수들에 자동으로 초기화
    /// </summary>
    [CustomPropertyDrawer(typeof(EditorDIAttribute), true)]
    public class EditorDIAttributeDrawer : PropertyDrawer
    {
        EditorDIAttribute Atr => attribute as EditorDIAttribute;

        private float Height { get; set; } =
#if UNITY_2019_3_OR_NEWER
            18;
#else
            16f;
#endif

        private bool IsPlayMode => EditorApplication.isPlaying;

        #region Icons
        private static GUIContent _iconGreen;
        private static GUIContent IconGreen
        {
            get
            {
                if (_iconGreen == null)
                    _iconGreen = EditorGUIUtility.IconContent("winbtn_mac_max");
                return _iconGreen;
            }
        }

        private static GUIContent _iconYellow;
        private static GUIContent IconYellow
        {
            get
            {
                if (_iconYellow == null)
                    _iconYellow = EditorGUIUtility.IconContent("winbtn_mac_min");
                return _iconYellow;
            }
        }

        private static GUIContent _iconRed;
        private static GUIContent IconRed
        {
            get
            {
                if (_iconRed == null)
                    _iconRed = EditorGUIUtility.IconContent("winbtn_mac_close");
                return _iconRed;
            }
        }

        #endregion

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (EditorDIHelperMenu.ItemToggles.OnOff.Value == false) return Height;
            if (EditorDIHelperMenu.ItemToggles.ShowDeco.Value == false) return Height;
            return IsPlayMode ? Height : Height * 2.5f;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // 1. 동작하지 않음
            if (IsPlayMode || EditorDIHelperMenu.ItemToggles.OnOff.Value == false)
            {
                if (EditorDIHelperMenu.ItemToggles.ShowDeco.Value == false)
                    EditorGUI.PropertyField(position, property, label, true);
                else
                    DrawPreviewGUI(position, property, label);
            }
            // 2. DI 동작
            else
            {
                DrawInjectionGUI(position, property, label);
            }
        }

        private void DrawPreviewGUI(Rect position, SerializedProperty property, GUIContent label)
        {

        }

        private void DrawInjectionGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // 데코를 보여줘야 하는 경우
            bool isFullDeco =
                EditorDIHelperMenu.ItemToggles.OnOff.Value &&
                EditorDIHelperMenu.ItemToggles.ShowDeco.Value;
            bool isMiniDeco =
                EditorDIHelperMenu.ItemToggles.OnOff.Value  &&
                EditorDIHelperMenu.ItemToggles.ShowDeco.Value == false;
            float propH  = isFullDeco ? Height * 1.5f : 0f;
            float errorH = isFullDeco ? Height * 2f   : Height;
            float errorY = isFullDeco ? Height * 0.5f : 0f;

            // -
            Rect infoRect  = new Rect(position.x, position.y + Height * 0.5f, position.width, Height);
            Rect propRect  = new Rect(position.x, position.y + propH        , position.width, Height);
            Rect errorRect = new Rect(position.x, position.y + errorY       , position.width, errorH);
            Type fieldType = fieldInfo.FieldType;

            const float ResetBtnW = 20f;
            Rect infoRectL = new Rect(position.x, position.y + Height * 0.5f, position.width - ResetBtnW, Height);
            Rect infoRectR = new Rect(position.x + position.width - ResetBtnW, position.y + Height * 0.5f, ResetBtnW, Height);


            // 컴포넌트 상속 타입이 아닌 타입 - 에러박스
            if (fieldType.IsSubclassOf(typeof(Component)) == false)
            {
                // 배열, 리스트면 크기를 0으로 고정하고 콘솔 에러 메시지
                if (fieldType.Ex_IsArrayOrListType())
                {
                    fieldInfo.SetValue(property.serializedObject.targetObject, null);
                    Debug.LogError("[AutoInject] 배열 또는 리스트에는 사용할 수 없습니다.");
                }

                if (isFullDeco)
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
            string includeDisabled = Atr.IncludeDisabledObject ? " (+ Disabled)" : "";

            // 인젝션
            {
                Component @this = property.serializedObject.targetObject as Component;

                // Inject 수행
                if(Atr.NameIncludes == null)
                    EditorDIHelper.Inject(@this, Atr, fieldType, out foundTarget);
                else
                    EditorDIHelper.Inject_NC(@this, Atr, fieldType, Atr.NameIncludes, out foundTarget);

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

            // 데코레이터 표시 여부
            if (isFullDeco)
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
            if (isMiniDeco)
            {
                const float MiniW = 20f;
                miniRect = new Rect(position.x - MiniW, position.y, MiniW, position.height);

                GUIContent icon =
                    isInjectionFailed ? IconRed : 
                    isRefDirty ? IconYellow : 
                    IconGreen;

                Color oldCol = GUI.backgroundColor;
                if (Atr.IncludeDisabledObject) // Disabled 포함이면 검정 버튼
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

            if (isMiniDeco)
            {
                DrawTooltip(miniRect, 
                    $"{Atr.Method}{(Atr.IncludeDisabledObject ? " [D]" : "")}",
                    foundTarget == null ? "=> NULL" : $"=> {foundTarget.name}",
                    Atr.NameIncludes,
                    isRefDirty
                );
            }
        }

        private void DrawTooltip(in Rect eventRect, string method, string nextName, string nameIncludes, bool isDirty)
        {
            static Rect GetBgRect(Rect rect)
            {
                rect.x -= 1f; rect.width += 2f; rect.y -= 1f; rect.height += 2f; return rect;
            }

            Vector2 mPos =  Event.current.mousePosition;

            // 스트링으로 너비 계산
            const float WPad = 4f;
            float rWidth1 = GUI.skin.label.CalcSize(new GUIContent(method)).x + WPad;
            //const float rWidth1 = 200f;

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
                GUI.color = Color.cyan;
                GUI.Box(tooltipRect, method);
                GUI.color = c;

                // 포함 게임오브젝트명
                if (nameIncludes != null)
                {
                    //nameIncludes = $"includes: {nameIncludes}";
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
                    GUI.color = Color.yellow;
                    GUI.Box(nameRect, nameIncludes);
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