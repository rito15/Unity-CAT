#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

namespace Rito.CAT
{
    // 플레이모드 시작 직전 동작
    [InitializeOnLoad]
    public class EditorDIPlaymodeHook
    {
        static EditorDIPlaymodeHook()
        {
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }

        private static void OnPlayModeStateChanged(PlayModeStateChange state)
        {
            if (EditorDIPrefs.AutoInjectBeforeEnteringPlaymode.Value == false) return;

            switch (state)
            {
                case PlayModeStateChange.ExitingEditMode:
                    EditorDIRunner.ForceRunInjector();
                    Debug.Log("Rito.CAT.EditorDI: AutoInject now working...");
                    break;
                case PlayModeStateChange.EnteredPlayMode:
                    Debug.Log("Rito.CAT.EditorDI: AutoInject just worked.");
                    break;
            }
        }
    }
}

#endif