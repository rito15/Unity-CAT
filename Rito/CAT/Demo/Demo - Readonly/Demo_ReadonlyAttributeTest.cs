#pragma warning disable CS0414

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 날짜 : 2023-10-21 오후 6:39:13
// 작성자 : Rito15

namespace Rito.CAT.Demo
{
    public class Demo_ReadonlyAttributeTest : MonoBehaviour
    {
        [Readonly] public int intValue = 3;
        [Readonly, SerializeField] private bool boolValue = true;
        [Readonly, EditorDI(ComponentType = typeof(Camera))] public GameObject go;
        [Readonly] public List<int> intList = new List<int> { 1, 2, 3 };
        [Readonly] public CustomClass customClass = new CustomClass { a = 10, b = "string" };

        [System.Serializable]
        public class CustomClass
        {
            public int a;
            public string b;
        }
    }
}

#pragma warning restore CS0414