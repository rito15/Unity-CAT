using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Rito.CAT
{
    /// <summary>
    /// 2020. 05. 18.
    /// <para/> Component 확장
    /// </summary>
    public static class ComponentExtension
    {
        // 1. 자기 + 자식 => 비활성화 제외 : Ex_GetComponentInChildrenIfEnabled
        // 2. 자기 + 자식 => 비활성화 포함 : Ex_GetComponentInAllChildren
        // 3.        자식 => 비활성화 제외 : Ex_GetComponentInChildrenOnlyIfEnabled
        // 4.        자식 => 비활성화 포함 : Ex_GetComponentInAllChildrenOnly

        // 1. 자기 + 부모 => 비활성화 제외 : GetComponentInParent
        // 2. 자기 + 부모 => 비활성화 포함 : Ex_GetComponentInParentsEvenDisabled
        // 3.        부모 => 비활성화 제외 : Ex_GetComponentInParentsOnly
        // 4.        부모 => 비활성화 포함 : Ex_GetComponentInParentsOnlyEvenDisabled

        // 2023. 07. 12. Pass
        /// <summary>
        /// <para/> 자신: 포함
        /// <para/> 자식: 포함
        /// <para/> 비활성화 상태: 제외
        /// </summary>
        public static Component Ex_GetComponentInChildrenIfEnabled(this Component @this, Type targetType)
        {
            Transform transform = @this.transform;
            if (transform.gameObject.activeInHierarchy == false) 
                return null;
            return transform.GetComponentInChildren(targetType);
        }

        // 2023. 07. 12. Pass
        /// <summary>
        /// <para/> 자신: 제외
        /// <para/> 자식: 포함
        /// <para/> 비활성화 상태: 제외
        /// </summary>
        public static Component Ex_GetComponentInChildrenOnlyIfEnabled(this Component @this, Type targetType)
        {
            Transform transform = @this.transform;

            if (transform.childCount.Equals(0))
                return null;

            int childCount = transform.childCount;
            for (int i = 0; i < childCount; i++)
            {
                Transform c = transform.GetChild(i);
                if (c.gameObject.activeInHierarchy == false)
                    continue;

                var targetComponent = c.GetComponentInChildren(targetType);
                if (targetComponent != null)
                    return targetComponent;
            }

            return null;
        }

        // 2023. 07. 12. Pass
        // 2023. 07. 13. DFS로 변경
        /// <summary>
        /// <para/> 자신: 포함
        /// <para/> 자식: 포함
        /// <para/> 비활성화 상태: 포함
        /// </summary>
        public static Component Ex_GetComponentInAllChildren(this Component @this, Type targetType)
        {
            return LocalRecurFunc(@this.transform, targetType);

            static Component LocalRecurFunc(Transform tr, Type targetType)
            {
                var found = tr.GetComponent(targetType);
                if (found != null)
                    return found;

                int childCount = tr.childCount;
                for (int i = 0; i < childCount; i++)
                {
                    found = LocalRecurFunc(tr.GetChild(i), targetType);
                    if (found != null)
                        return found;
                }

                return null;
            }
        }

        // 2023. 07. 12. Pass
        // 2023. 07. 13. DFS로 변경
        /// <summary>
        /// <para/> 자신: 제외
        /// <para/> 자식: 포함
        /// <para/> 비활성화 상태: 포함
        /// </summary>
        public static Component Ex_GetComponentInAllChildrenOnly(this Component @this, Type targetType)
        {
            return LocalRecurFunc(@this.transform, targetType);

            static Component LocalRecurFunc(Transform tr, Type targetType)
            {
                int childCount = tr.childCount;
                for (int i = 0; i < childCount; i++)
                {
                    Transform child = tr.GetChild(i);
                    var found = child.GetComponent(targetType);

                    if (found != null)
                        return found;

                    found = LocalRecurFunc(child, targetType);
                    if (found != null)
                        return found;
                }

                return null;
            }
        }


        // 2023. 07. 12. Pass
        /// <summary>
        /// <para/> 자신: 제외
        /// <para/> 부모: 포함
        /// <para/> 비활성화 상태: 제외
        /// </summary>
        public static Component Ex_GetComponentInParentsOnly(this Component @this, Type targetType)
        {
            Transform transform = @this.transform;

            if (transform.parent == null)
                return null;

            return transform.parent.GetComponentInParent(targetType);
        }

        /// <summary>
        /// <para/> 자신: 포함
        /// <para/> 부모: 포함
        /// <para/> 비활성화 상태: 포함
        /// </summary>
        public static Component Ex_GetComponentInParentsEvenDisabled(this Component @this, Type targetType)
        {
            Transform target = @this.transform;

            while (target != null)
            {
                Component com = target.GetComponent(targetType);
                if (com != null) return com;
                target = target.parent;
            }

            return null;
        }

        /// <summary>
        /// <para/> 자신: 제외
        /// <para/> 부모: 포함
        /// <para/> 비활성화 상태: 포함
        /// </summary>
        public static Component Ex_GetComponentInParentsOnlyEvenDisabled(this Component @this, Type targetType)
        {
            Transform target = @this.transform.parent;

            while (target != null)
            {
                Component com = target.GetComponent(targetType);
                if (com != null) return com;
                target = target.parent;
            }

            return null;
        }
    }
}
