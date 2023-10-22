using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 날짜 : 2023-10-21 오후 8:03:48
// 작성자 : Rito15

namespace Rito.CAT.Demo
{
    public class Demo_ColorIf : MonoBehaviour
    {
        public bool condition1 = true;
        public bool condition2 = true;

        [Space(12)]
        [ColorIf(nameof(condition1), true, 2.0f, 0.5f, 0.5f)]
        public int _conditionTrue = 1;

        [ColorIf(nameof(condition1), false, 0.0f, 0.5f, 1.5f)]
        public float _conditionFalse = 2;

        [ColorIf(nameof(condition1), true, 1.0f, 0.5f, 0.5f)] // Next
        [ColorIf(nameof(condition2), true, 0.0f, 0.5f, 1.5f)] // Top priority
        public GameObject _conditionBoth;
    }
}