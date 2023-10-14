#if UNITY_EDITOR
using Rito.CAT;
using System;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;

namespace Rito.CAT
{
    public static class EditorDIHelper
    {
        public static void Inject(Component comField, EditorDIAttribute atr, Type fieldType, out UnityEngine.Object found)
        {
            bool isMyGameObjDisabled = !comField.gameObject.activeInHierarchy;
            found = null;

            switch (atr.Method)
            {
                // 2023. 07. 12. Pass
                case DiMethod.GetComponent:
                    // 본인 스스로가 타겟이므로, 자신이 비활성화 상태면 동작 X
                    if (!atr.IncludeDisabledObject && isMyGameObjDisabled) break;

                    found = comField.GetComponent(fieldType);
                    break;

                // 2023. 07. 12. Pass
                // 본인 + 자식
                case DiMethod.GetComponentInChildren:
                    found = atr.IncludeDisabledObject ?
                            comField.Ex_GetComponentInAllChildren(fieldType) :
                            comField.Ex_GetComponentInChildrenIfEnabled(fieldType);
                    break;

                // 2023. 07. 12. Pass
                // 자식만
                case DiMethod.GetComponentInChildrenOnly:
                    found = atr.IncludeDisabledObject ?
                            comField.Ex_GetComponentInAllChildrenOnly(fieldType) :
                            comField.Ex_GetComponentInChildrenOnlyIfEnabled(fieldType);
                    break;

                // 2023. 07. 12. Pass
                case DiMethod.GetComponentInParents:
                    found = atr.IncludeDisabledObject ?
                            comField.Ex_GetComponentInParentsEvenDisabled(fieldType) :
                            comField.GetComponentInParent(fieldType);
                    break;

                // 2023. 07. 12. Pass
                case DiMethod.GetComponentInParentsOnly:
                    found = atr.IncludeDisabledObject ?
                            comField.Ex_GetComponentInParentsOnlyEvenDisabled(fieldType) :
                            comField.Ex_GetComponentInParentsOnly(fieldType);
                    break;

                // 2023. 07. 13. Pass
                case DiMethod.FindObjectOfType:
                    found = atr.IncludeDisabledObject ?
                            ComponentHelper.FindComponentInScene(fieldType) :
                            UnityEngine.Object.FindObjectOfType(fieldType);
                    break;

            } // switch
        }

        // 게임오브젝트 이름 포함 or 일치
        public static void Inject_NC(Component comField, EditorDIAttribute atr, Type fieldType, 
            string nameIncludes,
            string nameEquals,
            bool ignoreCase,
            out UnityEngine.Object found
        )
        {
            bool isMyGameObjDisabled = !comField.gameObject.activeInHierarchy;
            found = null;

            bool includeSelf =
                atr.Method == DiMethod.FindObjectOfType ||
                atr.Method == DiMethod.GetComponentInChildren ||
                atr.Method == DiMethod.GetComponentInParents;
            bool evenDisabled = atr.IncludeDisabledObject;

            switch (atr.Method)
            {
                case DiMethod.GetComponent:
                    // 본인 스스로가 타겟이므로, 자신이 비활성화 상태면 동작 X
                    if (!atr.IncludeDisabledObject && isMyGameObjDisabled) break;

                    found = comField.GetComponent(fieldType);
                    break;

                case DiMethod.GetComponentInChildren:
                case DiMethod.GetComponentInChildrenOnly:
                    found = comField.Ex_GetComponentInChildren_NC(
                        fieldType, nameIncludes, nameEquals, ignoreCase, includeSelf, evenDisabled);
                    break;

                case DiMethod.GetComponentInParents:
                case DiMethod.GetComponentInParentsOnly:
                    found = comField.Ex_GetComponentInParents_NC(
                        fieldType, nameIncludes, nameEquals, ignoreCase, includeSelf, evenDisabled);
                    break;

                case DiMethod.FindObjectOfType:
                    found = ComponentHelper.FindComponentInScene_NC(
                        fieldType, nameIncludes, nameEquals, ignoreCase, evenDisabled);
                    break;

            } // switch
        }
    }
}
#endif