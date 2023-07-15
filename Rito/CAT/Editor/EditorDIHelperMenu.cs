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
    // 2023-07-13
    public static class EditorDIHelperMenu
    {
        private const BindingFlags FIELD_FLAGS = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance;
        private static List<MonoBehaviour> injectables = new List<MonoBehaviour>(256);

        private const string MenuItemRootName = "Rito/EditorDI/";

        public static class ItemToggles
        {
            public struct OnOff
            {
                private const string MenuItemTitle = MenuItemRootName + "On";
                public static bool Value
                {
                    get => EditorPrefs.GetBool(MenuItemTitle, true);
                    private set => EditorPrefs.SetBool(MenuItemTitle, value);
                }

                [MenuItem(MenuItemTitle, false, priority = 101)]
                private static void MenuItem()
                {
                    Value = !Value;
                }

                [MenuItem(MenuItemTitle, true, priority = 101)]
                private static bool MenuItem_Validate()
                {
                    Menu.SetChecked(MenuItemTitle, Value);
                    return true;
                }
            }
            public struct ShowDeco
            {
                private const string MenuItemTitle = MenuItemRootName +  "Show Decorator";
                public static bool Value
                {
                    get => EditorPrefs.GetBool(MenuItemTitle, true);
                    private set => EditorPrefs.SetBool(MenuItemTitle, value);
                }

                [MenuItem(MenuItemTitle, false, priority = 102)]
                private static void MenuItem()
                {
                    Value = !Value;
                }

                [MenuItem(MenuItemTitle, true, priority = 102)]
                private static bool MenuItem_Validate()
                {
                    Menu.SetChecked(MenuItemTitle, Value);
                    return true;
                }
            }
        }

        [MenuItem(MenuItemRootName + "Force Update Now", priority = 601)]
        private static void ForceRunInjector()
        {
            ScanSceneForInjectables();
            //ResetInjectedDependencies();
            InjectDependencies();

            // 다음 에디터 프레임에 실행
            //EditorApplication.delayCall += () => {
            //    InjectDependencies();
            //    EditorApplication.delayCall -= InjectDependencies;
            //};
        }

        // EditorAutoInject 애트리뷰트를 필드에 하나라도 갖고 있는 컴포넌트 목록 가져오기
        private static void ScanSceneForInjectables()
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
        private static void InjectDependencies()
        {
            foreach (MonoBehaviour mono in injectables)
            {
                //Debug.LogWarning($"[{mono.gameObject.name}] {mono.GetType()}");
                Undo.RegisterCompleteObjectUndo(mono, "Rito.CAT - Inject");

                Type        mtype = mono.GetType();
                FieldInfo[] fis   = mtype.GetFields(FIELD_FLAGS);
                Transform   tr    = mono.transform;

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
                            EditorDIHelper.Inject_NC(tr, atr, ftype, atr.NameIncludes, out resObj);

                        finfo.SetValue(mono, resObj); // 강제로 상태 업데이트하여 인젝션 수행
                    }
                }
            }
            Debug.Log("Force Injected");
        }
    }
}
#endif