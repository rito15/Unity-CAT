using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 날짜 : 2023-10-21 오후 8:03:48
// 작성자 : Rito15

namespace Rito.CAT.Demo
{
    public class Demo_ReadonlyIf : MonoBehaviour
    {
        public bool condition = true;
        [Range(100, 101)] public int  mustBe100 = 100;

        [Space(12)]
        [ReadonlyIf(nameof(condition), true)]
        public int _conditionTrue = 1;

        [ReadonlyIf(nameof(condition), true, EQorNE:false)]
        public float _conditionFalse = 2;

        [ReadonlyIf(nameof(condition), true)] // OR 1
        [ReadonlyIf(nameof(mustBe100), 100)]  // OR 2
        public GameObject _conditionTrueOr100;
    }
}