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
        public static Component GetComponentInAllObjectsInScene(Type targetType)
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

    }
}