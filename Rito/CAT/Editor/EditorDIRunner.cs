#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using System;
using System.Reflection;
using System.Linq;

namespace Rito.CAT
{
    // 2023-07-15
    public static class EditorDIRunner
    {
        private const BindingFlags FIELD_FLAGS = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance;
        private static List<MonoBehaviour> injectables = new List<MonoBehaviour>(256);

        /// <summary>
        /// 당장 인젝션 수행
        /// </summary>
        public static void ForceRunInjector()
        {
            ScanSceneForInjectables();
            InjectDependencies();

            // 다음 에디터 프레임에 실행
            //EditorApplication.delayCall += () => {
            //    InjectDependencies();
            //    EditorApplication.delayCall -= InjectDependencies;
            //};
        }

        // EditorAutoInject 애트리뷰트를 필드에 하나라도 갖고 있는 컴포넌트 목록 가져오기
        public static void ScanSceneForInjectables()
        {
            Scene currentScene = SceneManager.GetActiveScene();
            GameObject[] rootObjects = currentScene.GetRootGameObjects();

            injectables.Clear();

            foreach (GameObject rootObject in rootObjects)
            {
                MonoBehaviour[] scripts = rootObject.GetComponentsInChildren<MonoBehaviour>(true);
                foreach (MonoBehaviour script in scripts)
                {
                    Type type = script.GetType();
                    FieldInfo[] fields = type.GetFields(FIELD_FLAGS);

                    if (fields.Any(field => Attribute.IsDefined(field, typeof(EditorDIAttribute))))
                    {
                        injectables.Add(script);
                    }
                }
            }
        }

        // SO, SP 대신 리플렉션 사용
        public static void InjectDependencies()
        {
            foreach (MonoBehaviour mono in injectables)
            {
                //Debug.LogWarning($"[{mono.gameObject.name}] {mono.GetType()}");
                Undo.RegisterCompleteObjectUndo(mono, "Rito.CAT - Inject");

                Type mtype = mono.GetType();
                FieldInfo[] fis = mtype.GetFields(FIELD_FLAGS);
                Transform tr = mono.transform;

                foreach (FieldInfo finfo in fis)
                {
                    Type ftype = finfo.FieldType;

                    if (Attribute.IsDefined(finfo, typeof(EditorDIAttribute)))
                    {
                        if (!ftype.Ex_IsChildOrEqualsTo(typeof(Component))) continue;

                        EditorDIAttribute atr = finfo.GetCustomAttribute<EditorDIAttribute>();

                        UnityEngine.Object resObj;
                        if (atr.NameIncludes == null)
                            EditorDIHelper.Inject(tr, atr, ftype, out resObj);
                        else
                            EditorDIHelper.Inject_NC(
                                tr, atr, ftype, atr.NameIncludes, atr.NameEquals, atr.IgnoreCase, out resObj);

                        finfo.SetValue(mono, resObj); // 강제로 상태 업데이트하여 인젝션 수행
                    }
                }
            }
        }
    }
}
#endif