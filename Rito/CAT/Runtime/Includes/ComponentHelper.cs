using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Rito.CAT
{
    public static class ComponentHelper
    {
        /// <summary>
        /// 씬 내에서 비활성화된 오브젝트를 포함해, 모든 게임오브젝트에서 특정 타입의 컴포넌트 탐색
        /// </summary>
        public static Component FindComponentInScene(Type targetType)
        {
            Scene currentScene = SceneManager.GetActiveScene();
            GameObject[] rootObjects = currentScene.GetRootGameObjects();

            foreach (GameObject rootObject in rootObjects)
            {
                Component found = LocalRecur_GetComponentInChildren(rootObject.transform, targetType);
                if (found != null)
                {
                    return found;
                }
            }
            return null;
            
            // -
            static Component LocalRecur_GetComponentInChildren(Transform tr, Type targetType)
            {
                var found = tr.GetComponent(targetType);
                if (found != null)
                    return found;

                int childCount = tr.childCount;
                for (int i = 0; i < childCount; i++)
                {
                    found = LocalRecur_GetComponentInChildren(tr.GetChild(i), targetType);
                    if (found != null)
                        return found;
                }

                return null;
            }
        }

        /// <summary>
        /// 씬 내에서 비활성화된 오브젝트를 포함해, 모든 게임오브젝트에서 특정 타입의 컴포넌트 탐색
        /// <para/> - 이름 포함 검색
        /// </summary>
        public static Component FindComponentInScene_NC(Type targetType,
            string nameIncludes,
            string nameEquals,
            bool ignoreCase,
            bool evenDisabled // 비활성화 상태 포함
        )
        {
            GameObject[] rootObjects = SceneManager.GetActiveScene().GetRootGameObjects();

            foreach (GameObject rootObject in rootObjects)
            {
                Component found = rootObject.transform.Ex_GetComponentInChildren_NC(
                    targetType, nameIncludes, nameEquals, ignoreCase, true, evenDisabled    
                );
                if (found != null)
                {
                    return found;
                }
            }
            return null;
        }
    }
}