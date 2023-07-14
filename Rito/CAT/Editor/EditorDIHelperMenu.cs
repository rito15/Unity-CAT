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

        [MenuItem("Rito/Editor DI/Force Update Now")]
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

        // AutoInject된 레퍼런스들 모두 강제 초기화
        private static void ResetInjectedDependencies()
        {
            foreach (MonoBehaviour mono in injectables)
            {
                Type mtype = mono.GetType();

                UnityEditor.SerializedObject so = new SerializedObject(mono);
                UnityEditor.SerializedProperty sp = so.GetIterator();

                // 필드 순회
                while (sp.NextVisible(true))
                {
                    Component fcom = sp.objectReferenceValue as Component;
                    if (fcom == null) continue;

                    FieldInfo finfo = mtype.GetField(sp.name, FIELD_FLAGS);
                    Type ftype = fcom.GetType();

                    // EditorAutoInject 애트리뷰트가 선언되어 있는지 검사
                    if (Attribute.IsDefined(finfo, typeof(EditorDIAttribute)))
                    {
                        if (!ftype.Ex_IsChildOrEqualsTo(typeof(Component))) continue;
                        sp.objectReferenceValue = null;
                    }
                }
                so.ApplyModifiedProperties();
            }

            Debug.Log("Reset Injected References");
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
                        EditorDIHelper.InjectByTr(tr, atr, ftype, out UnityEngine.Object resObj);

                        finfo.SetValue(mono, resObj); // 강제로 상태 업데이트하여 인젝션 수행
                    }
                }

                // TODO: SO 찾아서 Apply
            }
            Debug.Log("Force Injected");
        }


        // 이거 안됨
        // 이거 안됨
        // 이거 안됨
        // 이거 안됨
        // 이거 안됨
        // 이거 안됨
        // 이거 안됨
        // 이거 안됨
        // 이거 안됨
        // 이거 안됨
        // 이거 안됨
        // 이거 안됨
        // 이거 안됨
        // 이거 안됨
        // 이거 안됨
        // 이거 안됨
        // 이거 안됨
        // 이거 안됨
        // 이거 안됨
        // AutoInject된 레퍼런스들 모두 강제 재할당 => 이거 안됨
        private static void InjectDependencies22()
        {
            foreach (MonoBehaviour mono in injectables)
            {
                Debug.LogWarning($"[{mono.gameObject.name}] {mono.GetType()}");
                Type mtype = mono.GetType();
                Transform tr = mono.transform;

                UnityEditor.SerializedObject so = new SerializedObject(mono);
                UnityEditor.SerializedProperty sp = so.GetIterator();

                // 필드 순회
                while (sp.NextVisible(true))
                {
                    Component fcom = sp.serializedObject.targetObject as Component;

                    //Component fcom = sp.objectReferenceValue as Component;
                    //if (fcom == null) continue;

                    //Debug.Log(fcom.name);

                    FieldInfo finfo = mtype.GetField(sp.name, FIELD_FLAGS);
                    Type ftype = fcom.GetType();

                    // EditorAutoInject 애트리뷰트가 선언되어 있는지 검사
                    if (Attribute.IsDefined(finfo, typeof(EditorDIAttribute)))
                    {
                        if (!ftype.Ex_IsChildOrEqualsTo(typeof(Component))) continue;

                        EditorDIAttribute atr = finfo.GetCustomAttribute<EditorDIAttribute>();
                        EditorDIHelper.InjectByTr(tr, atr, ftype, out UnityEngine.Object resObj);

                        // 강제로 상태 업데이트하여 인젝션 수행
                        //sp.objectReferenceValue = resObj != null ? resObj : null;
                        sp.objectReferenceValue = resObj;
                    }
                }
                so.ApplyModifiedProperties();
            }

            Debug.Log("Force Injected");
        }
    }
}
#endif