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
    public static class EditorDIPrefs
    {
        private const string MenuItemRootName = "Rito/EditorDI/";

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
            private const string MenuItemTitle = MenuItemRootName + "Show Decorator";
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
        public struct FullDeco
        {
            private const string MenuItemTitle = MenuItemRootName + "Full Decorator";
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
        public struct AutoInjectBeforeEnteringPlaymode
        {
            private const string MenuItemTitle = MenuItemRootName + "AutoInject Before Entering Playmode";
            public static bool Value
            {
                get => EditorPrefs.GetBool(MenuItemTitle, true);
                private set => EditorPrefs.SetBool(MenuItemTitle, value);
            }

            [MenuItem(MenuItemTitle, false, priority = 301)]
            private static void MenuItem()
            {
                Value = !Value;
            }

            [MenuItem(MenuItemTitle, true, priority = 301)]
            private static bool MenuItem_Validate()
            {
                Menu.SetChecked(MenuItemTitle, Value);
                return true;
            }
        }

    }
}
#endif