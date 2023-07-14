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
        public static void InjectByTr(Transform tr, EditorDIAttribute atr, Type fieldType, out UnityEngine.Object found)
        {
            bool isMyGameObjDisabled = !tr.gameObject.activeInHierarchy;
            found = null;

            switch (atr.Option)
            {
                // 2023. 07. 12. Pass
                case DiMethod.GetComponent:
                    // 본인 스스로가 타겟이므로, 자신이 비활성화 상태면 동작 X
                    if (!atr.IncludeDisabledObject && isMyGameObjDisabled) break;

                    found = tr.GetComponent(fieldType);
                    break;

                // 2023. 07. 12. Pass
                // 본인 + 자식
                case DiMethod.GetComponentInChildren:
                    found = atr.IncludeDisabledObject ?
                            tr.Ex_GetComponentInAllChildren(fieldType) :
                            tr.Ex_GetComponentInChildrenIfEnabled(fieldType);
                    break;

                // 2023. 07. 12. Pass
                // 자식만
                case DiMethod.GetComponentInChildrenOnly:
                    found = atr.IncludeDisabledObject ?
                            tr.Ex_GetComponentInAllChildrenOnly(fieldType) :
                            tr.Ex_GetComponentInChildrenOnlyIfEnabled(fieldType);
                    break;

                // 2023. 07. 12. Pass
                case DiMethod.GetComponentInParents:
                    found = atr.IncludeDisabledObject ?
                            tr.Ex_GetComponentInParentsEvenDisabled(fieldType) :
                            tr.GetComponentInParent(fieldType);
                    break;

                // 2023. 07. 12. Pass
                case DiMethod.GetComponentInParentsOnly:
                    found = atr.IncludeDisabledObject ?
                            tr.Ex_GetComponentInParentsOnlyEvenDisabled(fieldType) :
                            tr.Ex_GetComponentInParentsOnly(fieldType);
                    break;

                // 2023. 07. 13. Pass
                case DiMethod.FindObjectOfType:
                    found = atr.IncludeDisabledObject ?
                            ComponentHelper.GetComponentInAllObjectsInScene(fieldType) :
                            UnityEngine.Object.FindObjectOfType(fieldType);
                    break;

            } // switch
        }

        public static void Inject(Component comField, EditorDIAttribute atr, Type fieldType, out UnityEngine.Object found)
        {
            bool isMyGameObjDisabled = !comField.gameObject.activeInHierarchy;
            found = null;

            switch (atr.Option)
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
                            ComponentHelper.GetComponentInAllObjectsInScene(fieldType) :
                            UnityEngine.Object.FindObjectOfType(fieldType);
                    break;

            } // switch
        }
    }
}
#endif