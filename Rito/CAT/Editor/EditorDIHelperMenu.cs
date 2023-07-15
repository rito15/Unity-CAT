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
        private const string MenuItemRootName = "Rito/EditorDI/";

        [MenuItem(MenuItemRootName + "Force Inject Now", priority = 601)]
        private static void ForceRunInjector()
        {
            EditorDIRunner.ForceRunInjector();
            Debug.Log("Rito,CAT.EditorDI: Force Injected");
        }
    }
}
#endif