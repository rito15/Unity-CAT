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
    [CustomPropertyDrawer(typeof(EditorAutoInjectAttribute), true)]
    public class EditorAutoInjectAttributeDrawer : PropertyDrawer
    {
        EditorAutoInjectAttribute Atr => attribute as EditorAutoInjectAttribute;

        private float Height { get; set; } =
#if UNITY_2019_3_OR_NEWER
            18;
#else
            16f;
#endif

        private bool IsPlayMode => EditorApplication.isPlaying;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            //return EditorGUI.GetPropertyHeight(property, label, true);
            return IsPlayMode ? Height : Height * 2.5f;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (!IsPlayMode)
            {
                DrawEditModeGUI(position, property, label);
            }
            else
            {
                DrawPlayModeGUI(position, property, label);
            }
        }

        private void DrawEditModeGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Rect infoRect  = new Rect(position.x, position.y + Height * 0.5f, position.width, Height     );
            Rect propRect  = new Rect(position.x, position.y + Height * 1.5f, position.width, Height     );
            Rect errorRect = new Rect(position.x, position.y + Height * 0.5f, position.width, Height * 2f);
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

                // 그 외의 경우, 인스펙터에 큼지막한 에러 박스
                EditorHelper.ColorErrorBox(errorRect, 
                    $"[{fieldType}] {fieldInfo.Name}\n" +
                    $"Error : Component를 상속하는 타입에만 사용할 수 있습니다"
                );
                return;
            }

            // 컴포넌트 타입 - 올바르게 동작 ===========================================================
            bool isRefDirty;

            // 업데이트 필요 대상(기존 레퍼런스가 null이거나 미스매치)
            bool isUpdateRequired =
                property.objectReferenceValue == null ||
                property.objectReferenceValue.GetType().Ex_IsChildOrEqualsTo(fieldType) == false;

            UnityEngine.Object foundTarget = null;

            {
                Component @this = property.serializedObject.targetObject as Component;
                bool isMyGameObjEnabled  = @this.gameObject.activeInHierarchy;
                bool isMyGameObjDisabled = !isMyGameObjEnabled;

                switch (Atr.Option)
                {
                    // 2023. 07. 12. Pass
                    case EInjection.GetComponent:
                        // 본인 스스로가 타겟이므로, 자신이 비활성화 상태면 동작 X
                        if (!Atr.IncludeDisabledObject && isMyGameObjDisabled) break;

                        foundTarget = @this.GetComponent(fieldType);
                        break;

                    // 2023. 07. 12. Pass
                    // 본인 + 자식
                    case EInjection.GetComponentInChildren:
                        foundTarget =
                            Atr.IncludeDisabledObject ? 
                                @this.Ex_GetComponentInAllChildren(fieldType) :
                                @this.Ex_GetComponentInChildrenIfEnabled(fieldType)
                        ;
                        break;

                    // 2023. 07. 12. Pass
                    // 자식만
                    case EInjection.GetComponentInChildrenOnly:
                        foundTarget =
                            Atr.IncludeDisabledObject ?
                                @this.Ex_GetComponentInAllChildrenOnly(fieldType) :
                                @this.Ex_GetComponentInChildrenOnlyIfEnabled(fieldType)
                        ;
                        break;

                    case EInjection.GetComponentInParents:
                        foundTarget =
                            Atr.IncludeDisabledObject ?
                                @this.Ex_GetComponentInParentsEvenDisabled(fieldType) :
                                @this.GetComponentInParent(fieldType)
                        ;
                        break;

                    case EInjection.GetComponentInParentsOnly:
                        foundTarget =
                            Atr.IncludeDisabledObject ?
                                @this.Ex_GetComponentInParentsOnlyEvenDisabled(fieldType) :
                                @this.Ex_GetComponentInParentsOnly(fieldType)
                        ;
                        break;

                    // TODO: 활성/비활성 구분하여 구현
                    // TODO: 활성/비활성 구분하여 구현
                    // TODO: 활성/비활성 구분하여 구현
                    // TODO: 활성/비활성 구분하여 구현
                    // TODO: 활성/비활성 구분하여 구현
                    // TODO: 활성/비활성 구분하여 구현
                    // TODO: 활성/비활성 구분하여 구현
                    // TODO: 활성/비활성 구분하여 구현
                    // TODO: 활성/비활성 구분하여 구현
                    // TODO: 활성/비활성 구분하여 구현
                    case EInjection.FindObjectOfType:
                        foundTarget = UnityEngine.Object.FindObjectOfType(fieldType);
                        break;

                } // switch

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

            // 실행 결과 - 할당 성공
            if (property.objectReferenceValue != null)
            {
                //EditorHelper.ColorInfoBox(infoRect, Color.green, $"{Atr.Option}");

                string foundObjName = foundTarget == null ? "Null" : foundTarget.name;
                string dirtyText = isRefDirty ? $"[* {foundObjName}]" : "";

                EditorHelper.ColorInfoBox(infoRectL, Color.green, $"{Atr.Option} {dirtyText}");
                if (GUI.Button(infoRectR, "")) // 강제 리셋 버튼
                {
                    property.objectReferenceValue = null;
                };
            }
            // 실행 결과 - 실패(대상이 없음)
            else
            {
                string includeDisabled = Atr.IncludeDisabledObject ? " (Include Disabled)" : "";
                EditorHelper.ColorWarningBox(infoRect, $"{Atr.Option}{includeDisabled} - Failed : 대상을 찾지 못했습니다");
            }
            EditorGUI.PropertyField(propRect, property, label, true);
        }

        private void DrawPlayModeGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.PropertyField(position, property, label, true);
        }
    }
}
#endif