using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Rito.CAT
{
    // 2023. 07. 15.
    public static class ComponentExtensionWithNameContained
    {
        // 멀티스레딩 배제: 공유 스택
        private static readonly Stack<Transform> _dfsSharedStack = new Stack<Transform>(48);

        // Test Pass: 2023-07-15 
        /// <summary>
        /// <para/> 이름 포함 자식 탐색
        /// </summary>
        public static Component Ex_GetComponentInChildren_NC(this Component @this, Type targetType, 
            string nameIncludes,
            string nameEquals,
            bool ignoreCase,
            bool includeSelf, // 자신 포함
            bool evenDisabled // 비활성화 상태 포함
        )
        {
            _dfsSharedStack.Clear();

            // 자신 포함/제외
            if (includeSelf)
            {
                _dfsSharedStack.Push(@this.transform);
            }
            else
            {
                foreach (Transform child in @this.transform)
                    _dfsSharedStack.Push(child);
            }

            while (_dfsSharedStack.Count > 0)
            {
                Transform current = _dfsSharedStack.Pop();

                // 비활성화 미포함 검사
                if (evenDisabled == false)
                {
                    // 비활성화 상태: 하위의 자식에 대한 추가 검사 하지 않음
                    if (!current.gameObject.activeInHierarchy) continue;
                }

                string currentName = current.name.Trim();
                if (ignoreCase)
                {
                    currentName  = currentName.ToLower();
                    nameIncludes = nameIncludes?.ToLower();
                }

                // nameEquals가 존재하는 경우 : nameIncludes 무시
                if (!string.IsNullOrEmpty(nameEquals))
                {
                    nameEquals = nameEquals.Trim();
                    if (ignoreCase) nameEquals = nameEquals.ToLower();

                    if (currentName.Equals(nameEquals))
                    {
                        Component foundComponent = current.GetComponent(targetType);
                        if (foundComponent != null) return foundComponent;
                    }
                }
                else if (currentName.Contains(nameIncludes))
                {
                    Component foundComponent = current.GetComponent(targetType);
                    if (foundComponent != null) return foundComponent;
                }

                foreach (Transform child in current)
                    _dfsSharedStack.Push(child);
            }

            return null;
        }

        // Test Pass: 2023-07-15 
        /// <summary>
        /// <para/> 이름 포함 부모 탐색
        /// </summary>
        public static Component Ex_GetComponentInParents_NC(this Component @this, Type targetType, 
            string nameIncludes,
            string nameEquals,
            bool ignoreCase,
            bool includeSelf, // 자신 포함
            bool evenDisabled // 비활성화 상태 포함
        )
        {
            Transform target = @this.transform;

            // 자신 제외인 경우
            if (includeSelf == false)
            {
                if (@this.transform.parent == null) return null;
                target = @this.transform.parent;
            }

            while (target != null)
            {
                // 비활성화 상태를 포함하지 않는 경우
                if (evenDisabled == false)
                {
                    if (target.gameObject.activeInHierarchy == false) return null;
                }

                string targetName = target.name.Trim();
                if (ignoreCase)
                {
                    targetName   = targetName.ToLower();
                    nameIncludes = nameIncludes?.ToLower();
                }

                // nameEquals가 존재하는 경우 : nameIncludes 무시
                if (!string.IsNullOrEmpty(nameEquals))
                {
                    nameEquals = nameEquals.Trim();
                    if (ignoreCase) nameEquals = nameEquals.ToLower();

                    if (!targetName.Equals(nameEquals.Trim()))
                    {
                        Component com = target.GetComponent(targetType);
                        if (com != null) return com;
                    }
                }
                else if (targetName.Contains(nameIncludes))
                {
                    Component com = target.GetComponent(targetType);
                    if (com != null) return com;
                }

                target = target.parent;
            }

            return null;
        }
    }
}
