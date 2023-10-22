using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 날짜 : 2023-10-21 오후 8:03:48
// 작성자 : Rito15

namespace Rito.CAT.Demo
{
    public class Demo_ShowAndHideIf : MonoBehaviour
    {
        public bool condition = true;
        public bool condition2 = true;

        [Header("[Show If]")]
        [ShowIf(nameof(condition), true)]
        public int showVal1 = 1;

        [ShowIf(nameof(condition), true)]
        public float showVal2 = 2;

        [ShowIf(nameof(condition), true)]
        [ShowIf(nameof(condition2), true)]
        public GameObject showVal3;


        [Header("[Hide If]")]
        [HideIf(nameof(condition), true)]
        public float hideVal1 = 456f;

        [HideIf(nameof(condition), true)]
        public string hideVal2 = "Hide If Condition is true";
    }
}